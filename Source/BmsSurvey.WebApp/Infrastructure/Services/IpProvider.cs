using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BmsSurvey.WebApp.Infrastructure.Services
{
    using Interfaces;
    using Microsoft.AspNetCore.Http;

    public class IpProvider:IIpProvider
    {
        private readonly IHttpContextAccessor httpAccessor;

        public IpProvider(IHttpContextAccessor httpAccessor)
        {
            this.httpAccessor = httpAccessor ?? throw new ArgumentNullException(nameof(httpAccessor));
        }

        public string GetIp() => this.httpAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
    }
}
