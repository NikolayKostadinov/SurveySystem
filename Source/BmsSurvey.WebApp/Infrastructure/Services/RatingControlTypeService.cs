using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BmsSurvey.WebApp.Infrastructure.Services
{
    using Common.Constants;
    using Domain.Entities;
    using Interfaces;
    using Microsoft.Extensions.Configuration;

    public class RatingControlTypeService : IRatingControlTypeService
    {
        public static readonly IDictionary<QuestionType, string> DefaultRatingTypes =
            new Dictionary<QuestionType, string>()
            {
                {QuestionType.Rate1to5Stars, "css-stars"},
                {QuestionType.LowMidHigh, "bars-movie"},
                {QuestionType.YesOrNo, "bars-yes-no"},
                {QuestionType.FreeText, null},
            };

        private readonly IDictionary<QuestionType, string> ratingTypes;

        public RatingControlTypeService(IConfiguration config)
        {
            this.ratingTypes = new Dictionary<QuestionType, string>();
            var questionTypes = Enum.GetValues(typeof(QuestionType));
            foreach (QuestionType questionType in questionTypes)
            {
                this.ratingTypes[questionType] = config[$"RatingTypes:{questionType.ToString()}"]
                                                 ?? DefaultRatingTypes[questionType] ?? string.Empty;
            }
        }

        public string GetRatingControlType(QuestionType questionType)
        {
            this.ratingTypes.TryGetValue(questionType, out string result);
            return result;
        }
    }
}
