using System.Collections.Generic;

namespace BmsSurvey.Application.Surveys.Models
{
    using Answers.Models;

    public class SurveyDto : ISurveyDto
    {
        private readonly IDictionary<int, AnswerViewModel> answers;

        public SurveyDto()
        {
            this.answers = new Dictionary<int, AnswerViewModel>();
        }

        public virtual void AddAnswer(AnswerViewModel answer)
        {
            this.answers[answer.QuestionId] = answer;
        }

        public virtual void SetAnswer(int questionId, string value)
        {
            if (!this.answers.ContainsKey(questionId))
            {
                throw new KeyNotFoundException($"Question with the key \"{questionId}\" Not found!");
            }

            this.answers[questionId].Value = value;
        }

        public virtual IDictionary<int, AnswerViewModel> Answers => this.answers;
        public virtual void ClearAnswers()
        {
            this.Answers.Clear();
        }
    }
}
