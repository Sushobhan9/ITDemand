using ItDemand.Domain.Enums;

namespace ItDemand.Web.Models
{
    public enum NotificationType
    {
        None = 0,
        ItsNewDemandRequest = 1,
        ItsRedCabReview = 2,
        ItsItHeadInitialReview = 3,
        ItsItHeadApproval = 4,
        ItsSolutionDesignReview = 5,
        ItsSecurityComplianceReview = 6,
        ItsArchitectureBoardReview = 7,
        ItsDemandGate2Review = 8,
        ApprovalRequest = 9,
        ItHeadApprovalComplete = 10
    }

    public class ChecklistNotificationModel
    {
        public int DemandId { get; set; }
        public string DemandName { get; set; } = string.Empty;
        public string RequestOwnerEmail { get; set; } = string.Empty;
        public string RequestSponsorEmail { get; set; } = string.Empty;
        public string ProjectManagerEmail { get; set; } = string.Empty;
        public string ItHeadEmail { get; set; } = string.Empty;
        public string PmoReviewerEmail { get; set; } = string.Empty;
        public string CorrectionRequestComments { get; set; } = string.Empty;
        public int ChecklistId { get; set; }
        public NotificationType NotificationType { get; set; }
        public StatusType ApprovalStatus { get; set; }
    }
}
