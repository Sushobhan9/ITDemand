using System.ComponentModel;

namespace ItDemand.Domain.Enums
{
    [Flags]
    public enum ApproverRole
	{
        [Description("IT Project Committee")]
        ItsProjectCommittee = 1,
        [Description("PMO (IT)")]
        ItsPmo = 2,
        [Description("Finance (IT)")]
        ItsFinance = 4,
        [Description("Business Sponsor (IT)")]
        ItsBusinessSponsor = 8,
        [Description("It Head")]
        ItsHead = 16,
        [Description("Global Business Consultants")]
        Gbc = 32,
        [Description("Project Manager")]
        ProjectManager = 64,
        [Description("Risk & Compliance")]
        RiskCompliance = 128,
        [Description("Architecture Team")]
        ArchitectureTeam = 256,
    }
}
