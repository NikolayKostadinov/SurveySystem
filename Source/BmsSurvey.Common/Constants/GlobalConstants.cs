namespace BmsSurvey.Common.Constants
{
    using System.Collections.Generic;
    using System.Globalization;

    public static class GlobalConstants
    {
        private static Dictionary<string, CultureInfo> supportedCultures = new Dictionary<string, CultureInfo>()
        {
            {"bg", new CultureInfo("bg")},
            {"en", new CultureInfo("en")},
        };

        public static Dictionary<string, CultureInfo> SupportedCultures => supportedCultures;
        public static CultureInfo DefaultCulture => new CultureInfo("bg");
        public static string DefaultCultureId => DefaultCulture.TwoLetterISOLanguageName;
        public static int DailyLimit => 16;

        public static string DefaultPassword => "P@ssw0rd";
    }
}