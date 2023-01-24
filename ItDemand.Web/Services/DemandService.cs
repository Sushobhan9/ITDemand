using AutoMapper;
using ItDemand.Domain.DataContext;
using ItDemand.Domain.Enums;
using ItDemand.Domain.Models;
using ItDemand.Domain.Repositories;
using ItDemand.Domain.Utils;
using ItDemand.Web.Models;
using ItDemand.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ItDemand.Web.Services
{
    public class DemandService
	{
		private readonly UserViewModel _user;
        private readonly ApplicationLog _log;
        private readonly ItDemandContext _db;
		private readonly IMapper _mapper;

		public DemandService(ApplicationLog log, ItDemandContext dbContext, IMapper mapper, UserViewModel user)
		{
			_db = dbContext;
            _log = log;
            _mapper = mapper;
			_user = user;
		}

		public DemandListItemViewModel[] GetDemandList(int businessUnitFilter = 0, WorkflowType projectType = 0, DemandState demandState = 0)
		{
			var query = _db.DemandRequests
				.Include(x => x.RequestOwner)
                .Include(x => x.RequestSponsor)
                .Include(x => x.ProjectManager)
                .Include(x => x.ItHead)
                .Include(x => x.BusinessUnit)
                .Include(x => x.ApplicationType)
                .Include(x => x.UsersImpacted)
                .AsQueryable();

			if (businessUnitFilter > 0)
                query = query.Where(x => x.BusinessUnitId == businessUnitFilter);

			// Unassigned might equal ExcutionType == null or Execution Type == ItDemandReview
            if (projectType > 0) 
				query = query.Where(x => x.ExecutionType == projectType);

            if (demandState > 0)
                query = query.Where(x => x.DemandState == demandState);

            var demands = query.Select(x => new DemandListItemViewModel(x));
            return demands.ToArray();
        }

		public DemandRequestViewModel? GetDemandRequest(int? id = null)
		{
			if (id.HasValue)
				return GetExistingDemandRequest(id.Value);
			else
				return GetNewDemandRequest();
		}

		public int SaveRequest(DemandRequestViewModel request)
		{
            DemandRequest? entity;

			var createMode = request.Id <= 0;

            entity = createMode ? new DemandRequest() : GetDemandRequest(request.Id);
           
            if (entity == null)
            {
                _log.Warn($"Entity was null when attempting to {(createMode ? "create" : "update")} Request {request.Name} ({request.Id}).");
                return 0;
            }

            // Before we map to the entity check for changes in some of the fields that we
            // need to react to if they are changed.
            //
            
            var submittedForReview = entity.SubmittedForReview == false && request.SubmittedForReview == true;

            // Record Submitted By/Date and update state of the Demand.
            if (submittedForReview)
            {
                request.SubmittedForReviewDate = DateTimeOffset.Now;
                request.SubmittedById = _user.Id;
                request.DemandState = DemandState.PmoReview;
            }

            var correctionsRequested = entity.RequestCorrections == false && request.RequestCorrections == true;

            // If PMO just requested corrections from the submitter, record the date, who did it, and
            // set the state of the Demand back to it's Initial Entry state.
            if (correctionsRequested)
            {
                request.SubmittedForReview = false;
                request.SubmittedForReviewDate = null;
                request.SubmittedById = null;
                request.SubmittedBy = null;
                request.DemandState = DemandState.InitialEntry;
                request.RequestCorrectionsById = _user.Id;
                request.RequestCorrectionsDate = DateTimeOffset.Now;
            }

            // Set flag if someone from PMO has reviewed the request and
            // assigned an Execution workflow for the project.
            // We create the workflow items after the main form data has been successfully saved.
            var createWorkflow = request.PmoReviewExecutionType.HasValue && !entity.PmoReviewExecutionType.HasValue;

            _mapper.Map(request, entity);

            var userService = new UserService(_db, _mapper);

            // Process any user fields that are part of the Request model.
            // We need to handle the fields that get there data from People Picer controls
            // special to make sure the data is properly assigned to the database record.
            SyncUserFields(request, entity);
            
            // Process the multi-select options fields.
            SyncAffectedBusinessUnits(request, entity);
            SyncComplianceRelevant(request, entity);
            SyncFileAttachments(request, entity);

            if (createMode)
			{
				entity.CreatedDate = DateTimeOffset.Now;
				entity.CreatedBy = userService.VerifyOrCreateUser(_user) ?? User.Default; // this shouldn't happen, what to do if it does?
                _db.DemandRequests.Add(entity);
            }
			else
			{
				entity.ModifiedDate = DateTimeOffset.Now;
                entity.ModifiedById = _user.Id;
                entity.ModifiedBy = null;
            }

            _db.SaveChanges();
            _log.Trace($"[{entity.Name}]: {(createMode ? "created" : "updated")}.");

            // If just assigned by PMO, create workflow items.
            if (createWorkflow)
            {
                // PMO has reviewed and determined if this request requires a
                // Gate 1 Review or can Proceed Locally (L1).
                // Create the necessary checklists.
                entity.ExecutionType = request.PmoReviewExecutionType;
                entity.PmoReviewById = _user.Id;
                entity.PmoReviewedOnDate = DateTimeOffset.Now;
                var factory = new ProjectFactory(_log, _db);
                factory.CreateWorkItems(entity);

                _log.Info($"[{entity.Name}]: PMO Review Complete. Assigned as {(entity.ExecutionType == null ? "Unknown" : entity.ExecutionType.GetDescription<WorkflowType>())}");

                // Assign proper state based on PMO Project Type selected.
                entity.DemandState = entity.ExecutionType == WorkflowType.ProceedLocallyL1 ? DemandState.ItHeadApproval : DemandState.DemandGate1;
                _db.SaveChanges();
            }

            // If just submitted to PMO for review, send notification.
            if (submittedForReview)
            {
                _log.Trace($"[{entity.Name}]: IT Demand Submitted for PMO Review.");
                NotifySubmittedToPmo(entity);
            }

            // If just submitted to PMO for review, send notification.
            if (correctionsRequested)
            {
                _log.Trace($"[{entity.Name}]: IT Demand returned to submitter for additional information.");
                NotifyCorrectionRequest(entity);
            }

            return entity.Id;
        }

        public void CancelRequest(int demandId)
        {
            var demand = _db.DemandRequests.SingleOrDefault(x => x.Id == demandId);
            if (demand == null) return; // feedback error to user

            var isWorkflowEnabled = demand.ExecutionType != null;

            demand.CancelledById = _user.Id;
            demand.CancelledOn = DateTimeOffset.Now;
            demand.DemandState = DemandState.Cancelled;
            demand.ProjectPhase = "Cancelled";

            // Clear any flags, timestamps, and user stamps.
            demand.SubmittedForReview = false;
            demand.SubmittedForReviewDate = null;
            demand.SubmittedById = null;
            demand.SubmittedBy = null;
            demand.ExecutionType = null;
            demand.RequestCorrectionsById = null;
            demand.RequestCorrectionsBy = null;
            demand.RequestCorrectionsDate = null;
            demand.PmoReviewById = null;
            demand.PmoReviewBy = null;
            demand.PmoReviewedOnDate = null;

            _db.SaveChanges();

            _log.Info($"{demand.Name} ({demand.Id}) was cancelled by {_user.DisplayName}.");

            // Remove any project artifacts created. These will be recreated when/if the demand is restarted.
            if (isWorkflowEnabled)
            {
                _db.CancelDemand(demand.Id, (model) => _log.Info($"{demand.Name} ({demand.Id}) workflow artifacts have been deleted."));
            }
        }

        public void ReinstateRequest(int demandId)
        {
            var demand = _db.DemandRequests.SingleOrDefault(x => x.Id == demandId);
            if (demand == null) return; // feedback error to user

            demand.CancelledBy = null;
            demand.CancelledById = null;
            demand.CancelledOn = null;
            demand.DemandState = DemandState.InitialEntry;
            _db.SaveChanges();

            _log.Info($"[{demand.Name}]: reinstated by {_user.DisplayName}.");
        }

        public UserViewModel GetItHeadForBusinessUnit(int businessUnitId)
        {
            var bu = _db.BusinessUnits.Include(x => x.ItHead).SingleOrDefault(x => x.Id == businessUnitId);
            return _mapper.Map<UserViewModel>(bu?.ItHead ?? User.Default);
        }

		private DemandRequestViewModel GetNewDemandRequest()
		{
			var demandViewModel = new DemandRequestViewModel();		
			FillInSelectOptions(demandViewModel);

            // Prepopulate the Owner and PM fields with the current user.
            demandViewModel.RequestOwner = _user;
            demandViewModel.ProjectManager = _user;

			return demandViewModel;
		}

		private DemandRequest? GetDemandRequest(int id)
		{
			var demand = _db.DemandRequests
                .Include(x => x.AffectedBusinessUnits)
                .Include(x => x.ComplianceRelevant)
				.Include(x => x.RequestOwner)
                .Include(x => x.RequestSponsor)
                .Include(x => x.ProjectManager)
                .Include(x => x.ItHead)
                .Include(x => x.SubmittedBy)
                .Include(x => x.RequestCorrectionsBy)
                .Include(x => x.PmoReviewBy)
                .Include(x => x.AssignedSme)
                .Include(x => x.Attachments).ThenInclude(x => x.CreatedBy)
                .Include(x => x.CancelledBy)
                .Include(x => x.CreatedBy)
                .Include(x => x.ModifiedBy)
                .SingleOrDefault(p => p.Id == id);

			return demand;
		}

        private DemandRequestViewModel? GetExistingDemandRequest(int id)
        {
            var demand = GetDemandRequest(id);

            if (demand == null) return null;

            // map entity to view model
            var demandViewModel = _mapper.Map<DemandRequestViewModel>(demand);

            demandViewModel.IsPmo = _user.IsPmo();
            demandViewModel.IsBusinessConsulting = _user.IsBusinessConsulting();

            FillInSelectOptions(demandViewModel);

            return demandViewModel;
        }

        private void FillInSelectOptions(DemandRequestViewModel vm)
		{
            // getting a SelectListViewModel, transform to Mvc.SelectListItem

            // fill in select list options
            var selectOptionsService = new SelectOptionsService(_db, _mapper);

            var businessUnitOptions = selectOptionsService.GetBusinessUnitOptions().ToArray();
            vm.BusinessUnits = _mapper.Map<SelectOptionViewModel[], SelectListItem[]>(businessUnitOptions);

            var processAreaOptions = selectOptionsService.GetProcessAreaOptions().ToArray();
            vm.ProcessAreas = _mapper.Map<SelectOptionViewModel[], SelectListItem[]>(processAreaOptions);

            var usersImpactedOptions = selectOptionsService.GetNumberOfUsersImpactedOptions().ToArray();
            vm.UsersImpacted = _mapper.Map<SelectOptionViewModel[], SelectListItem[]>(usersImpactedOptions);

            var businessDriverOptions = selectOptionsService.GetBusinessDriverOptions().ToArray();
            vm.BusinessDrivers = _mapper.Map<SelectOptionViewModel[], SelectListItem[]>(businessDriverOptions);

            var applicationTypeOptions = selectOptionsService.GetApplicationTypeOptions().ToArray();
            vm.ApplicationTypes = _mapper.Map<SelectOptionViewModel[], SelectListItem[]>(applicationTypeOptions);

            var complianceItemOptions = selectOptionsService.GetComplianceItemOptions().ToArray();
            vm.ComplianceItems = _mapper.Map<SelectOptionViewModel[], SelectListItem[]>(complianceItemOptions);

            var dcuOptions = selectOptionsService.GetDcuOptions().ToArray();
            // do map manually to assign the description field for the select list text field
            vm.DCUs = dcuOptions.Select(x => new SelectListItem { Text = x.Description, Value = x.Id.ToString() }).ToArray();

            var countryOptions = selectOptionsService.GetCountryOptions().OrderBy(x => x.Name).ToArray();
            vm.Countries = _mapper.Map<SelectOptionViewModel[], SelectListItem[]>(countryOptions);

            var businessProcessL1Options = selectOptionsService.GetBusinessProcessL1Options().OrderBy(x => x.Name).ToArray();
            vm.BusinessProcessL1s = _mapper.Map<SelectOptionViewModel[], SelectListItem[]>(businessProcessL1Options);

			// L2 & L3 are only needed if there are selections already made
			if (vm.BusinessProcessL2Id != null)
			{
                var businessProcessL2Options = selectOptionsService.GetBusinessProcessL2Options(vm.BusinessProcessL1Id).OrderBy(x => x.Name).ToArray();
                vm.BusinessProcessL2s = _mapper.Map<SelectOptionViewModel[], SelectListItem[]>(businessProcessL2Options);
            }

            if (vm.BusinessProcessL3Id != null)
            {
                var businessProcessL3Options = selectOptionsService.GetBusinessProcessL3Options(vm.BusinessProcessL2Id).OrderBy(x => x.Name).ToArray();
                vm.BusinessProcessL3s = _mapper.Map<SelectOptionViewModel[], SelectListItem[]>(businessProcessL3Options);
            }

            var itPlatformOptions = selectOptionsService.GetItPlatformOptions().OrderBy(x => x.Name).ToArray();
            vm.ItPlatforms = _mapper.Map<SelectOptionViewModel[], SelectListItem[]>(itPlatformOptions);

            var itSegmentOptions = selectOptionsService.GetItSegmentOptions().OrderBy(x => x.Name).ToArray();
            vm.ItSegments = _mapper.Map<SelectOptionViewModel[], SelectListItem[]>(itSegmentOptions);
        }

        private void SyncUserFields(DemandRequestViewModel request, DemandRequest entity)
        {
            var userService = new UserService(_db, _mapper);

            // Sync Request Owner Field
            if (request.RequestOwner == null)
            {
                entity.RequestOwnerId = null;
                entity.RequestOwner = null;
            }
            else
            {
                if (string.IsNullOrEmpty(request.RequestOwner.UserName))
                {
                    entity.RequestOwnerId = null;
                    entity.RequestOwner = null;
                }
                else if (request.RequestOwner.UserName.ToLower() != (entity.RequestOwner?.UserName ?? string.Empty).ToLower())
                {
                    var requestOwner = userService.VerifyOrCreateUser(request.RequestOwner);
                    if (requestOwner == null)
                    {
                        entity.RequestOwnerId = null;
                        entity.RequestOwner = null;
                    }
                    else
                    {
                        entity.RequestOwnerId = requestOwner.Id;
                        entity.RequestOwner = requestOwner;
                    }                    
                }
            }

            // Sync Request Sponsor Field
            if (request.RequestSponsor == null)
            {
                entity.RequestSponsorId = null;
                entity.RequestSponsor = null;
            }
            else
            {
                if (string.IsNullOrEmpty(request.RequestSponsor.UserName))
                {
                    entity.RequestSponsorId = null;
                    entity.RequestSponsor = null;
                }
                else if (request.RequestSponsor.UserName.ToLower() != (entity.RequestSponsor?.UserName ?? string.Empty).ToLower())
                {
                    var requestSponsor = userService.VerifyOrCreateUser(request.RequestSponsor);
                    if (requestSponsor == null)
                    {
                        entity.RequestSponsorId = null;
                        entity.RequestSponsor = null;
                    }
                    else
                    {
                        entity.RequestSponsorId = requestSponsor.Id;
                        entity.RequestSponsor = requestSponsor;
                    }
                }
            }

            // Sync Project Manager Field
            if (request.ProjectManager == null)
            {
                entity.ProjectManagerId = null;
                entity.ProjectManager = null;
            }
            else
            {
                if (string.IsNullOrEmpty(request.ProjectManager.UserName))
                {
                    entity.ProjectManagerId = null;
                    entity.ProjectManager = null;
                }
                else if (request.ProjectManager.UserName.ToLower() != (entity.ProjectManager?.UserName ?? string.Empty).ToLower())
                {
                    var projectManager = userService.VerifyOrCreateUser(request.ProjectManager);
                    if (projectManager == null)
                    {
                        entity.ProjectManagerId = null;
                        entity.ProjectManager = null;
                    }
                    else
                    {
                        entity.ProjectManagerId = projectManager.Id;
                        entity.ProjectManager = projectManager;
                    }
                }
            }

            // Sync It Head Field
            if (request.ItHead == null)
            {
                entity.ItHeadId = null;
                entity.ItHead = null;
            }
            else
            {
                if (string.IsNullOrEmpty(request.ItHead.UserName))
                {
                    entity.ItHeadId = null;
                    entity.ItHead = null;
                }
                else if (request.ItHead.UserName.ToLower() != (entity.ItHead?.UserName ?? string.Empty).ToLower())
                {
                    var itHead = userService.VerifyOrCreateUser(request.ItHead);
                    if (itHead == null)
                    {
                        entity.ItHeadId = null;
                        entity.ItHead = null;
                    }
                    else
                    {
                        entity.ItHeadId = itHead.Id;
                        entity.ItHead = itHead;
                    }
                }
            }

            // Sync Assigned Sme Field
            if (request.AssignedSme == null)
            {
                entity.AssignedSmeId = null;
                entity.AssignedSme = null;
            }
            else
            {
                if (string.IsNullOrEmpty(request.AssignedSme.UserName))
                {
                    entity.AssignedSmeId = null;
                    entity.AssignedSme = null;
                }
                else if (request.AssignedSme.UserName.ToLower() != (entity.AssignedSme?.UserName ?? string.Empty).ToLower())
                {
                    var assignedSme = userService.VerifyOrCreateUser(request.AssignedSme);
                    if (assignedSme == null)
                    {
                        entity.AssignedSmeId = null;
                        entity.AssignedSme = null;
                    }
                    else
                    {
                        entity.AssignedSmeId = assignedSme.Id;
                        entity.AssignedSme = assignedSme;
                    }
                }
            }
        }

        private void SyncAffectedBusinessUnits(DemandRequestViewModel request, DemandRequest entity)
        {
            // Remove any items that were deleted
            var postedItemIds = request.AffectedBusinessUnits ?? new List<int>();
            var existingSelections = _db.DemandRequestBusinessUnits.Where(x => x.DemandRequestId == request.Id).Select(x => x.BusinessUnitId).ToList();
            var toDelete = existingSelections.Where(x => !postedItemIds.Contains(x)).ToArray();
            if (toDelete.Any())
            {
                foreach (var businessUnitId in toDelete)
                {
                    var bu = _db.DemandRequestBusinessUnits.Single(x => x.DemandRequestId == entity.Id && x.BusinessUnitId == businessUnitId);
                    _db.DemandRequestBusinessUnits.Remove(bu);
                }
            }

            foreach (var id in postedItemIds)
            {
                var match = existingSelections.Contains(id);
                if (match) continue;
                var businessUnit = _db.BusinessUnits.SingleOrDefault(x => x.Id == id);
                if (businessUnit != null)
                    entity.AffectedBusinessUnits.Add(new DemandRequestBusinessUnit { DemandRequestId = entity.Id, BusinessUnitId = businessUnit.Id });
            }
        }

        private void SyncComplianceRelevant(DemandRequestViewModel request, DemandRequest entity)
        {
            // Remove any items that were deleted
            var postedItemIds = request.ComplianceRelevant ?? new List<int>();
            var existingSelections = _db.DemandRequestComplianceItems.Where(x => x.DemandRequestId == request.Id).Select(x => x.ComplianceItemId).ToList();
            var toDelete = existingSelections.Where(x => !postedItemIds.Contains(x)).ToArray();
            if (toDelete.Any())
            {
                foreach (var complianceItemId in toDelete)
                {
                    var bu = _db.DemandRequestComplianceItems.Single(x => x.DemandRequestId == entity.Id && x.ComplianceItemId == complianceItemId);
                    _db.DemandRequestComplianceItems.Remove(bu);
                }
            }

            foreach (var id in postedItemIds)
            {
                var match = existingSelections.Contains(id);
                if (match) continue;
                var complianceItem = _db.ComplianceItems.SingleOrDefault(x => x.Id == id);
                if (complianceItem != null)
                    entity.ComplianceRelevant.Add(new DemandRequestComplianceItem { DemandRequestId = entity.Id, ComplianceItemId = complianceItem.Id });
            }
        }

        private void SyncFileAttachments(DemandRequestViewModel request, DemandRequest entity)
        {
            // Remove any files that were deleted.
            var postedItems = request.Attachments ?? Array.Empty<AttachmentViewModel>();
            var postedIds = postedItems.Select(x => x.Id).ToArray();
            var toDelete = _db.Attachments.Where(x => !postedIds.Contains(x.Id)).ToArray() ?? Array.Empty<Attachment>();

            if (toDelete != null && toDelete.Length > 0)
            {
                _log.Trace($"Removing {toDelete.Length} Attachments.");

                foreach (var attachment in toDelete)
                {
                    _db.Attachments.Remove(attachment);
                }
            }

            if (postedItems.Length > 0)
            {
                var newAttachments = postedItems.Where(x => x.Id < 1).Count();
                if (newAttachments > 0) _log.Trace($"Adding {newAttachments} Attachments.");
            }

            // Add new file attachments.
            foreach (var attachment in postedItems)
            {
                if (attachment.Id >= 1 || attachment.File.Length <= 0) continue;
                try
                {
                    var file = attachment.File;
                    byte[] fileBytes = Array.Empty<byte>();

                    var userService = new UserService(_db, _mapper);

                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }

                    var model = new Attachment
                    {
                        //DemandRequestId = entity.Id,
                        ContentType = file.ContentType,
                        Size = file.Length,
                        FileName = Path.GetFileName(file.FileName),
                        Contents = fileBytes,
                        CreatedById = userService.VerifyOrCreateUser(_user)?.Id ?? null,
                        Created = DateTimeOffset.Now
                    };

                    entity.Attachments.Add(model);
                }
                catch (Exception ex)
                {
                    _log.Error(ex);
                }
            }
        }

        private void NotifySubmittedToPmo(DemandRequest entity)
        {
            var notifyService = new NotificationService(_log, _db);

            var pmoNotifyModel = new NotifySubmitForReviewModel
            {
                DemandId = entity.Id,
                DemandName = entity.Name,
                RequestOwner = _mapper.Map<UserViewModel>(entity.RequestOwner)
            };

            notifyService.NewDemandRequestSubmitted(pmoNotifyModel);
        }

        private void NotifyCorrectionRequest(DemandRequest entity)
        {
            var notifyService = new NotificationService(_log, _db);

            _db.Entry(entity).Reference(x => x.RequestCorrectionsBy).Load();

            var correctionRequestModel = new NotifyCorrectionRequestModel
            {
                DemandId = entity.Id,
                DemandName = entity.Name,
                RequestOwner = _mapper.Map<UserViewModel>(entity.RequestOwner),
                RequestedBy = _mapper.Map<UserViewModel>(entity.RequestCorrectionsBy),
                CorrectionComments = entity.RequestCorrectionsComments
            };

            notifyService.RequestCorrections(correctionRequestModel);
        }

    }
}