namespace BmsSurvey.Application.Surveys.Queries.GetSurveyQuestionsWithAnswers
{
    using MediatR;
    using Models;

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
