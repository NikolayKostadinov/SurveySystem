using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BmsSurvey.WebApp.Infrastructure.Middleware
{
    using System.IO;
    using System.Text;
    using Kendo.Mvc.Infrastructure.Implementation;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;

    public class ClientAddressMiddleware
    {
        private readonly RequestDelegate next;

        public ClientAddressMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //First, get the incoming request
            var request = context.Request;
            if (request.Method.ToLower() == "post" && request.Form != null)
            {
                //request.Form = "testIp";
            }
            //Copy a pointer to the original response body stream
            var originalBodyStream = context.Response.Body;

            //Create a new memory stream...
            using (var responseBody = new MemoryStream())
            {
                //Continue down the Middleware pipeline, eventually returning to this class
                await next(context);
            }
        }
    }
}
