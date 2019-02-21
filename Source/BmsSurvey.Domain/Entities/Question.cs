//  ------------------------------------------------------------------------------------------------
//   <copyright file="Question.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Domain.Entities
{
    #region Using

    using System.Collections.Generic;
    using Abstract;

    #endregion

    public enum QuestionType
    {
        Rate1to5Stars, YesOrNo, LowMidHigh, FreeText

        
    }

    public class Question : DeletableEntity
    {
        public Question()
        {
            this.Answers = new HashSet<Answer>();
        }

        public override int Id { get; set; }

        public int DisplayNumber { get; set; }

        public string Text { get; set; }
        public QuestionType QuestionType { get; set; } = QuestionType.Rate1to5Stars;

        public int SurveyId { get; set; }

        public virtual  Survey Survey { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }

   
}