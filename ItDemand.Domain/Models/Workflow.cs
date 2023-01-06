using ItDemand.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItDemand.Domain.Models
{
    public class Workflow
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public WorkflowType Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<WorkflowItem> Items { get; set; } = null!;
    }
}
