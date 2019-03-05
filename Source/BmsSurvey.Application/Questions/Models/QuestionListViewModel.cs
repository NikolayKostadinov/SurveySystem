//  ------------------------------------------------------------------------------------------------
//   <copyright file="QuestionListViewModel.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Questions.Models
{
    #region Using

    using System;
    using System.ComponentModel.DataAnnotations;
    using Domain.Entities;
    using Interfaces.Mapping;

    #endregion

    public class QuestionListViewModel : IMapFrom<Question>
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "DISPLAY_NUMBER")]
        public int DisplayNumber { get; set; }

        [Required]
        [Display(Name = "TEXT")]
        public string Text { get; set; }

        [Required]
        [Display(Name = "QUESTION_TYPE")]
        public QuestionType QuestionType { get; set; }

        public string QuestionTypeStr => QuestionType.ToString();

        [Display(Name = "CREATE_FROM")]
        public string CreatedFrom { get; set; }

        [Display(Name = "LAST_MODIFIED_FROM")]
        public string ModifiedFrom { get; set; }

        [Display(Name = "LAST_MODIFIED_ON")]
        public DateTime? ModifiedOn { get; set; }
    }
}