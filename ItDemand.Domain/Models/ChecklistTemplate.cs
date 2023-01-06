using System.ComponentModel.DataAnnotations.Schema;

namespace ItDemand.Domain.Models
{
	public class ChecklistTemplate
	{
		public int Id { get; set; }

        public int WorkflowItemId { get; set; }
        [ForeignKey("WorkflowItemId")]
        public virtual WorkflowItem WorkflowItem { get; set; } = null!;
        
        public string Name { get; set; } = string.Empty;
        public string GateReviewDescription { get; set; } = string.Empty;
     
        public virtual ICollection<ChecklistTemplateApprover> Approvers { get; set; } = null!;
        public virtual ICollection<ChecklistTemplateQuestion> Questions { get; set; } = null!;

    }
}
