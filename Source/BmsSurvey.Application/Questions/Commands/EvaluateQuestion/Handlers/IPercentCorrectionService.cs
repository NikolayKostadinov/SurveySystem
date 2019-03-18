//  ------------------------------------------------------------------------------------------------
//   <copyright file="IPercentCorrectionService.cs" company="Business Management System Ltd.">
//       Copyright "" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------
namespace BmsSurvey.Application.Questions.Commands.EvaluateQuestion.Handlers
{
    using System.Collections.Generic;
    using Models.EvaluationModels;

    public interface IPercentCorrectionService
    {
        void CorrectResult(IDictionary<string, DistributionOfResultsModel> result);
    }
}