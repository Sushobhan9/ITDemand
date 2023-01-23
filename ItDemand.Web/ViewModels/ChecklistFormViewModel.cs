using ItDemand.Domain.Enums;

namespace ItDemand.Web.ViewModels
{
    public class ChecklistFormViewModel
    {
        public int Id { get; set; }
        public int DemandRequestId { get; set; }
        public StatusType Status { get; set; } = StatusType.New;
        public DateTime? MeetingDate { get; set; }
        public string AdditionalComments { get; set; } = string.Empty;
        public string ReviewComments { get; set; } = string.Empty;

        public ChecklistApproverViewModel[] Approvers { get; set; } = Array.Empty<ChecklistApproverViewModel>();
        public ChecklistQuestionViewModel[] Questions { get; set; } = Array.Empty<ChecklistQuestionViewModel>();
    }
}
