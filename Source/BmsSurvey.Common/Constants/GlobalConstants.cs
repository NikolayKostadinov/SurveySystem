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

        public static Dictionary<string, int> Rate1To5StarsValues => new Dictionary<string, int>
            {{"1", 1}, {"2", 2}, {"3", 3}, {"4", 4}, {"5", 5}};

        public static Dictionary<string, bool> YesOrNoValues => new Dictionary<string, bool> { { "Да", true }, { "Не", false } };

        public static Dictionary<string, int> LowMidHighValues => new Dictionary<string, int> { { "Ниско", 0 }, { "Средно", 1 }, { "Високо", 2 } };
    }
}