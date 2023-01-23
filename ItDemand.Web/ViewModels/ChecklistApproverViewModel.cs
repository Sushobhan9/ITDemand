using ItDemand.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ItDemand.Web.ViewModels
{
    public class ChecklistApproverViewModel
    {
        public int Id { get; set; }
        public int ChecklistId { get; set; }
        public SecurityRole Role { get; set; }
        public ApproverType Type { get; set; }
        public bool Required { get; set; }
        public int? SortIndex { get; set; }

        public int? ApproverId { get; set; }
        public UserViewModel? Approver { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public IEnumerable<SelectListItem> ApproverList { get; set; } = Enumerable.Empty<SelectListItem>();

        public UserViewModel CurrentUser { get; set; } = null!; // we need the current user to check to see if they are the same as the approver selected
    }
}
