using ItDemand.Domain.DataContext;
using ItDemand.Domain.Enums;
using ItDemand.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace ItDemand.Web.ViewModels
{
    public class ProjectWorkflowViewModel
    {
        private readonly Workflow _workflow;
        private readonly DemandRequest _demand;

        public ProjectWorkflowViewModel(ItDemandContext db, int id)
        {
            _demand = db.DemandRequests
                .Include(x => x.Checklists)
                .SingleOrDefault(x => x.Id == id) ?? new DemandRequest();

            _workflow = db.Workflows
                .Include(x => x.Items)
                .FirstOrDefault(x => x.Id == _demand.ExecutionType) ?? new Workflow { Id = 0, Name = "Unknown", Items = Array.Empty<WorkflowItem>() };
        }

        public double GetCurrentStage(List<WorkflowItem> workflowItems)
        {
            var documents = new List<Checklist>(_demand.Checklists);
            
            double stage = 0;
            foreach (var workflowItem in workflowItems)
            {
                stage = workflowItem.SequenceNumber;

                var workflowDocuments = documents
                    .Where(x => x.WorkflowItemId == workflowItem.Id)
                    .OrderBy(x => x.SequenceNumber)
                    .ToArray();

                //if everything is approved then keep moving
                if (workflowDocuments.All(x => x.Status == StatusType.Approved))
                    continue;

                //if approved or waiting approval see if you can move it to the process block for that stage, this means management is reviewing
                //and its almost ready to move to the next stage
                if (workflowDocuments.All(x => x.Status == StatusType.Approved || x.Status == StatusType.WaitingApproval))
                {
                    var item = workflowItem;
                    if (workflowItem.WorkflowItemType != WorkflowItemType.Process) continue;
                    var decisionBlock = workflowItems.Where(x => x.SequenceNumber > item.SequenceNumber).OrderBy(x => x.SequenceNumber).FirstOrDefault();
                    if (decisionBlock == null) continue;
                    stage = decisionBlock.SequenceNumber;
                }

                break;
            }

            return stage;
        }

        List<WorkflowItem> FindChildren(WorkflowItem? parent = null, List<WorkflowItem>? list = null)
        {
            var query = (parent == null
                ? _workflow.Items.Where(x => x.ParentId == null)
                : _workflow.Items.Where(x => x.ParentId == parent.Id)).OrderBy(x => x.SequenceNumber).ToArray();

            list ??= new List<WorkflowItem>();
            foreach (var item in query)
            {
                list.Add(item);
                FindChildren(item, list);
            }

            return list;
        }

        public object FindWorkflowData()
        {
            var workflowItems = FindChildren();

            var stages = new List<object>();

            foreach (var item in workflowItems)
            {
                var htmlDescription = HttpUtility.HtmlEncode(item.Description);
                var stage = new { stage = item.SequenceNumber, shape = item.WorkflowItemType.ToString().ToLower(), title = item.Name, description = htmlDescription };
                stages.Add(stage);
            }

            return new
            {
                currentStage = GetCurrentStage(workflowItems),
                stages = stages.ToArray()
            };
        }
    }
}
