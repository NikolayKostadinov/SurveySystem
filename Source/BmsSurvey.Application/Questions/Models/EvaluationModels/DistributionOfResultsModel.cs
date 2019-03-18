using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Questions.Models.EvaluationModels
{
    public class DistributionOfResultsModel
    {
        public DistributionOfResultsModel(int answersCount, decimal percentage)
        {
            AnswersCount = answersCount;
            Percentage = percentage;
        }

        public int AnswersCount { get; set; }
        public decimal Percentage { get; set; }
    }
}
