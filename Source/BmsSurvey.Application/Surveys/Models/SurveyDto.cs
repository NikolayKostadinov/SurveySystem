using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Surveys.Models
{
    using System.Data;
    using System.Linq;
    using Answers.Models;
    using Exceptions;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using Questions.Models;
    using Surveys.Models;

    public class SurveyDto : ISurveyDto
    {
        private readonly IDictionary<int, AnswerViewModel> answers;

        public SurveyDto()
        {
            this.answers = new Dictionary<int, AnswerViewModel>();
        }

        internal SurveyDto(IEnumerable<AnswerViewModel> answers)
        {
            this.answers = answers.ToDictionary(q => q.QuestionId);
        }

        public virtual void AddAnswer(AnswerViewModel answer)
        {
            this.answers[answer.QuestionId] = answer;
        }

        public virtual void SetAnswer(int questionId, string value)
        {
            if (!this.answers.ContainsKey(questionId))
            {
                throw new KeyNotFoundException("Question with the key \"{questionId}\" Not found!");
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
