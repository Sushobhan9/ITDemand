namespace ItDemand.Domain.Models
{
	public class DemandRequestBusinessUnit
	{
		public int DemandRequestId { get; set; }
		public DemandRequest DemandRequest { get; set; } = null!;
		public int BusinessUnitId { get; set; }
		public BusinessUnit BusinessUnit { get; set; } = null!;
	}
}
