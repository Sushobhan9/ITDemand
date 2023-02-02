namespace ItDemand.Web.ViewModels
{
    public class MyApprovalsViewModel
    {
        public int DemandRequestId { get; set; }
        public string RequestName { get; set; } = string.Empty;
        public string ChecklistName { get; set; } = string.Empty;
        public int ChecklistId { get; set; }
        public DateTime? ApprovalDate { get; set; }
    }
}