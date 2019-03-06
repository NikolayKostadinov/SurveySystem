//  ------------------------------------------------------------------------------------------------
//   <copyright file="PageModelBase.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.WebApp.Pages
{
    using System;
    using MediatR;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.DependencyInjection;

    public class PageModelBase:PageModel
    {
        private IMediator mediator;

        protected IMediator Mediator => mediator ?? (mediator = HttpContext.RequestServices.GetService<IMediator>());
    }
}