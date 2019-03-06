//  ------------------------------------------------------------------------------------------------
//   <copyright file="DeleteQuestionCommand.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Questions.Commands.DeleteQuestion
{
    #region Using

    using MediatR;

    #endregion

    public class DeleteQuestionCommand : IRequest
    {
        public int Id { get; set; }
    }
}