//  ------------------------------------------------------------------------------------------------
//   <copyright file="SupportedCulturesProvider.cs" company="Business Management System Ltd.">
//       Copyright "2018" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.WebApp.Services
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using Application.Interfaces;
    using Common.Constants;

    #endregion

    /// <summary>
    ///     Summary description for SupportedCulturesProvider
    /// </summary>
    public class SupportedCulturesService : ISupportedCulturesService
    {
        public SupportedCulturesService(string defaultCultureParam)
        {
            if (string.IsNullOrEmpty(defaultCultureParam))
                throw new ArgumentException($"Parameter {nameof(defaultCultureParam)} must be not empty.");

            SystemDefaultCulture = defaultCultureParam;
        }

        public string SystemDefaultCulture { get; }

        public ICollection<string> GetSupportedCultures()
        {
            return GlobalConstants.SupportedCultures.Select(x => x.Key).ToList();
        }

        public IDictionary<string, CultureInfo> GetSupportedCulturesDictionary()
        {
            return GlobalConstants.SupportedCultures;
        }

        public bool IsCultureSupported(string culture)
        {
            return GetSupportedCultures().Contains(culture);
        }

        public void SetApplicationCulture(string culture)
        {
            var cultures = GetSupportedCulturesDictionary();
            if (!cultures.ContainsKey(culture)) throw new ArgumentException("Invalid culture");

            var appCulture = cultures[culture];
            Thread.CurrentThread.CurrentCulture = new CultureInfo(appCulture.TwoLetterISOLanguageName);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(appCulture.TwoLetterISOLanguageName);
        }

        public void SetApplicationDefaultCulture()
        {
            SetApplicationCulture(SystemDefaultCulture);
        }
    }
}