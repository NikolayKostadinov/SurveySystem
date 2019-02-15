using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Surveys.Queries.GetSurveyQuestions
{
    using MediatR;
    using Models;
    using Questions.Models;

    public class SurveyQuestionsQuery: IRequest<SurveyViewModel>
    {
        public SurveyQuestionsQuery(int id, int pageNumber)
        {
            Id = id;
            PageNumber = pageNumber;
        }

        internal int Id { get; }
        internal int PageNumber { get; }
    }
}
