using System.ComponentModel;

namespace ItDemand.Domain.Enums
{
	public enum DemandState
    {
        [Description("Initial Entry")]
        InitialEntry = 1,
        [Description("PMO Review")]
        PmoReview = 2,
        [Description("Demand Gate 1")]
        DemandGate1 = 3,
        [Description("Red CAB Review")]
        RedCabApproval = 4,
        [Description("IT Head Initial Review")]
        ItHeadInitialReview = 5,
        [Description("Solution Design")]
        SolutionDesign = 6,
        [Description("Security Review")]
        SecurityReview = 7,
        [Description("Architecture Review")]
        ArchitectureReview = 8,
        [Description("Demand Gate 2")]
        DemandGate2 = 9,
        [Description("IT Head Approval")]
        ItHeadApproval = 10,
        [Description("Gate 3 Plan")]
        Gate3Plan = 11,
        [Description("Gate 4 Execute")]
        Gate4Execute = 12,
        [Description("Gate 5 Pre-Go Live")]
        Gate5PreGoLive = 13,
        [Description("Gate 6 Go Live")]
        Gate6GoLive = 14,
        [Description("Gate 7 Closeout")]
        Gate7Closeout = 15,
        [Description("Cancelled")]
        Cancelled = 16,
        [Description("On-Hold")]
        OnHold = 17,
        [Description("Complete")]
        Complete = 18
    }
}
