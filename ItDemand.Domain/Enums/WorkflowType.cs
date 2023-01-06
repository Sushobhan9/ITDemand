using System.ComponentModel;

namespace ItDemand.Domain.Enums
{
	public enum WorkflowType
	{
        [Description("IT Demand Review")]
        ItDemandReview = 1,
        [Description("Proceed Locally (L1)")]
        ProceedLocallyL1 = 2,
        [Description("Proceed Locally with Additional Scrutiny (L2)")]
        ProceedLocallyL2 = 3,
        [Description("Fast Track (L3)")]
        FastTrackL3 = 4,
        [Description("Big Project (L4)")]
        BigProjectL4 = 5
    }
}
