//  ------------------------------------------------------------------------------------------------
//   <copyright file="ILocalizationUrlService.cs" company="Business Management System Ltd.">
//       Copyright "2018" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Interfaces
{
    #region Using

    using System;

    #endregion

    /// <summary>
    ///     Summary description for ILocalizationUrlService
    /// </summary>
    public interface ILocalizationUrlService
    {
        Uri GetLocalizedUri(Uri uri, string uICulture);

        string GetUrlCulture(Uri url);
    }
}