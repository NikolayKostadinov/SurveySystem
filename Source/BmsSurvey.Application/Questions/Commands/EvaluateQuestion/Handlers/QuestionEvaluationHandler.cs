using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Questions.Commands.EvaluateQuestion.Handlers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Domain.Entities;
    using Exceptions;
    using Models.EvaluationModels;

    public abstract class QuestionEvaluationHandler : IQuestionEvaluationHandler
    {
        private IQuestionEvaluationHandler Successor { get; set; }
        
        public async Task<BaseQuestionEvaluationModel> EvaluateQuestionAsync(Question question)
        {
            if (await this.CanHandleAsync(question))
            {
                return await this.HandleAsync(question);
            }

            if (this.Successor is null)
            {
                throw new NotFoundException(nameof(QuestionEvaluationHandler), question.QuestionType);
            }

            return await this.Successor?.EvaluateQuestionAsync(question);
        }


        public void SetSuccessor(IQuestionEvaluationHandler successor)
        {
            this.Successor = successor;
        }

        protected abstract Task<bool> CanHandleAsync(Question question);

        protected abstract Task<BaseQuestionEvaluationModel> HandleAsync(Question question);
    }
}
