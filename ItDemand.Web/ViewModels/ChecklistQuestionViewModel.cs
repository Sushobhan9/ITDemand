using ItDemand.Domain.Enums;

namespace ItDemand.Web.ViewModels
{
    public class ChecklistQuestionViewModel
    {
        public int Id { get; set; }
        public int ChecklistId { get; set; }
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
