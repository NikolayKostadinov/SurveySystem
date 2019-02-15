namespace BmsSurvey.WebApp.Infrastructure.Components.Models
{
    using System.Collections.Generic;
    using Domain.Entities;
    using Interfaces;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class RatingControlModel
    {
        private readonly IRatingControlTypeService ratingControlTypeService;

        public RatingControlModel(IRatingControlTypeService ratingControlTypeService)
        {
            this.ratingControlTypeService = ratingControlTypeService;
        }

        public string Name { get; set; }
        public List<SelectListItem> SelectedListItems { get; set; }
        public QuestionType QuestionType { get; set; }
        public string RatingControlType => this.ratingControlTypeService.GetRatingControlType(this.QuestionType);
        public string RatingControlValue { get; set; }
    }
}
