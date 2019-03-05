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
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Application.Users.Commands.ChangeUserCulture;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    #endregion

    public class CultureController : BaseController
    {
        private readonly ILocalizationUrlService localizationUrlService;

        public CultureController(ILocalizationUrlService localizationUrlService)
        {
            this.localizationUrlService = localizationUrlService ?? throw new ArgumentNullException(nameof(localizationUrlService));
        }

        // GET: Culture
        public async Task<ActionResult> Change(string returnUrl, string uICulture)
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var command = new ChangeUserCultureCommand(User, uICulture);
                    await this.Mediator.Send(command);
                }

                returnUrl = localizationUrlService.GetLocalizedUri(new Uri(returnUrl), uICulture).OriginalString;
            }

            return Redirect(returnUrl);
        }
    }
}