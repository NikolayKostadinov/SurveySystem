//  ------------------------------------------------------------------------------------------------
//   <copyright file="CookieAcceptFilter.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.WebApp.Infrastructure.Filters
{
    #region Using

    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    #endregion

    public class CookieAcceptFilterAttribute : ActionFilterAttribute
    {
        private ITrackingConsentFeature consentFeature;

        private void Initialize(ActionExecutingContext context)
        {
            // The only possible way for DI in ActionFilter (Constructor Injection is inpossible)
            consentFeature = context.HttpContext.Features.Get<ITrackingConsentFeature>();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Initialize(context);
            var isCookieApproved = consentFeature?.CanTrack ?? true;

            if (!isCookieApproved)
            {
                context.Result = new RedirectToActionResult("AcceptCookies", "Home", new {area = "", returnUrl = context.HttpContext.Request.GetDisplayUrl()});
                return;
            }


            base.OnActionExecuting(context);
        }
    }
}