using ItDemand.Domain.Enums;

namespace ItDemand.Web.ViewModels
{
    public class ChecklistApproverViewModel
    {
        public int Id { get; set; }
        public int ChecklistId { get; set; }
        public ApproverRole Role { get; set; }
        public ApproverType Type { get; set; }
        public bool Required { get; set; }
        public int? SortIndex { get; set; }

        public int? ApproverId { get; set; }
        public UserViewModel? Approver { get; set; }

        public DateTime? ApprovalDate { get; set; }
    }
}
