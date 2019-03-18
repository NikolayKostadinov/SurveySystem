//  ------------------------------------------------------------------------------------------------
//   <copyright file="CompletedSurveyFilterAttribute.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.WebApp.Infrastructure.Filters
{
    #region Using

    using System;
    using Application.CompletedSurvey.Queries.IsSurveyCompeted;
    using Domain.Entities.Identity;
    using Interfaces;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.DependencyInjection;

    #endregion

    public class CompletedSurveyFilterAttribute : ActionFilterAttribute
    {
        private IIpProvider ipProvider;
        private IMediator mediator;

        private void Initialize(ActionExecutingContext context)
        {
            // The only possible way for DI in ActionFilter (Constructor Injection is inpossible)
            mediator = context.HttpContext.RequestServices.GetService<IMediator>() ??
                       throw new ArgumentNullException(nameof(mediator));
            ipProvider = context.HttpContext.RequestServices.GetService<IIpProvider>() ??
                         throw new ArgumentNullException(nameof(ipProvider));
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!(context.HttpContext.User.IsInRole("Administrator") 
                || context.HttpContext.User.IsInRole("SurveySupporter")))
            {
                Initialize(context);

                if (int.TryParse(context.ActionArguments["id"].ToString(), out var surveyId))
                {
                    var isSurveyCompleted =
                        mediator.Send(new IsSurveyCompletedQuery(surveyId, ipProvider.GetIp())).Result;
                    if (isSurveyCompleted)
                    {
                        context.Result = new RedirectToActionResult("ThankYou", "Survey", new {area = ""});
                        return;
                    }
                }

                base.OnActionExecuting(context);
            }
        }
    }
}