namespace BmsSurvey.WebApp.Infrastructure
{
    using System;
    using System.Threading.Tasks;
    using Common.Constants;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Localization;

    public class RouteDataRequestCultureProvider : RequestCultureProvider
    {
        public int IndexOfCulture { get; set; }
        public int IndexofUICulture { get; set; }

        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            string culture = null;
            string uiCulture = null;

            var twoLetterCultureName = httpContext.Request.Path.Value.Split('/')[IndexOfCulture]?.ToString();
            var twoLetterUICultureName = httpContext.Request.Path.Value.Split('/')[IndexofUICulture]?.ToString();

            if (twoLetterCultureName == "bg")
                culture = "bg";
            else if (twoLetterCultureName == "en")
                culture = uiCulture = "en";

            if (twoLetterUICultureName == "bg")
                culture = "bg";
            else if (twoLetterUICultureName == "en")
                culture = uiCulture = "en";

            if (culture == null && uiCulture == null)
                uiCulture = culture = GlobalConstants.DefaultCultureId;
                //return NullProviderCultureResult;

            if (culture != null && uiCulture == null)
                uiCulture = culture;

            if (culture == null && uiCulture != null)
                culture = uiCulture;


            var providerResultCulture = new ProviderCultureResult(culture, uiCulture);

            return Task.FromResult(providerResultCulture);
        }
    }
}
