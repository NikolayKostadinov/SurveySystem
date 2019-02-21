//  ------------------------------------------------------------------------------------------------
//   <copyright file="QuestionsForSurveyQuery.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Questions.Queries.GetAllQuestionsForSurvey
{
    #region Using

    using System.Collections.Generic;
    using MediatR;
    using Models;

    #endregion

    public class QuestionsForSurveyQuery : IRequest<IEnumerable<QuestionListViewModel>>
    {
        public int SurveyId { get; set; }
    }
}