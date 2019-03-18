using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Questions.Commands.EvaluateQuestion.Handlers
{
    public interface IQuestionEvaluationHandlerFactory
    {
        IQuestionEvaluationHandler GetHandler();
    }
}
