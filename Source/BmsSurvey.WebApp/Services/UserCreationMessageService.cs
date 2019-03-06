using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BmsSurvey.WebApp.Services
{
    using System.Reflection;
    using System.Text.Encodings.Web;
    using System.Web;
    using Application.Interfaces;
    using Application.Notifications.Models;
    using Domain.Entities.Identity;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Infrastructure;
    using Microsoft.Extensions.Localization;
    using Resources;

    public class UserCreationMessageService : IUserCreationMessageService
    {
        private readonly IStringLocalizer layoutLocalizer;
        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly LinkGenerator linkGenerator;

        public UserCreationMessageService(UserManager<User> userManager, 
            IStringLocalizerFactory stringLocalizerFactory, 
            IHttpContextAccessor httpContextAccessor,
            LinkGenerator linkGenerator)
        {
            var type = typeof(LayoutResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            this.layoutLocalizer = stringLocalizerFactory.Create("LayoutResource", assemblyName.Name);
            this.userManager = userManager??throw new ArgumentNullException(nameof(userManager)); ;
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            this.linkGenerator = linkGenerator ?? throw new ArgumentNullException(nameof(linkGenerator));
        }

        public async Task<Message> GetMessageAsync(User user)
        {
            var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var httpContext = this.httpContextAccessor.HttpContext;
            var callbackUrlPath = this.linkGenerator.GetPathByPage(
                httpContext: httpContext, 
                page: "/Account/ConfirmEmail", 
                handler: null, 
                values: new {userId = user.Id, code = code, area = "Identity"});
            var callbackUrl =
                $"{httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.PathBase}{callbackUrlPath}";

            var message = new Message()
            {
                To = user.Email,
                Subject = layoutLocalizer["CONFIRM_YOUR_EMAIL"],
                Body = layoutLocalizer["CONFIRM_YOUR_EMAIL_TEXT", HtmlEncoder.Default.Encode(callbackUrl)]
            };

            return message;
        }
    }
}
