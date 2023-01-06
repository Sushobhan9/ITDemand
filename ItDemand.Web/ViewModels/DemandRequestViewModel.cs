using ItDemand.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ItDemand.Web.ViewModels
{
	public class DemandRequestViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string ProblemStatement { get; set; } = string.Empty;
		public int? BusinessUnitId { get; set; }
		public IEnumerable<int> AffectedBusinessUnits { get; set; } = null!; // multiple selection value field
		public int? ProcessAreaId { get; set; }
		public int? UsersImpactedId { get; set; }
		public int? BusinessDriverId { get; set; }
		public string BenefitsPerYear { get; set; } = string.Empty; // should this be a decimal?
		public string BenefitDuration { get; set; } = string.Empty;
		public DateTimeOffset? BenefitStart { get; set; }
		public string BusinessBenefit { get; set; } = string.Empty;
		public string ConsequenceNotImplemented { get; set; } = string.Empty;
		public string ScopeSummary { get; set; } = string.Empty;
		public string Comments { get; set; } = string.Empty;

		//public int? RequestOwnerId { get; set; }
		public UserViewModel? RequestOwner { get; set; }

		//public int? RequestSponsorId { get; set; }
		public UserViewModel? RequestSponsor { get; set; }

		//public int? ProjectManagerId { get; set; }
		public UserViewModel? ProjectManager { get; set; }

		public int EstimatedInternalEffort { get; set; }
		public DateTimeOffset? StartDate { get; set; }
		public DateTimeOffset? EndDate { get; set; }
		public string EstimatedTotalCost { get; set; } = string.Empty;

		public int? ApplicationTypeId { get; set; }
		public string ApplicationDescription { get; set; } = string.Empty; // LocalApplicationDescription
		public string IsRedCab { get; set; } = "No"; // Yes/No select
		public string TechnicalScope { get; set; } = string.Empty;
		public string InterfaceChanges { get; set; } = string.Empty;
        public string SecurityAssessment { get; set; } = string.Empty;
		public IEnumerable<int> ComplianceRelevant { get; set; } = null!;
		public string MedicalValidationReference { get; set; } = string.Empty;

        //public int? ItHeadId { get; set; }
        public virtual UserViewModel? ItHead { get; set; }

        public int? CreatedById { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

        public bool SubmittedForReview { get; set; }
		public DateTimeOffset? SubmittedForReviewDate { get; set; }
		public int? SubmittedById { get; set; }
		public UserViewModel? SubmittedBy { get; set; }

		public DemandState DemandState { get; set; } = DemandState.InitialEntry;

		#region PMO Review
		public bool RequestCorrections { get; set; }
		public string RequestCorrectionsComments { get; set; } = string.Empty;
		public DateTimeOffset? RequestCorrectionsDate { get; set; }
		public int? RequestCorrectionsById { get; set; }
		//public virtual UserViewModel? RequestCorrectionsBy { get; set; }
        public virtual string RequestCorrectionsByDisplayName { get; set; } = string.Empty;

        public int? ExecutionType { get; set; }
        public DateTimeOffset? PmoReviewedOnDate { get; set; }
		public string PmoReviewComments { get; set; } = string.Empty;

		public string NewApplication { get; set; } = string.Empty; // Yes/No select
        public int? ProposedPlatformId { get; set; }
		public string DecommissionRequired { get; set; } = string.Empty; // Yes/No select
        public UserViewModel? AssignedSme { get; set; }
		public int? ItSegmentId { get; set; }

        public DateTimeOffset? CancelledOn { get; set; }
        public virtual string CancelledByDisplayName { get; set; } = string.Empty;

        public AttachmentViewModel[] Attachments { get; set; } = Array.Empty<AttachmentViewModel>();

		#endregion

		#region Project Info
		public string ArchitectureRelevant { get; set; } = "No"; // Yes/No
		public string PowerSteeringId { get; set; } = string.Empty;
		public int? DcuId { get; set; }
		public int? CountryId { get; set; }
		public string KeyProject { get; set; } = "No"; // Yes/No
		public string FTEsAssigned { get; set; } = string.Empty;
		public string ExecutiveShortDescription { get; set; } = string.Empty;
		public int? BusinessProcessL1Id { get; set; }
		public int? BusinessProcessL2Id { get; set; }
		public int? BusinessProcessL3Id { get; set; }
		public string Methodology { get; set; } = string.Empty; // select list with just two options - maintain list in code
		public string Digital { get; set; } = "No"; // Yes/No
		public string Replicated { get; set; } = "No"; // Yes/No
		public string ProjectRepositoryLink { get; set; } = string.Empty;
		#endregion

		#region Project Status
		public string ProjectPhase { get; set; } = string.Empty; // select list maintained in the view code
		public string OverallStatus { get; set; } = string.Empty; // Green/Yellow/Red
		public string Budget { get; set; } = string.Empty; // Green/Yellow/Red
		public string Time { get; set; } = string.Empty; // Green/Yellow/Red
		public string Scope { get; set; } = string.Empty; // Green/Yellow/Red

		public string BaselineCapex { get; set; } = string.Empty; // should these be decimal?
		public string BaselineOpex { get; set; } = string.Empty; // should these be decimal?
		public string CostItPerYear { get; set; } = string.Empty;  // should these be decimal?
		public string Payback { get; set; } = string.Empty; // should these be decimal?
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
		#endregion


		#region SelectListOptions
		public SelectListItem[] ApplicationTypes { get; set; } = Array.Empty<SelectListItem>(); 
		public SelectListItem[] BusinessDrivers { get; set; } = Array.Empty<SelectListItem>();
		public SelectListItem[] BusinessProcessL1s { get; set; } = Array.Empty<SelectListItem>();
		public SelectListItem[] BusinessProcessL2s { get; set; } = Array.Empty<SelectListItem>();
		public SelectListItem[] BusinessProcessL3s { get; set; } = Array.Empty<SelectListItem>();
		public SelectListItem[] BusinessUnits { get; set; } = Array.Empty<SelectListItem>();
		public SelectListItem[] ComplianceItems { get; set; } = Array.Empty<SelectListItem>();
		public SelectListItem[] Countries { get; set; } = Array.Empty<SelectListItem>();
		public SelectListItem[] DCUs { get; set; } = Array.Empty<SelectListItem>();
		public SelectListItem[] ItPlatforms { get; set; } = Array.Empty<SelectListItem>();
		public SelectListItem[] ItSegments { get; set; } = Array.Empty<SelectListItem>();
        public SelectListItem[] ProcessAreas { get; set; } = Array.Empty<SelectListItem>();
		public SelectListItem[] UsersImpacted { get; set; } = Array.Empty<SelectListItem>();

		public IEnumerable<SelectListItem> BenefitDurations => new List<SelectListItem>
		{
			new SelectListItem {Value = "One Time", Text = "One Time"},
			new SelectListItem {Value = "Recurring", Text = "Recurring"}
		};

		public IEnumerable<SelectListItem> EstimatedTotalCosts => new List<SelectListItem>
		{
			new SelectListItem {Value = "< $30,000", Text = @"< $30,000"},
			new SelectListItem {Value = "$30,000 > AND <= $100,000", Text = @"$30,000 > AND <= $100,000"},
			new SelectListItem {Value = "$100,000 > AND <= $750,000", Text = @"$100,000 > AND <= $750,000"},
			new SelectListItem {Value = "> $750,000", Text = @"> $750,000"}
		};

		public IEnumerable<SelectListItem> GreenYellowRedOptions => new List<SelectListItem>
		{
			new SelectListItem {Value = "Green", Text = "Green"},
			new SelectListItem {Value = "Yellow", Text = "Yellow"},
			new SelectListItem {Value = "Red", Text = "Red"}
		};

		public IEnumerable<SelectListItem> Methodologies => new List<SelectListItem>
		{
			new SelectListItem {Value = "Agile", Text = "Agile"},
			new SelectListItem {Value = "Waterfall", Text = "Waterfall"}
		};

        public IEnumerable<Tuple<int, string>> PmoReviewOptions => new List<Tuple<int, string>>
        {
            new Tuple<int, string>((int)WorkflowType.ProceedLocallyL1, "Proceed Locally (L1)"),
            new Tuple<int, string>((int)WorkflowType.ItDemandReview, "Proceed to Demand Review Gate 1")
        };

        public IEnumerable<SelectListItem> ProjectPhases => new List<SelectListItem>
        {
            new SelectListItem {Value = "Initiate", Text = "Initiate"},
            new SelectListItem {Value = "Plan", Text = "Plan"},
            new SelectListItem {Value = "Execute", Text = "Execute"},
            new SelectListItem {Value = "Pre Go Live", Text = "Pre Go Live"},
            new SelectListItem {Value = "Go Live", Text = "Go Live"},
            new SelectListItem {Value = "Close", Text = "Close"},
            new SelectListItem {Value = "On Hold", Text = "On Hold"},
            new SelectListItem {Value = "Cancelled", Text = "Cancelled"},
        };

        public IEnumerable<SelectListItem> SecurityAssessmentOptions => new List<SelectListItem>
		{
			new SelectListItem {Value = "Low", Text = "Low"},
			new SelectListItem {Value = "Medium", Text = "Medium"},
			new SelectListItem {Value = "High", Text = "High"},
			new SelectListItem {Value = "TBD", Text = "TBD"}
		};

		public IEnumerable<SelectListItem> YesNoOptions => new List<SelectListItem>
		{
			new SelectListItem {Value = "Yes", Text = "Yes"},
			new SelectListItem {Value = "No", Text = "No"}
		};

		public IEnumerable<SelectListItem> YesNoUnknownOptions => new List<SelectListItem>
		{
			new SelectListItem {Value = "Yes", Text = "Yes"},
			new SelectListItem {Value = "No", Text = "No"},
			new SelectListItem {Value = "Unknown", Text = "Unknown"}
		};
		#endregion
	}
}
