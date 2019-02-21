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
    using Common.Extensions;
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
                RatingControlInitialValueRate1To5Stars = config.GetValue<int>("RatingControlInitialValues:Rate1to5Stars"),
                RatingControlInitialValueYesOrNo = config.GetValue<bool>("RatingControlInitialValues:YesOrNo"),
                RatingControlValueLowMidHigh = config.GetValue<int>("RatingControlInitialValues:LowMidHigh")
            };
        }

        public IViewComponentResult Invoke(QuestionType ratingControlType, string name, string value = null)
        {
            var ratingControlValues = new Dictionary<string, object>();
            object ratingControlInitialValue = "";

            if (ratingControlType == QuestionType.LowMidHigh)
            //if (ratingControlType == "bars-pill")
            {
                ratingControlOptions.RatingControlTagsLowMidHigh.ForEach(a => ratingControlValues.Add(a.Key, a.Value));
                ratingControlInitialValue = ratingControlOptions.RatingControlValueLowMidHigh;
            }
            else if (ratingControlType == QuestionType.YesOrNo)
            //else if (ratingControlType == "bars-1to10")
            {
                ratingControlOptions.RatingControlTagsYesOrNo.ForEach(a => ratingControlValues.Add(a.Key, a.Value));
                ratingControlInitialValue = ratingControlOptions.RatingControlInitialValueYesOrNo;
            }
            else if (ratingControlType == QuestionType.Rate1to5Stars)
            //else if (ratingControlType == "css-stars")
            {
                ratingControlOptions.RatingControlTagsRate1To5Stars.ForEach(a => ratingControlValues.Add(a.Key, a.Value));

                ratingControlInitialValue = ratingControlOptions.RatingControlInitialValueRate1To5Stars;
            }

            ratingControlInitialValue = string.IsNullOrEmpty(value) ? ratingControlInitialValue : value;

            var ratings = ratingControlValues
                .Select(position => new SelectListItem
                {
                    Value = position.Value.ToString(),
                    Text = position.Key,
                    Selected = position.Value.ToString().Equals(ratingControlInitialValue)
                }).ToList();


            var ratingControlModel = new RatingControlModel(this.ratingTypeService)
            {
                SelectedListItems = ratings,
                QuestionType = ratingControlType,
                Name = name,
                RatingControlValue = value
            };

            return View(ratingControlModel);
        }
    }
}