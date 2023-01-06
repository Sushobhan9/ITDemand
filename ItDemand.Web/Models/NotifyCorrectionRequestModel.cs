using ItDemand.Web.ViewModels;

namespace ItDemand.Web.Models
{
    public class NotifyCorrectionRequestModel
    {
        public int DemandId { get; set; }
        public string DemandName { get; set; } = string.Empty;
        public UserViewModel RequestOwner { get; set; } = null!;
        public UserViewModel RequestedBy { get; set; } = null!;
        public string CorrectionComments { get; set; } = string.Empty;
    }
}
