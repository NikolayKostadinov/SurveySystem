namespace BmsSurvey.WebApp.Resources
{
    using System.Reflection;
    using Common.Interfaces;
    using Microsoft.Extensions.Localization;

    public class LayoutLocalizationService: ILocalizationService<LayoutResource>
    {
        private readonly IStringLocalizer localizer;

        public LayoutLocalizationService(IStringLocalizerFactory factory)
        {
            var type = typeof(LayoutResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            localizer = factory.Create("LayoutResource", assemblyName.Name);
        }

        public LocalizedString GetLocalizedHtmlString(string key)
        {
            return localizer[key];
        }

        public LocalizedString GetLocalizedHtmlString(string key, string parameter)
        {
            return localizer[key, parameter];
        }
    }

}