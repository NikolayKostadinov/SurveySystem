//  ------------------------------------------------------------------------------------------------
//   <copyright file="ILocalizationService.cs" company="Business Management System Ltd.">
//       Copyright "2018" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Common.Interfaces
{
    using Microsoft.Extensions.Localization;

    public interface ILocalizationService<T> where T : class
    {
        LocalizedString GetLocalizedHtmlString(string key);
        LocalizedString GetLocalizedHtmlString(string key, string parameter);
    }
}