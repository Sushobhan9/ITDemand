using ItDemand.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItDemand.Domain.Models
{
	public class WorkflowItem
	{
        public int Id { get; set; }

        [ForeignKey("WorkflowId"), InverseProperty("Items")]
        public virtual Workflow Workflow { get; set; } = null!;
        public WorkflowType WorkflowId { get; set; }

        [ForeignKey("ParentId")]
        public virtual WorkflowItem Parent { get; set; } = null!;
        public int? ParentId { get; set; }

        public WorkflowItemType WorkflowItemType { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public int Stage { get; set; }
        public double SequenceNumber { get; set; }
        public string Description { get; set; } = string.Empty;

        public virtual ICollection<WorkflowItem> Children { get; set; } = null!;
        public virtual ICollection<ChecklistTemplate> ChecklistTemplates { get; set; } = null!;

        public WorkflowItem()
        {
            WorkflowItemType = WorkflowItemType.Unspecified;
            Children = new List<WorkflowItem>();
            ChecklistTemplates = new List<ChecklistTemplate>();
        }

        public WorkflowItem(WorkflowItemType itemType, WorkflowType workflowId, string name, string description, int stage)
            : this()
        {
            WorkflowId = workflowId;
            WorkflowItemType = itemType;
            Name = name;
            Stage = stage;
            SequenceNumber = stage;
            Description = description;
        }

        public virtual WorkflowItem CreateChild(WorkflowItemType itemType, string name, string description)
        {
            var child = new WorkflowItem
            {
                Workflow = Workflow,
                WorkflowId = WorkflowId,
                Parent = this,
                ParentId = Id,
                WorkflowItemType = itemType,
                Name = name,
                Description = description,
                Stage = Stage
            };

            Children.Add(child);
            child.SequenceNumber = SequenceNumber + (Children.Count * .01);
            return child;
        }
    }
}
