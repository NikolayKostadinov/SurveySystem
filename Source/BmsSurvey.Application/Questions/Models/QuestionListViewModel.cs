using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Questions.Models
{
    using System.ComponentModel.DataAnnotations;
    using Domain.Entities;
    using Interfaces.Mapping;

    public class QuestionListViewModel:IMapFrom<Question>
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

        public string QuestionTypeStr => this.QuestionType.ToString();

        [Display(Name = "CREATE_FROM")]
        public string CreatedFrom { get; set; }

        [Display(Name = "LAST_MODIFIED_FROM")]
        public string ModifiedFrom { get; set; }

        [Display(Name = "LAST_MODIFIED_ON")]
        public DateTime? ModifiedOn { get; set; }
    }
}
