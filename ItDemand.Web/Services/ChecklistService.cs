using AutoMapper;
using ItDemand.Domain.DataContext;
using ItDemand.Domain.Enums;
using ItDemand.Domain.Models;
using ItDemand.Web.Models;
using ItDemand.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.RegularExpressions;

namespace ItDemand.Web.Services
{
    public class ChecklistService
    {
        private readonly UserViewModel _user;
        private readonly ApplicationLog _log;
        private readonly ItDemandContext _db;
        private readonly IMapper _mapper;

        private IList<ChecklistNotificationModel> _itNotificationQueue;

        public ChecklistService(ApplicationLog log, ItDemandContext dbContext, IMapper mapper, UserViewModel user)
        {
            _db = dbContext;
            _log = log;
            _mapper = mapper;
            _user = user;

            _itNotificationQueue = new List<ChecklistNotificationModel>();
        }

        public ChecklistViewModel GetChecklist(int id)
        {
            var checklist = _db.Checklists
                .Include(x => x.DemandRequest)
                .Include(x => x.Approvers).ThenInclude(x => x.Approver)
                .Include(x => x.Questions)
                .SingleOrDefault(x => x.Id == id);

            if (checklist == null) return ChecklistViewModel.Empty;

            var vm = _mapper.Map<ChecklistViewModel>(checklist);

            // if there are any approvers that are the drop-down type
            // query db and fill in those lists with the appropriate people
            if (vm.Approvers.Any(x => x.Type == ApproverType.Dropdown))
            {
                foreach (var approver in vm.Approvers)
                {
                    var approvers = _db.Users.Where(x => x.SecurityRole.HasFlag(approver.Role));

                    // we want to show deactivated approvers for already approved checklists
                    // (for history).
                    if (vm.Status != StatusType.Approved)
                    {
                        approvers = approvers.Where(x => x.IsActive);
                    }

                    var approverList = approvers
                        .OrderBy(x => x.DisplayName)
                        .Select(x => new SelectListItem
                        {
                            Value = x.Id.ToString(),
                            Text = x.DisplayName
                        }).ToArray();

                    approver.ApproverList = approverList;
                }
            }

            // fill in the current user for the 'can sign' check
            foreach (var approver in vm.Approvers)
            {
                approver.CurrentUser = _user;
            }

            return vm;
        }

        public void SaveChecklist(ChecklistFormViewModel checklistData)
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                var model = GetChecklistModel(checklistData.Id);
                if (model == null)
                {
                    _log.Error($"Attempt to save checklist that does not exist (for Id: {checklistData.Id}).");
                    return;
                }

                // If the status of the checklist was changed based on user interaction (submitting
                // for review, rejecting, approval, etc.) check for state changes based on current
                // conditions).
                // Must be checked before mapping to model so we can detect changes between submitted
                // data and what is in database.
                var status = ProcessStatusChange(checklistData, model);

                var stateChanged = model.Status != status;
                var oldStatus = model.Status;

                _mapper.Map(checklistData, model);
                model.Status = status;
                
                ProcessApprovers(checklistData.Approvers, model);
                ProcessQuestions(checklistData.Questions, model);

                _db.SaveChanges();
                transaction.Commit();
                _log.Trace($"Checklist #{model.Id}: {model.Name} has been saved.");

                if (stateChanged)
                {
                    _log.Info($"Checklist #{model.Id}: {model.Name} status changed from {oldStatus} to {status}.");
                    SendApprovalNotification(model);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                transaction.Rollback();
                throw;
            }
        }

        private Checklist? GetChecklistModel(int id)
        {
            var checklist = _db.Checklists
                .Include(x => x.DemandRequest).ThenInclude(x => x.RequestOwner)
                .Include(x => x.DemandRequest).ThenInclude(x => x.RequestSponsor)
                .Include(x => x.DemandRequest).ThenInclude(x => x.ProjectManager)
                .Include(x => x.DemandRequest).ThenInclude(x => x.ItHead)
                .Include(x => x.Approvers).ThenInclude(x => x.Approver)
                .Include(x => x.Questions)
                .SingleOrDefault(x => x.Id == id);

            return checklist;
        }

        private StatusType ProcessStatusChange(ChecklistFormViewModel vm, Checklist model)
        {
            if (model.DemandRequest.ExecutionType == null)
                throw new Exception("Unable to process checklist state change. Execution Type of Demand Request is null.");

            // if the checklist was submitted for approval
            if (vm.Status == StatusType.WaitingApproval)
            {
                //if none of the approvers are required
                if (!vm.Approvers.Any(x => x.Required))
                {
                    //if at least one person has approved, then we assume the whole checklist is approved
                    if (vm.Approvers.Any(x => x.ApprovalDate.HasValue))
                    {
                        AssignState(
                            model.DemandRequest,
                            DetermineNextState(model.DemandRequest.ExecutionType.Value, model.DemandRequest.DemandState)
                        );
                        return StatusType.Approved;
                    }
                }
                //else, if at least one signature is required
                else
                {
                    //if all the approvers have signed off
                    if (vm.Approvers.Where(x => x.Required).All(x => x.ApprovalDate.HasValue))
                    {
                        var stateAssigned = false;

                        // Demand Review Gate 1
                        if (model.DemandRequest.ExecutionType == WorkflowType.ItDemandReview &&
                            (int)model.SequenceNumber == 1)
                        {
                            // A decision has been made during the Gate 1 Demand Review.
                            // Get the answer from the Execution Workflow question.
                            // This is the workflow that PMO has decieded on for this demand
                            // request.
                            var executionDecision =
                                vm.Questions.SingleOrDefault(x => x.Text == "Execution Workflow");

                            if (executionDecision != null)
                            {
                                switch (executionDecision.Answer)
                                {
                                    case "Proceed Locally (L2)":
                                        model.DemandRequest.ExecutionType = WorkflowType.ProceedLocallyL2;
                                        AssignState(model.DemandRequest, DemandState.ItHeadApproval);
                                        break;
                                    case "Fast Track (L3)":
                                        model.DemandRequest.ExecutionType = WorkflowType.FastTrackL3;
                                        AssignState(model.DemandRequest, DemandState.ItHeadApproval);
                                        break;
                                    case "Big Project (L4)":
                                        model.DemandRequest.ExecutionType = WorkflowType.BigProjectL4;
                                        AssignState(model.DemandRequest, DemandState.ItHeadInitialReview);
                                        break;
                                    default:
                                        // throw exception
                                        break;
                                }

                                stateAssigned = true;
                                var factory = new ProjectFactory(_log, _db);
                                //factory.CreateWorkItems(model.DemandRequest, IsCabReviewRequired());
                                factory.CreateWorkItems(model.DemandRequest, false);

                                //CheckForRedCabReviewNotify();
                            }
                            else
                            {
                                _log.Error(
                                    "Expecting a Execution Process Decision for Demand Review Gate 1 checklist and got NULL.");
                            }
                        }

                        // The following gates are only on L4 level IT projects.
                        if (model.DemandRequest.ExecutionType == WorkflowType.BigProjectL4)
                        {
                            CheckForSecurityReviewNotify(model);
                        }

                        if (!stateAssigned)
                        {
                            AssignState(
                                model.DemandRequest,
                                DetermineNextState(model.DemandRequest.ExecutionType.Value, model.DemandRequest.DemandState)
                            );
                        }

                        return StatusType.Approved;

                    }
                }
            }

            //if the status is new, and the user saves the form we move to the in progress state
            if (vm.Status == StatusType.New)
            {
                return StatusType.InProgress;
            }

            //if the status was rejected but the approval was cleared, set back to in progress 
            if (vm.Status == StatusType.Rejected && !vm.Approvers.Any(x => x.ApprovalDate.HasValue))
            {
                return StatusType.InProgress;
            }

            //if the status was approved but the checklist is "un-approved" 
            if (vm.Status == StatusType.Approved && vm.Status != StatusType.Approved)
            {
                //force every one to sign again by clearing out the approval date
                foreach (var approver in vm.Approvers)
                    approver.ApprovalDate = null;
            }

            return vm.Status;
        }

        private static void AssignState(DemandRequest demandRequest, DemandState state)
        {
            demandRequest.DemandState = state;

            if (state == DemandState.Complete)
                demandRequest.ProjectPhase = "Close";
        }

        private static DemandState DetermineNextState(WorkflowType executionType, DemandState currentState)
        {
            var nextState = currentState;

            if (executionType == WorkflowType.ItDemandReview)
            {
                // This is set above when 
            }
            else if (executionType == WorkflowType.ProceedLocallyL1)
            {
                switch (currentState)
                {
                    case DemandState.ItHeadApproval:
                        nextState = DemandState.Gate7Closeout;
                        break;
                    case DemandState.Gate7Closeout:
                        nextState = DemandState.Complete;
                        break;
                }
            }
            else if (executionType == WorkflowType.ProceedLocallyL2)
            {
                switch (currentState)
                {
                    case DemandState.ItHeadApproval:
                        nextState = DemandState.Gate7Closeout;
                        break;
                    case DemandState.Gate7Closeout:
                        nextState = DemandState.Complete;
                        break;
                }
            }
            else if (executionType == WorkflowType.FastTrackL3)
            {
                switch (currentState)
                {
                    // Optional transition to RED CAB handled in CheckCab function.
                    case DemandState.ItHeadApproval:
                        nextState = DemandState.Gate5PreGoLive;
                        break;
                    case DemandState.Gate5PreGoLive:
                        nextState = DemandState.Gate7Closeout;
                        break;
                    case DemandState.Gate7Closeout:
                        nextState = DemandState.Complete;
                        break;
                }
            }
            else if (executionType == WorkflowType.BigProjectL4)
            {
                switch (currentState)
                {
                    case DemandState.ItHeadInitialReview:
                        nextState = DemandState.SolutionDesign;
                        break;
                    case DemandState.SolutionDesign:
                        nextState = DemandState.SecurityReview;
                        break;
                    case DemandState.SecurityReview:
                        nextState = DemandState.ArchitectureReview;
                        break;
                    case DemandState.ArchitectureReview:
                        nextState = DemandState.DemandGate2;
                        break;
                    case DemandState.DemandGate2:
                        nextState = DemandState.ItHeadApproval;
                        break;
                    case DemandState.ItHeadApproval:
                        nextState = DemandState.Gate3Plan;
                        break;
                    case DemandState.Gate3Plan:
                        nextState = DemandState.Gate4Execute;
                        break;
                    case DemandState.Gate4Execute:
                        nextState = DemandState.Gate5PreGoLive;
                        break;
                    case DemandState.Gate5PreGoLive:
                        nextState = DemandState.Gate6GoLive;
                        break;
                    case DemandState.Gate6GoLive:
                        nextState = DemandState.Gate7Closeout;
                        break;
                    case DemandState.Gate7Closeout:
                        nextState = DemandState.Complete;
                        break;
                }
            }

            return nextState;
        }

        

        private void ProcessApprovers(ChecklistApproverViewModel[] approvers, Checklist model)
        {
            var userService = new UserService(_db, _mapper);

            // approvers are static and can be neither added or removed, only updated
            var toUpdate = approvers.Where(x => x.Id > 0).ToArray();

            foreach (var item in toUpdate)
            {
                var entity = model.Approvers.Single(x => x.Id == item.Id);
                _mapper.Map(item, entity);
                entity.Approver = userService.GetEmployeeById(item.ApproverId ?? 0);
            }
        }

        private void ProcessQuestions(ChecklistQuestionViewModel[] questions, Checklist model)
        {
            // questions are set and can be neither added or removed, only updated
            var toUpdate = questions.Where(x => x.Id > 0).ToArray();

            foreach (var item in toUpdate)
            {
                var entity = model.Questions.Single(x => x.Id == item.Id);

                // don't bother with automapper, it's only the two fields we handle below that change
				entity.Answer = entity.QuestionType == QuestionType.MultiSelect
					? string.Join(",", item.MultiSelectAnswers?.ToArray() ?? Array.Empty<string>())
					: item.Answer;

                entity.Comments = item.Comments;
            }
        }

        #region Notifications
        private void SendApprovalNotification(Checklist checklist)
        {
            if (checklist.Status == StatusType.New || checklist.Status == StatusType.InProgress) return;

            var notifyService = new NotificationService(_log, _db);

            var notificationModel = new ChecklistApprovalNotificationModel
            {
                ChecklistId = checklist.Id,
                ChecklistTitle = checklist.Name,
                DemandId = checklist.DemandRequestId,
                DemandName = checklist.DemandRequest.Name,
                Approvers = checklist.Approvers.Select(x => _mapper.Map<UserViewModel>(x.Approver)).ToArray(),
                SentBy = _user
            };

            if (checklist.Status == StatusType.WaitingApproval)
                notifyService.SendChecklistApprovalRequest(notificationModel);

            if (checklist.Status == StatusType.Rejected)
                notifyService.SendChecklistCorrectionsRequest(notificationModel);

            if (checklist.Status == StatusType.Approved)
                notifyService.SendChecklistApproved(notificationModel);
        }

        private void CheckForSecurityReviewNotify(Checklist checklist)
        {
            if (checklist.Name != "Solution Design Review") return;

            var demandRequest = checklist.DemandRequest;

            // Queue notification for Security Review.
            var notificationModel = new ChecklistNotificationModel
            {
                DemandId = demandRequest.Id,
                DemandName = demandRequest.Name,
                RequestOwnerEmail = demandRequest.RequestOwner?.Email ?? string.Empty,
                RequestSponsorEmail = demandRequest.RequestSponsor?.Email ?? string.Empty,
                ProjectManagerEmail = demandRequest.ProjectManager?.Email ?? string.Empty,
                ItHeadEmail = demandRequest.ItHead?.Email ?? string.Empty,
                ChecklistId = checklist.Id,
                NotificationType = NotificationType.ItsSecurityComplianceReview
            };

            _itNotificationQueue.Add(notificationModel);
        }
        #endregion
    }
}
