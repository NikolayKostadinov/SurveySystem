//  ------------------------------------------------------------------------------------------------
//   <copyright file="GetNextQuestionDisplayNumberQuery.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Questions.Queries.GetNextQuestionDisplayNumber
{
    #region Using

    using MediatR;

    #endregion

    public class GetNextQuestionDisplayNumberQuery : IRequest<int>
    {
        public int SurveyId { get; set; }
    }
}