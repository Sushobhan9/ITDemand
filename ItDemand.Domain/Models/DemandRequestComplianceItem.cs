namespace ItDemand.Domain.Models
{
	public class DemandRequestComplianceItem
	{
		public int DemandRequestId { get; set; }
		public DemandRequest DemandRequest { get; set; } = null!;
		public int ComplianceItemId { get; set; }
		public ComplianceItem ComplianceItem { get; set; } = null!;
	}
}
