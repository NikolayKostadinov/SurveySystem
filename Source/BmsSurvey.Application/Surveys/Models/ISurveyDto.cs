//  ------------------------------------------------------------------------------------------------
//   <copyright file="ISurveyDto.cs" company="Business Management System Ltd.">
//       Copyright "" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------
namespace BmsSurvey.Application.Surveys.Models
{
    using System.Collections.Generic;
    using Questions.Models;

    public interface ISurveyDto
    {
        void AddQuestion(QuestionSimpleViewModel question);
        void SetAnswer(int questionId, string value);
        IDictionary<int, QuestionSimpleViewModel> Questions { get; }
    }
}