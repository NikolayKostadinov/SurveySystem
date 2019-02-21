using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Services
{
    using Answers.Models;
    using Domain.Abstract;
    using Domain.Entities;
    using Domain.Entities.Answers;
    using Interfaces;
    using Questions.Models;
    using Remotion.Linq.Clauses;

    public class AnswerFactory : IAnswerFactory
    {
        public Answer GetAnswer(QuestionType questionType, AnswerViewModel answer)
        {
            switch (questionType)
            {
                case QuestionType.Rate1to5Stars:
                    return new Rate1To5StarsAnswer()
                    {
                        QuestionId = answer.QuestionId,
                        Value = int.Parse(answer.Value)
                    };
                case QuestionType.LowMidHigh:
                    return new LowMidHighAnswer()
                    {
                        QuestionId = answer.QuestionId,
                        Value = int.Parse(answer.Value)
                    };
                case QuestionType.YesOrNo:
                    return new YesOrNoAnswer()
                    {
                        QuestionId = answer.QuestionId,
                        Value = bool.Parse(answer.Value)
                    };
                case QuestionType.FreeText:
                    return new FreeTextAnswer()
                    {
                        QuestionId = answer.QuestionId,
                        Value = answer.Value
                    };
                default:
                    throw new ArgumentOutOfRangeException(nameof(questionType), $"Unrecognized question type \"{questionType}\"");
            }
        }
    }
}
