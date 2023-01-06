using ItDemand.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItDemand.Domain.Models
{
	public class BusinessUnit : ISelectListOption
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int ItHeadId { get; set; }
        [ForeignKey("ItHeadId")]
        public virtual User ItHead { get; set; } = null!;

        public bool Active { get; set; }

        public virtual ICollection<DemandRequest> DemandRequests { get; set; } = null!;
    }
}
