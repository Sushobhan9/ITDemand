using ItDemand.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItDemand.Domain.Models
{
	public class ChecklistApprover
	{
		public int Id { get; set; }

		public int ChecklistId { get; set; }
		[ForeignKey("ChecklistId")]
		public virtual Checklist Checklist { get; set; } = null!;

		public SecurityRole Role { get; set; }
        public ApproverType Type { get; set; }
        public bool Required { get; set; }
        public int? SortIndex { get; set; }

        public int? ApproverId { get; set; }
		[ForeignKey("ApproverId")]
		public virtual User? Approver { get; set; }

		public DateTime? ApprovalDate { get; set; }
		
	}
}
