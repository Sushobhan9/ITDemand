namespace ItDemand.Web.ViewModels
{
    public class AttachmentViewModel
    {
        public int Id { get; set; }
        public int DemandRequestId { get; set; }
        public string ContentType { get; set; } = string.Empty;
        public long Size { get; set; }
        public string FileName { get; set; } = string.Empty;
        public IFormFile File { get; set; } = null!;      
        public int CreatedById { get; set; }
        public UserViewModel CreatedBy { get; set; } = null!;
        public DateTimeOffset Created { get; set; }
    }
}
