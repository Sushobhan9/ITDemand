﻿using ItDemand.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItDemand.Domain.Models
{
	public class ChecklistQuestion
	{
		public int Id { get; set; }

		public int ChecklistId { get; set; }
		[ForeignKey("ChecklistId")]
		public virtual Checklist Checklist { get; set; } = null!;

		public QuestionType QuestionType { get; set; }
		public string Path { get; set; } = string.Empty;
		public string Text { get; set; } = string.Empty;
		public string HelpText { get; set; } = string.Empty;
		public string Answer { get; set; } = string.Empty;
		public string Comments { get; set; } = string.Empty;
		public string AcceptedAnswers { get; set; } = string.Empty;
		public string CustomChoices { get; set; } = string.Empty;
	}
}
