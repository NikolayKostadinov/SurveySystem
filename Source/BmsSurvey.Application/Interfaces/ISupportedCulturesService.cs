//-----------------------------------------------------------------------
// <copyright file="ISupportedCulturesService.cs" company="Business Management System Ltd.">
//     Copyright "2017" (c) Business Management System Ltd.. All rights reserved.
// </copyright>
// <author>Nikolay.Kostadinov</author>
//-----------------------------------------------------------------------

namespace BmsSurvey.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Summary description for ISupportedCulturesService
    /// </summary>
    public interface ISupportedCulturesService
    {
        ICollection<string> GetSupportedCultures();

        string SystemDefaultCulture { get; }

        IDictionary<string, CultureInfo> GetSupportedCulturesDictionary();

        bool IsCultureSupported(string uICulture);

        void SetApplicationCulture(string culture);

        void SetApplicationDefaultCulture();
    }
}
