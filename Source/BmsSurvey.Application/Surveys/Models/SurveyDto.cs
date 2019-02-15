using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Surveys.Models
{
    using System.Data;
    using System.Linq;
    using Exceptions;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using Questions.Models;
    using Surveys.Models;

    public class SurveyDto : ISurveyDto
    {
        private readonly IDictionary<int, QuestionSimpleViewModel> questions;

        public SurveyDto()
        {
            this.questions = new Dictionary<int, QuestionSimpleViewModel>();
        }

        internal SurveyDto(IEnumerable<QuestionSimpleViewModel> questions)
        {
            this.questions = questions.ToDictionary(q => q.Id);
        }

        public virtual void AddQuestion(QuestionSimpleViewModel question)
        {
            this.questions[question.Id] = question;
        }

        public virtual void SetAnswer(int questionId, string value)
        {
            if (!this.questions.ContainsKey(questionId))
            {
                throw new KeyNotFoundException("Question with the key \"{questionId}\" Not found!");
            }

            this.questions[questionId].Value = value;
        }

        public virtual IDictionary<int, QuestionSimpleViewModel> Questions => this.questions;
    }
}
