using ItDemand.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItDemand.Domain.Models
{
	public class BusinessProcessL3 : ISelectListOption
	{
		public int Id { get; set; }

		public int BusinessProcessL2Id { get; set; }
		[ForeignKey("BusinessProcessL2Id")]
		public virtual BusinessProcessL2 BusinessProcessL2 { get; set; } = null!;

		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public bool Active { get; set; }
	}
}
