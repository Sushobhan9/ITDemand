using ItDemand.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItDemand.Domain.Models
{
	public class ChecklistTemplateApprover
	{
        public int Id { get; set; }

        public int? ChecklistTemplateId { get; set; }
        [ForeignKey("ChecklistTemplateId")]
        public virtual ChecklistTemplate ChecklistTemplate { get; set; } = null!;

        public ApproverRole Role { get; set; } // when approver type is drop-down will list out those in user table with matching security role
        public ApproverType Type { get; set; }
        public bool Required { get; set; }
        public int? SortIndex { get; set; }    
    }
}
