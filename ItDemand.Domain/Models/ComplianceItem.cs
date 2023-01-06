using ItDemand.Domain.Interfaces;

namespace ItDemand.Domain.Models
{
	public class ComplianceItem : ISelectListOption
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public bool Active { get; set; }

		public virtual ICollection<DemandRequest> DemandRequests { get; set; } = null!;
	}
}
