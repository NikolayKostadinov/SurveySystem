using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Answers.Commands
{
    using MediatR;

    class EvaluateAnswersCommand:IRequest<QuestionEvaluationModel>
    {
    }

    internal class QuestionEvaluationModel
    {
    }
}
