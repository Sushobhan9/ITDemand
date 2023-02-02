namespace ItDemand.Web.ViewModels
{
    public class MyDemandsViewModel
    {
        public int Id { get; set; }
        public string RequestName { get; set; } = string.Empty;
        public string RequestOwner { get; set; } = string.Empty;
        public string RequestSponsor { get; set; } = string.Empty;
        public string ProjectManager { get; set; } = string.Empty;
        public string ExecutionType { get; set; } = string.Empty;
        public string DemandState { get; set; } = string.Empty;
        public DateTimeOffset CreatedDate { get; set; }
    }
}
