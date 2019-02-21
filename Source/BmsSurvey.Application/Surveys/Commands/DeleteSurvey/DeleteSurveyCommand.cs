//  ------------------------------------------------------------------------------------------------
//   <copyright file="DeleteSurveyCommand.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Surveys.Commands.DeleteSurvey
{
    #region Using

    using MediatR;

    #endregion

    public class DeleteSurveyCommand : IRequest
    {
        public int Id { get; set; }
    }
}