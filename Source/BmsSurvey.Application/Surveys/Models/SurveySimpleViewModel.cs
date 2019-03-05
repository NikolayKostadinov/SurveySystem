//  ------------------------------------------------------------------------------------------------
//   <copyright file="SurveySimpleViewModel.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Surveys.Models
{
    #region Using

    using Domain.Entities;
    using Interfaces.Mapping;

    #endregion

    public class SurveySimpleViewModel : IMapFrom<Survey>
    {
        public int Id { get; set; }
        public string SurveyTitle { get; set; }
    }
}