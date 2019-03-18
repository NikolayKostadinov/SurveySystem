namespace BmsSurvey.WebApp.Infrastructure.Components.Models
{
    using System.Collections.Generic;
    using Common.Constants;

    public class RatingControlOptions
    {
        public RatingControlOptions()
        {
            RatingControlTagsRate1To5Stars = GlobalConstants.Rate1To5StarsValues;
            RatingControlInitialValueRate1To5Stars = 1;

            RatingControlTagsYesOrNo = GlobalConstants.YesOrNoValues;
            RatingControlInitialValueYesOrNo = true;

            RatingControlTagsLowMidHigh = GlobalConstants.LowMidHighValues;
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
