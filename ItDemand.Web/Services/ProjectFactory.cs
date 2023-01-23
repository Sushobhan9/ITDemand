using ItDemand.Domain.DataContext;
using ItDemand.Domain.Models;
using Linde.LoggingCore;
using Microsoft.EntityFrameworkCore;

namespace ItDemand.Web.Services
{
    public class ProjectFactory
    {
        private readonly ApplicationLog _log;
        private readonly ItDemandContext _db;

        public ProjectFactory(ApplicationLog log, ItDemandContext dbContext)
        {
            _db = dbContext;
            _log = log;
        }

        public void CreateWorkItems(DemandRequest demand, bool itsCabReview = false)
        {
            // Find all the workflows that apply to this demand.
            var workflows = _db.Workflows
                .Include(x => x.Items).ThenInclude(x => x.ChecklistTemplates)
                .Where(x => x.Id == demand.ExecutionType)
                .ToArray();

            // For each workflow, create the items that you will need to finish each stage.            
            foreach (var workflow in workflows)
            {
                foreach (var workflowItem in workflow.Items)
                {
                    // Not sure best way to do this, so hack it in here for now.
                    // IT Fast Track (L3) workflows have an optional gate (CAB Review)
                    // that is determined by a question on the Gate 1 Review checklist.
                    if (itsCabReview == false &&
                        workflowItem.Name.StartsWith("CAB"))
                    {
                        continue;
                    }

                    var templateIds =
                        workflowItem.ChecklistTemplates
                            .Select(x => x.Id)
                            .ToArray();

                    foreach (var id in templateIds)
                    {
                        if (id < 1)
                        {
                            throw new ArgumentException($"Checklist Template Id to add is invalid. Id: [{id}]");
                        }

                        var template =
                            _db.ChecklistTemplates
                                .Include(x => x.Approvers)
                                .Include(x => x.Questions)
                                .SingleOrDefault(x => x.Id == id);

                        if (template == null)
                        {
                            _log.Warn($"Unable to find Checklist Template with Id: [{id}]");
                            continue;
                        }

                        // Using the template for this type of checklist, create a new
                        // one and insert it in sequence.
                        var checklist = new Checklist
                        {
                            Name = template.Name,
                            GateReviewDescription = template.GateReviewDescription,
                            WorkflowItem = template.WorkflowItem,
                            WorkflowItemId = template.WorkflowItemId,
                            ChecklistTemplateId = template.Id,
                            SequenceNumber = GetNextSequenceNumber(template.WorkflowItem, demand.Id), // Assign the next available sequence number.
                            Questions = template.Questions
                                .Select(q => new ChecklistQuestion
                                {
                                    Path = q.Path,
                                    Text = q.Text,
                                    HelpText = q.HelpText,
                                    QuestionType = q.QuestionType,
                                    AcceptedAnswers = q.AcceptedAnswers,
                                    CustomChoices = q.CustomChoices
                                }).ToList(),
                            Approvers = template.Approvers
                                .Select(a => new ChecklistApprover
                                {
                                    Role = a.Role,
                                    Type = a.Type,
                                    Required = a.Required,
                                    SortIndex = a.SortIndex
                                }).ToList()
                        };

                        demand.Checklists.Add(checklist);
                        _db.SaveChanges();
                    }
                }
            }
        }

        /// <summary>
        /// Find and return the next available sequence number for a checklist within a Workflow Item.
        /// </summary>
        /// <param name="workflowItem"></param>
        /// <param name="demandId"></param>
        /// <returns></returns>
        private double GetNextSequenceNumber(WorkflowItem workflowItem, int demandId)
        {
            double sequenceNumber;

            if (demandId < 1)
            {
                sequenceNumber = workflowItem.SequenceNumber + .001;
                return sequenceNumber;
            }

            sequenceNumber = FindMaxSequenceNumber(workflowItem.Id, demandId);

            if (!(sequenceNumber < 1)) return sequenceNumber + .001;
            sequenceNumber = workflowItem.SequenceNumber + .001;
            return sequenceNumber;
        }

        /// <summary>
        /// Find the highest sequence number assigned to a workflow item for a 
        /// specific project.
        /// </summary>
        /// <param name="workflowItemId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public double FindMaxSequenceNumber(int? workflowItemId, int projectId)
        {
            if (workflowItemId == null)
                throw new NullReferenceException();

            var query =
                (from c in _db.Checklists
                 where c.WorkflowItemId == workflowItemId && c.DemandRequestId == projectId
                 select new { c.SequenceNumber })
                .OrderByDescending(u => u.SequenceNumber).ToList();

            var item = query.FirstOrDefault();

            return item?.SequenceNumber ?? -1;
        }
    }
}
