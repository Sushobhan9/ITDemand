using ItDemand.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItDemand.Domain.Models
{
	public class Checklist
	{
        public int Id { get; set; }

        public int DemandRequestId { get; set; }
        [ForeignKey("DemandRequestId")]
        public virtual DemandRequest DemandRequest { get; set; } = null!;

        public int? ChecklistTemplateId { get; set; }
        [ForeignKey("ChecklistTemplateId")]
        public virtual ChecklistTemplate ChecklistTemplate { get; set; } = null!;

        public string Name { get; set; } = string.Empty;
        public StatusType Status { get; set; } = StatusType.New;
        public double SequenceNumber { get; set; }

        public int? WorkflowItemId { get; set; }
        [ForeignKey("WorkflowItemId")]
        public virtual WorkflowItem WorkflowItem { get; set; } = null!;

        public string AssociatedStandard { get; set; } = string.Empty;
        public string GateReviewDescription { get; set; } = string.Empty;

        public DateTime? MeetingDate { get; set; }
        public string MeetingMinutesBy { get; set; } = string.Empty;
        public string MeetingMinutesByUserName { get; set; } = string.Empty;

        public DateTime? RevisionDate { get; set; }

        public string Scope { get; set; } = string.Empty;

        public string AdditionalComments { get; set; } = string.Empty;

        public string ReviewComments { get; set; } = string.Empty;
                
        public virtual ICollection<ChecklistApprover> Approvers { get; set; } = null!;
        public virtual ICollection<ChecklistQuestion> Questions { get; set; } = null!;
    }
}
