using ItDemand.Domain.Interfaces;

namespace ItDemand.Domain.Models
{
	public class ItSegment : ISelectListOption
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public bool Active { get; set; }
	}
}
