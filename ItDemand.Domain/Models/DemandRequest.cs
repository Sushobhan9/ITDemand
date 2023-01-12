using ItDemand.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace ItDemand.Domain.Models
{
	public class DemandRequest
	{
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string ProblemStatement { get; set; } = string.Empty;

        public int CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; } = null!;

        public DateTimeOffset CreatedDate { get; set; }

        public int? BusinessUnitId { get; set; }
        [ForeignKey("BusinessUnitId")]
        public virtual BusinessUnit? BusinessUnit { get; set; }

        // The options chosen from the multiple select control in the UI.
        public virtual ICollection<DemandRequestBusinessUnit> AffectedBusinessUnits { get; set; } = new HashSet<DemandRequestBusinessUnit>();

        public int? ProcessAreaId { get; set; }
        [ForeignKey("ProcessAreaId")]
        public virtual ProcessArea? ProcessArea { get; set; }

        public int? UsersImpactedId { get; set; }
        [ForeignKey("UsersImpactedId")]
        public virtual UsersImpacted? UsersImpacted { get; set; }

        public int? BusinessDriverId { get; set; }
        [ForeignKey("BusinessDriverId")]
        public virtual BusinessDriver? BusinessDriver { get; set; }

        public string BenefitsPerYear { get; set; } = string.Empty;
        public string BenefitDuration { get; set; } = string.Empty;
        public DateTimeOffset? BenefitStart { get; set; }
        
        public string BusinessBenefit { get; set; } = string.Empty;
        public string ConsequenceNotImplemented { get; set; } = string.Empty;
        public string ScopeSummary { get; set; } = string.Empty;
        public string Comments { get; set; } = string.Empty;

        public int? RequestOwnerId { get; set; }
        [ForeignKey("RequestOwnerId")]
        public virtual User? RequestOwner { get; set; }

        public int? RequestSponsorId { get; set; }
        [ForeignKey("RequestSponsorId")]
        public virtual User? RequestSponsor { get; set; }

        public int? ProjectManagerId { get; set; }
        [ForeignKey("ProjectManagerId")]
        public virtual User? ProjectManager { get; set; }

        public int EstimatedInternalEffort { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public string EstimatedTotalCost { get; set; } = null!; // select list maintained in code

        public int? ApplicationTypeId { get; set; }
        [ForeignKey("ApplicationTypeId")]
        public virtual ApplicationType? ApplicationType { get; set; }

        public string ApplicationDescription { get; set; } = string.Empty; // LocalApplicationDescription
        public string IsRedCab { get; set; } = string.Empty; // Yes/No
        public string TechnicalScope { get; set; } = string.Empty;
        public string InterfaceChanges { get; set; } = string.Empty;
        public string SecurityAssessment { get; set; } = string.Empty;

        public virtual ICollection<DemandRequestComplianceItem> ComplianceRelevant { get; set; } = new HashSet<DemandRequestComplianceItem>(); // The options chosen from the multiple select control in the UI.
        public string MedicalValidationReference { get; set; } = string.Empty;

        public int? ItHeadId { get; set; }
        [ForeignKey("ItHeadId")]
        public virtual User? ItHead { get; set; }

        public bool SubmittedForReview { get; set; }
        public DateTimeOffset? SubmittedForReviewDate { get; set; }

        public int? SubmittedById { get; set; }
        [ForeignKey("SubmittedById")]
        public virtual User? SubmittedBy { get; set; }

        public DemandState DemandState { get; set; } = DemandState.InitialEntry;

        public WorkflowType? ExecutionType { get; set; }
        [ForeignKey("ExecutionType")]
        public virtual Workflow? Workflows { get; set; }

        // PMO Review
        //

        public bool RequestCorrections { get; set; }
        public string RequestCorrectionsComments { get; set; } = string.Empty;
        public DateTimeOffset? RequestCorrectionsDate { get; set; }

        public int? RequestCorrectionsById { get; set; }
        [ForeignKey("RequestCorrectionsById")]
        public virtual User? RequestCorrectionsBy { get; set; }

        public int? PmoReviewById { get; set; }
        [ForeignKey("PmoReviewById")]
        public virtual User? PmoReviewBy { get; set; }

        public DateTimeOffset? PmoReviewedOnDate { get; set; }
        public string PmoReviewComments { get; set; } = string.Empty;

        public string NewApplication { get; set; } = string.Empty; // Yes/No

        public int? ProposedPlatformId { get; set; }
        [ForeignKey("ProposedPlatformId")]
        public virtual ItPlatform? ProposedPlatform { get; set; }

        public string DecommissionRequired { get; set; } = string.Empty; // Yes/No

        public int? AssignedSmeId { get; set; }
        [ForeignKey("AssignedSmeId")]
        public virtual User? AssignedSme { get; set; }

        public int? ItSegmentId { get; set; }
        [ForeignKey("ItSegmentId")]
        public virtual ItSegment? ItSegment { get; set; }

        // Project Info
        //

        public string ArchitectureRelevant { get; set; } = string.Empty; // select list maintained in code (Yes/No - change to bit?)
        public string PowerSteeringId { get; set; } = string.Empty;
        
        public int? DcuId { get; set; }
        [ForeignKey("DcuId")]
        public virtual DCU? Dcu { get; set; }

        public int? CountryId { get; set; }
        [ForeignKey("CountryId")]
        public virtual Country? Country { get; set; }

        public string KeyProject { get; set; } = string.Empty;  // select list maintained in code (Yes/No - change to bit?)
        public string FTEsAssigned { get; set; } = string.Empty;
        public string ExecutiveShortDescription { get; set; } = string.Empty;

        public int? BusinessProcessL1Id { get; set; }
        [ForeignKey("BusinessProcessL1Id")]
        public virtual BusinessProcessL1? BusinessProcessL1 { get; set; }

        public int? BusinessProcessL2Id { get; set; }
        [ForeignKey("BusinessProcessL2Id")]
        public virtual BusinessProcessL2? BusinessProcessL2 { get; set; }

        public int? BusinessProcessL3Id { get; set; }
        [ForeignKey("BusinessProcessL3Id")]
        public virtual BusinessProcessL3? BusinessProcessL3 { get; set; }

        public string Methodology { get; set; } = string.Empty; // select list with just two options - maintain list in code
        public string Digital { get; set; } = string.Empty; // select list maintained in code (Yes/No)
        public string Replicated { get; set; } = string.Empty; // select list maintained in code (Yes/No)
        public string ProjectRepositoryLink { get; set; } = string.Empty;

        // Project Status
        //

        public string ProjectPhase { get; set; } = string.Empty; // select list maintained in the view code
        public string OverallStatus { get; set; } = string.Empty; // select list maintained in code (Green/Yellow/Red)
        public string Budget { get; set; } = string.Empty; // select list maintained in code (Green/Yellow/Red)
        public string Time { get; set; } = string.Empty; // select list maintained in code (Green/Yellow/Red)
        public string Scope { get; set; } = string.Empty; // select list maintained in code (Green/Yellow/Red)

        public string BaselineCapex { get; set; } = string.Empty; // should these be decimal?
        public string BaselineOpex { get; set; } = string.Empty; // should these be decimal?
        public string CostItPerYear { get; set; } = string.Empty;  // decimal?
        public string Payback { get; set; } = string.Empty;  // decimal?
        public string ActualCapex { get; set; } = string.Empty; // should these be decimal?
        public string ActualOpex { get; set; } = string.Empty; // should these be decimal?
        public string EacCapex { get; set; } = string.Empty; // should these be decimal?
        public string EacOpex { get; set; } = string.Empty; // should these be decimal?
        public string ActualCapexCurrentYear { get; set; } = string.Empty; // should these be decimal?
        public string ActualOpexCurrentYear { get; set; } = string.Empty; // should these be decimal?
        public string EacCapexCurrentYear { get; set; } = string.Empty; // should these be decimal?
        public string EacOpexCurrentYear { get; set; } = string.Empty; // should these be decimal?

        public string KeyMessages { get; set; } = string.Empty;
        public string TopIssues { get; set; } = string.Empty;
        public string TopRisks { get; set; } = string.Empty;
        public string AccomplishedLastPeriod { get; set; } = string.Empty;
        public string PlannedNextPeriod { get; set; } = string.Empty;
        public string KeyMilestones { get; set; } = string.Empty;
        public DateTimeOffset? MilestonePlanDate { get; set; }
        public DateTimeOffset? MilestoneActualDate { get; set; }

        public int? CancelledById { get; set; }
        [ForeignKey("CancelledById")]
        public virtual User? CancelledBy { get; set; }

        public DateTimeOffset? CancelledOn { get; set; }
        
        public int? ModifiedById { get; set; }
        [ForeignKey("ModifiedById")]
        public virtual User? ModifiedBy { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        public virtual ICollection<Checklist> Checklists { get; set; } = new HashSet<Checklist>();
        public virtual ICollection<Attachment> Attachments { get; set; } = new HashSet<Attachment>();
    }
}