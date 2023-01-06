using ItDemand.Web.ViewModels;

namespace ItDemand.Web.Models
{
    public class NotifySubmitForReviewModel
    {
        public int DemandId { get; set; }
        public string DemandName { get; set; } = string.Empty;
        public UserViewModel RequestOwner { get; set; } = null!;
    }
}
