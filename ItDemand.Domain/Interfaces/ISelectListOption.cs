namespace ItDemand.Domain.Interfaces
{
	public interface ISelectListOption
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        bool Active { get; set; }
    }
}
