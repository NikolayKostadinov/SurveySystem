namespace BmsSurvey.Application.Questions.Commands.EvaluateQuestion.Handlers
{
    using System.Collections.Generic;
    using System.Linq;
    using Models.EvaluationModels;

    public class PercentCorrectionService : IPercentCorrectionService
    {
        public void CorrectResult(IDictionary<string, DistributionOfResultsModel> result)
        {
            var correction = 100 - result.Sum(x => x.Value.Percentage);
            if (correction == 100)
            {
                return;
            }
            var maxValue = result.Max(x => x.Value.Percentage);
            var mostSignificantKey = result.Where(x => x.Value.Percentage == maxValue).Select(x => x.Key).First();
            result[mostSignificantKey].Percentage += correction;
        }
    }
}