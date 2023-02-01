using System.ComponentModel;

namespace ItDemand.Domain.Enums
{
	[Flags]
    public enum SecurityRole
    {
        [Description("None")]
        None = 0,
        [Description("User")]
        User = 1,
        [Description("System Administrator")]
        Admin = 4,
        [Description("PMO")]
        Pmo = 8,
        [Description("Architecture")]
        Architecture = 16,
        [Description("Security & Compliance")]
        Security = 32,
        [Description("Global Business Consulting")]
        Consulting = 64,
        [Description("IT Head")]
        ItHead = 128
    }
}
