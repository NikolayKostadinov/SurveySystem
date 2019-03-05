namespace BmsSurvey.Common.Constants
{
    using System.Collections.Generic;
    using System.Globalization;

    public static class GlobalConstants
    {
        public static readonly Dictionary<string, CultureInfo> SupportedCultures =
            new Dictionary<string, CultureInfo>()
                {
                    {"bg", new CultureInfo("bg")},
                    {"en", new CultureInfo("en")},
                };

        public static CultureInfo DefaultCulture => new CultureInfo("bg");
        public static string DefaultCultureId => DefaultCulture.TwoLetterISOLanguageName;
        public static int DailyLimit => 16;

        public static string DefaultPassword => "P@ssw0rd";
    }
}