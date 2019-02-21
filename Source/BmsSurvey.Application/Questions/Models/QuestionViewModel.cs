namespace BmsSurvey.Application.Questions.Models
{
    using Domain.Entities;

    public class QuestionViewModel
    {
        public int Id { get; set; }
        public int DisplayNumber { get; set; }
        public string Text { get; set; }
        public QuestionType QuestionType { get; set; }
        public string Value { get; set; }
        public bool IsChecked { get; set; }
    }
}
