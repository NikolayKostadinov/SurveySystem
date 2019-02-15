//-----------------------------------------------------------------------
// <copyright file="LocalizationUrlService.cs" company="Business Management System Ltd.">
//     Copyright "2017" (c) Business Management System Ltd.. All rights reserved.
// </copyright>
// <author>Nikolay.Kostadinov</author>
//-----------------------------------------------------------------------

namespace BmsSurvey.WebApp.Services
{
    #region Usings

    using System;
    using System.Text.RegularExpressions;
    using Application.Interfaces;

    #endregion

    /// <summary>
    /// Summary description for LocalizationUrlService
    /// </summary>
    public class LocalizationUrlService : ILocalizationUrlService
    {
        private readonly ISupportedCulturesService supportedCulturesService;

        public LocalizationUrlService(ISupportedCulturesService supportedCulturesService)
        {
            this.supportedCulturesService = supportedCulturesService ?? throw new ArgumentNullException(nameof(supportedCulturesService));
        }

        public Uri GetLocalizedUri(Uri uri, string uICulture)
        {
            var cleanSegment = this.GetCleanSegment(uri);

            var queryString = uri.Query;

            if (!this.supportedCulturesService.IsCultureSupported(uICulture))
            {
                return new Uri($"{uri.Scheme}://{uri.Authority}{cleanSegment}{queryString}");
            }

            return new Uri($"{uri.Scheme}://{uri.Authority}/{uICulture}{cleanSegment}{queryString}");
        }

        public string GetUrlCulture(Uri url)
        {
            string urlWithoutQuery = url.AbsoluteUri;

            if (!string.IsNullOrEmpty(url.Query))
            {
                urlWithoutQuery = url.AbsoluteUri.Replace(url.Query, string.Empty);
            }
                
            var urlParts = urlWithoutQuery.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            if (urlParts?.Length > 2 && this.supportedCulturesService.IsCultureSupported(urlParts[2]))
            {
                return urlParts[2];
            }

            return string.Empty;
        }

        private string GetCleanSegment(Uri inUri)
        {
            var supportedCultures = this.supportedCulturesService.GetSupportedCultures();
            string endingPattern = $"(/{string.Join("|/", supportedCultures)})$";
            string innerPattern = $"/{string.Join("/|/", supportedCultures)}/";
            string replacement = "/";
            Regex rgx = new Regex(innerPattern);
            var result = rgx.Replace(inUri.AbsolutePath, replacement);
            rgx = new Regex(endingPattern);
            string resultSegment = rgx.Replace(result, string.Empty);
            return (resultSegment == "/") ? string.Empty : resultSegment;
        }
    }
}