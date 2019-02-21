namespace BmsSurvey.Application.Surveys.Queries.GetSurveyQuestionsWithAnswers
{
    using MediatR;
    using Models;

    public class SurveyQuestionsQuery: IRequest<SurveyViewModel>
    {
        public SurveyQuestionsQuery(int id, int pageNumber, string ipAddress)
        {
            Id = id;
            PageNumber = pageNumber;
            IpAddress = ipAddress;
        }

        internal int Id { get; }
        internal int PageNumber { get; }
        internal string IpAddress { get; }
    }
}
