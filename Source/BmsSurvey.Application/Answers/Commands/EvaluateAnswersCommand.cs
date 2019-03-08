using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Answers.Commands
{
    using MediatR;
    using Questions.Models;
    using Questions.Models.EvaluationModels;

    class EvaluateAnswersCommand:IRequest<QuestionEvaluationModel>
    {
    }
}
