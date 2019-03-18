//  ------------------------------------------------------------------------------------------------
//   <copyright file="QuestionEvaluationHandlerFactory.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Questions.Commands.EvaluateQuestion.Handlers
{
    #region Using

    using System;

    #endregion

    public class QuestionEvaluationHandlerFactory : IQuestionEvaluationHandlerFactory
    {
        private readonly IQuestionEvaluationHandler handler;

        public QuestionEvaluationHandlerFactory(IQuestionEvaluationHandler handler)
        {
            this.handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public IQuestionEvaluationHandler GetHandler() => handler;
    }
}