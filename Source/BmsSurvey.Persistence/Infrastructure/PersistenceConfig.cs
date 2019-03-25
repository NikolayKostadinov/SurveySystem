using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Persistence.Infrastructure
{
    using BmsSurvey.Application.Interfaces;
    using BmsSurvey.Persistence.Interfaces;
    using Microsoft.Extensions.DependencyInjection;

    public static class PersistenceConfig
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddScoped<IAuditableDbContext, BmsSurveyDbContext>();
            services.AddScoped<IBmsSurveyDbContext, BmsSurveyDbContext>();
            return services;
        }
    }
}
