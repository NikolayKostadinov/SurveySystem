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
    using Answers.Models;
    using Questions.Models;

    public interface ISurveyDto
    {
        void AddAnswer(AnswerViewModel answer);
        void SetAnswer(int questionId, string value);
        IDictionary<int, AnswerViewModel> Answers { get; }
        void ClearAnswers();
    }
}