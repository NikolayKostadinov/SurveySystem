//  ------------------------------------------------------------------------------------------------
//   <copyright file="PieChartOptions.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.WebApp.Infrastructure.Components.Models
{
    #region Using

    using System.Collections.Generic;
    using Domain.Entities;

    #endregion

    public class PieChartOptions
    {
        public PieChartOptions()
        {
            DefaultColors = new Dictionary<QuestionType, Dictionary<string, string>>
            {
                {
                    QuestionType.Rate1to5Stars,
                    new Dictionary<string, string>
                        {{"1", "#e95b54"}, {"2", "#854e9b"}, {"3", "#fbce4a"}, {"4", "#309fdb"}, {"5", "#3caf85"}}
                },
                {
                    QuestionType.LowMidHigh,
                    new Dictionary<string, string> {{"Ниско", "#e95b54"}, {"Средно", "#fbce4a"}, {"Високо", "#309fdb"}}
                },
                {QuestionType.YesOrNo, new Dictionary<string, string> {{"Не", "#e95b54"}, {"Да", "#309fdb"}}}
            };
        }

        public Dictionary<QuestionType, Dictionary<string, string>> DefaultColors { get; }
    }
}