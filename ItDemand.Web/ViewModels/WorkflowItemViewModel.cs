using ItDemand.Domain.Enums;

namespace ItDemand.Web.ViewModels
{
    public class WorkflowItemViewModel
    {
        public int Id { get; set; }
        public WorkflowType WorkflowId { get; set; }
        public WorkflowItemType WorkflowItemType { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Stage { get; set; }
        public double SequenceNumber { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
