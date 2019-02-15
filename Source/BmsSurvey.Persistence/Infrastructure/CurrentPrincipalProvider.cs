namespace BmsSurvey.Persistence.Infrastructure
{
    using System.Security.Principal;
    using Interfaces;
    using Microsoft.AspNetCore.Http;

    public class CurrentPrincipalProvider : ICurrentPrincipalProvider
    {
        private readonly IHttpContextAccessor httpContext;

        public CurrentPrincipalProvider(IHttpContextAccessor httpContextParam)
        {
            this.httpContext = httpContextParam;
        }

        public IPrincipal GetCurrentPrincipal()
        {
            return this.httpContext?.HttpContext?.User;
        }
    }
}
