using ItDemand.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItDemand.Domain.Models
{
	public class ChecklistTemplateQuestion
	{
        public int Id { get; set; }

        public int ChecklistTemplateId { get; set; }
        [ForeignKey("ChecklistTemplateId")]
        public virtual ChecklistTemplate ChecklistTemplate { get; set; } = null!;

        public QuestionType QuestionType { get; set; }
        public string Path { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string HelpText { get; set; } = string.Empty;
        public string AcceptedAnswers { get; set; } = string.Empty;
        public string CustomChoices { get; set; } = string.Empty;
    }
}
