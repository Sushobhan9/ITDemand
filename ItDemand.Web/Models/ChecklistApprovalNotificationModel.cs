using ItDemand.Domain.Models;
using ItDemand.Web.ViewModels;

namespace ItDemand.Web.Models
{
    public class ChecklistApprovalNotificationModel
    {
        public int ChecklistId { get; set; }
        public string ChecklistTitle { get; set; } = string.Empty;
        public int DemandId { get; set; }
        public string DemandName { get; set; } = string.Empty;
        public string Comments { get; set; } = string.Empty;

        public string ChecklistRelativePath => "/Project/Index/" + DemandId + "#checklist=" + ChecklistId;

        public UserViewModel[] Approvers { get; set; } = Array.Empty<UserViewModel>();

        public UserViewModel SentBy { get; set; } = null!;
    }
}
