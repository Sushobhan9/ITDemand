namespace ItDemand.Domain.Enums
{
	[Flags]
    public enum SecurityRole
    {
        None = 0,
        User = 1,
        Admin = 4,
        Pmo = 8,
        Architecture = 16,
        Security = 32,
        Consulting = 64,
        ItHead = 128
    }
}
