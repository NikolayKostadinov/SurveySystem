using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Questions.Commands.EvaluateQuestion.Handlers
{
    using System.Threading.Tasks;
    using Domain.Entities;
    using Models.EvaluationModels;

    public interface IQuestionEvaluationHandler
    {
        Task<BaseQuestionEvaluationModel> EvaluateQuestionAsync(Question question);
        void SetSuccessor(IQuestionEvaluationHandler successor);
    }
}
