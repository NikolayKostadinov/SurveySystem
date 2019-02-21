//  ------------------------------------------------------------------------------------------------
//   <copyright file="GetSurveyByIdQuery.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Surveys.Queries.GetSurveyById
{
    #region Using

    using MediatR;
    using Models;

    #endregion

    public class GetSurveyByIdQuery : IRequest<SurveySimpleViewModel>
    {
        public GetSurveyByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}