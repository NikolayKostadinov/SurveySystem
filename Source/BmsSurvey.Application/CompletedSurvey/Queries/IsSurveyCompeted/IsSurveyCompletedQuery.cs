namespace BmsSurvey.Application.CompletedSurvey.Queries.IsSurveyCompeted
{
    using MediatR;

    public class IsSurveyCompletedQuery:IRequest<bool>
    {
        public IsSurveyCompletedQuery(int surveyId, string ipAddress)
        {
            SurveyId = surveyId;
            IpAddress = ipAddress;
        }

        public int SurveyId { get; }
        public string IpAddress { get; }
    }
}
