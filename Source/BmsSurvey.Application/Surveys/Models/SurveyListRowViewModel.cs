//  ------------------------------------------------------------------------------------------------
//   <copyright file="SurveyListRowViewModel.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Surveys.Models
{
    #region Using

    using System;
    using System.ComponentModel.DataAnnotations;
    using Domain.Entities;
    using Interfaces.Mapping;

    #endregion

    public class SurveyListRowViewModel : IMapFrom<Survey>
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "TITLE")]
        public string SurveyTitle { get; set; }
        
        [Display(Name = "DESCRIPTION")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "PAGE_SIZE")]
        public int PageSize { get; set; }

        [Required]
        [Display(Name = "ACTIVE_FROM")]
        public DateTime ActiveFrom { get; set; }

        [Required]
        [Display(Name = "ACTIVE_TO")]
        public DateTime ActiveTo { get; set; }

        [Display(Name = "IS_ACTIVE")]
        public bool IsActive { get; set; }

        [Display(Name = "CREATE_FROM")]
        public string CreatedFrom { get; set; }

        [Display(Name = "LAST_MODIFIED_FROM")]
        public string ModifiedFrom { get; set; }

        [Display(Name = "LAST_MODIFIED_ON")]
        public DateTime? ModifiedOn { get; set; }
    }
}