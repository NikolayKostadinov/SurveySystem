//  ------------------------------------------------------------------------------------------------
//   <copyright file="CultureController.cs" company="Business Management System Ltd.">
//       Copyright "2018" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.WebApp.Controllers
{
    #region Using

    using System;
    using Application.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    #endregion

    public class CultureController : Controller
    {
        private readonly ILocalizationUrlService localizationUrlService;
        private readonly ISupportedCulturesService supportedCulturesProvider;
        private readonly IUserService userService;

        public CultureController(ILocalizationUrlService localizationUrlService,
            IUserService userService,
            ISupportedCulturesService supportedCulturesServiceParam)
        {
            this.localizationUrlService = localizationUrlService ?? throw new ArgumentNullException(nameof(localizationUrlService));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));

            supportedCulturesProvider = supportedCulturesServiceParam ?? throw new ArgumentNullException(nameof(supportedCulturesServiceParam));
        }

        // GET: Culture
        public ActionResult Change(string returnUrl, string uICulture)
        {
            if (ModelState.IsValid)
            {
                if (supportedCulturesProvider.IsCultureSupported(uICulture)
                    && User.Identity.IsAuthenticated)
                {
                    userService.UpdateUserCulture(int.Parse(userService.GetUserId(User)), uICulture);
                }

                returnUrl = localizationUrlService.GetLocalizedUri(new Uri(returnUrl), uICulture).OriginalString;
            }

            return Redirect(returnUrl);
        }
    }


}