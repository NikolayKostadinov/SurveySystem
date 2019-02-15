namespace BmsSurvey.WebApp.Infrastructure.Components.Models
{
    using System.Collections.Generic;

    public class RatingControlOptions
    {
        public RatingControlOptions()
        {
            RatingControlValuesRate1to5Stars = new List<string> { "1", "2", "3", "4", "5" };
            RatingControlInitialValueRate1to5Stars = "1";

            RatingControlValuesYesOrNo = new List<string> { "Да", "Не" };
            RatingControlInitialValueYesOrNo = "Да";
            
            RatingControlValuesLowMidHigh = new List<string> { "Ниско", "Средно", "Високо"};
            RatingControlValueLowMidHigh = "Високо";

        }

        //Rate1to5Stars, YesOrNo, LowMidHigh
        public List<string> RatingControlValuesRate1to5Stars { get; set; }
        public string RatingControlInitialValueRate1to5Stars { get; set; }

        public List<string> RatingControlValuesYesOrNo { get; set; }
        public string RatingControlInitialValueYesOrNo { get; set; }

        public List<string> RatingControlValuesLowMidHigh { get; set; }
        public string RatingControlValueLowMidHigh { get; set; }

    }
}
