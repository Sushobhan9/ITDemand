using ItDemand.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItDemand.Domain.Models
{
	public class BusinessProcessL2 : ISelectListOption
	{
		public int Id { get; set; }

		public int BusinessProcessL1Id { get; set; }
		[ForeignKey("BusinessProcessL1Id")]
		public virtual BusinessProcessL1 BusinessProcessL1 { get; set; } = null!;

		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public bool Active { get; set; }
	}
}
