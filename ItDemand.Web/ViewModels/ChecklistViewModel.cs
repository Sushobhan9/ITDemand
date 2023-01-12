using ItDemand.Domain.Enums;

namespace ItDemand.Web.ViewModels
{
    public class ChecklistViewModel
    {
        public int Id { get; set; }
        public int DemandRequestId { get; set; }
        public int? ChecklistTemplateId { get; set; }
        
        public string Name { get; set; } = string.Empty;
        public StatusType Status { get; set; } = StatusType.New;
        public double SequenceNumber { get; set; }
        public DateTime? MeetingDate { get; set; }

        //public int? WorkflowItemId { get; set; }        

        public string GateReviewDescription { get; set; } = string.Empty;
        public string AdditionalComments { get; set; } = string.Empty;

        public string ReviewComments { get; set; } = string.Empty;

        public ChecklistApproverViewModel[] Approvers { get; set; } = Array.Empty<ChecklistApproverViewModel>();
        public ChecklistQuestionViewModel[] Questions { get; set; } = Array.Empty<ChecklistQuestionViewModel>();
    }
}