//  ------------------------------------------------------------------------------------------------
//   <copyright file="Class.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.WebApp.Infrastructure.Components
{
    #region Using

    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Domain.Entities;
    using Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.Extensions.Configuration;
    using Models;

    #endregion

    public class RatingControlViewComponent : ViewComponent
    {
        private readonly RatingControlOptions ratingControlOptions;
        private readonly IRatingControlTypeService ratingTypeService;

        public RatingControlViewComponent(IConfiguration config, IRatingControlTypeService ratingTypeService)
        {
            this.ratingTypeService = ratingTypeService;

            ratingControlOptions = new RatingControlOptions
            {
                RatingControlInitialValueRate1to5Stars = config["RatingControlInitialValues:Rate1to5Stars"],
                RatingControlInitialValueYesOrNo = config["RatingControlInitialValues:YesOrNo"],
                RatingControlValueLowMidHigh = config["RatingControlInitialValues:LowMidHigh"]
            };
        }

        public IViewComponentResult Invoke(QuestionType ratingControlType, string name, string value = null)
        {
            var ratingControlValues = new List<string>();
            var ratingControlInitialValue = "";

            if (ratingControlType == QuestionType.LowMidHigh)
            //if (ratingControlType == "bars-pill")
            {
                ratingControlOptions.RatingControlValuesLowMidHigh.ForEach(a => ratingControlValues.Add(a));
                ratingControlInitialValue = ratingControlOptions.RatingControlValueLowMidHigh;
            }
            else if (ratingControlType == QuestionType.YesOrNo)
            //else if (ratingControlType == "bars-1to10")
            {
                ratingControlOptions.RatingControlValuesYesOrNo.ForEach(a => ratingControlValues.Add(a));
                ratingControlInitialValue = ratingControlOptions.RatingControlInitialValueYesOrNo;
            }
            else if (ratingControlType == QuestionType.Rate1to5Stars)
            //else if (ratingControlType == "css-stars")
            {
                ratingControlOptions.RatingControlValuesRate1to5Stars.ForEach(a => ratingControlValues.Add(a));

                ratingControlInitialValue = ratingControlOptions.RatingControlInitialValueRate1to5Stars;
            }

            ratingControlInitialValue = string.IsNullOrEmpty(value) ? ratingControlInitialValue : value;

            var ratings = ratingControlValues
                .Select(myValue => new SelectListItem
                {
                    Value = myValue,
                    Text = myValue,
                    Selected = myValue.Equals(ratingControlInitialValue)
                }).ToList();


            var ratingControlModel = new RatingControlModel(this.ratingTypeService)
            {
                SelectedListItems = ratings,
                QuestionType = ratingControlType,
                Name = name
            };

            return View(ratingControlModel);
        }
    }
}