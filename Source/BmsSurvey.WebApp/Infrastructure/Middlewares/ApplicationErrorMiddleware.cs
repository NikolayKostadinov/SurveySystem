//  ------------------------------------------------------------------------------------------------
//   <copyright file="ErrorHandlingMiddleware.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.WebApp.Infrastructure.Middlewares
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Common.Interfaces;
    using Extensions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Resources;

    #endregion

    public class ApplicationErrorMiddleware
    {
        private readonly IConfiguration config;
        private readonly IEmailSender mailSender;
        private readonly RequestDelegate next;
        private readonly ILogger<ApplicationErrorMiddleware> logger;

        public ApplicationErrorMiddleware(RequestDelegate next, 
            IConfiguration config, 
            IEmailSender mailSender, 
            ILogger<ApplicationErrorMiddleware> logger)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            // other dependencies
            this.config = config ?? throw new ArgumentNullException(nameof(config));
            this.mailSender = mailSender ?? throw new ArgumentNullException(nameof(mailSender));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorDescription = await PrepareErrorDescriptionAsync(context, exception);
            
            this.logger.LogError(errorDescription);

            await SendMailIfNeededAsync(exception, errorDescription);

            await PrepareResponseAsync(context);
        }

        private async Task PrepareResponseAsync(HttpContext context)
        {
            var result = new RedirectResult(context.Request.GetEncodedUrl());
            context.Session.Set("Error", UTF8Encoding.UTF8.GetBytes("FAILED_OPERATION"));
            await context.WriteResultAsync(result);
        }

        private async Task SendMailIfNeededAsync(Exception exception, string errorDesctiption)
        {
            var mailableExceptions = config.GetSection("MailableExceptions").Get<List<string>>() ?? new List<string>();
            if (mailableExceptions.Any(x => x == exception.GetType().Name))
            {
                var email = config.GetValue<string>("SupportEmail");

                await mailSender.SendEmailAsync(email, "Error in BmsSurveySystem!",
                    errorDesctiption.Replace(Environment.NewLine, "<br/>"));
            }
        }

        private static async Task<string> PrepareErrorDescriptionAsync(HttpContext context, Exception exception)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"User: {context.User?.Identity.Name}");
            sb.AppendLine($"Host IP: {context.Connection.RemoteIpAddress.MapToIPv4()}");
            sb.AppendLine($"Method: \"{context.Request.Method}\"");
            sb.AppendLine($"Url: {context.Request.GetEncodedUrl()}");
            if (context.Request.Method.ToLower() == "post")
            {
                if (context.Request.ContentType == "application/x-www-form-urlencoded")
                {
                    var form = await context.Request.ReadFormAsync();
                    sb.AppendLine(
                        $"Form: {Environment.NewLine}{string.Join(Environment.NewLine, form.Select(x => $"\t{x.Key} : {x.Value}"))}");
                }
                else
                {
                    sb.AppendLine(
                        $"Body: {Environment.NewLine}{await context.Request.GetRawBodyStringAsync()}");
                }
            }

            sb.AppendLine($"Exception: {Environment.NewLine}{exception}");
            return sb.ToString();
        }
    }
}