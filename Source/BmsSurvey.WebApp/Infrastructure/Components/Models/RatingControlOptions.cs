namespace BmsSurvey.WebApp.Infrastructure.Components.Models
{
    using System.Collections.Generic;

    public class RatingControlOptions
    {
        public RatingControlOptions()
        {
            RatingControlTagsRate1To5Stars = new Dictionary<string, int> { { "1", 1 }, { "2", 2 }, { "3", 3 }, { "4", 4 }, { "5", 5 } };
            RatingControlInitialValueRate1To5Stars = 1;

            RatingControlTagsYesOrNo = new Dictionary<string, bool> { { "Да", true }, { "Не", false } };
            RatingControlInitialValueYesOrNo = true;

            RatingControlTagsLowMidHigh = new Dictionary<string, int> { { "Ниско", 0 }, { "Средно", 1 }, { "Високо", 2 } };
            RatingControlValueLowMidHigh = 2;

        }

        //Rate1to5Stars, YesOrNo, LowMidHigh
        public Dictionary<string, int> RatingControlTagsRate1To5Stars { get; set; }
        public int RatingControlInitialValueRate1To5Stars { get; set; }

        public Dictionary<string, bool> RatingControlTagsYesOrNo { get; set; }
        public bool RatingControlInitialValueYesOrNo { get; set; }

        public Dictionary<string, int> RatingControlTagsLowMidHigh { get; set; }
        public int RatingControlValueLowMidHigh { get; set; }

    }
}
