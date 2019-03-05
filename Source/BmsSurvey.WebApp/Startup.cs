//  ------------------------------------------------------------------------------------------------
//   <copyright file="Startup.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.WebApp
{
    #region Using

    using System;
    using System.Linq;
    using System.Reflection;
    using Application.Infrastructure;
    using Application.Infrastructure.AutoMapper;
    using Application.Interfaces;
    using Application.Resources;
    using Application.Services;
    using Application.Surveys.Models;
    using Application.Users.Commands.CreateUser;
    using Application.Users.Queries.GetAllUsers;
    using AutoMapper;
    using Common.Constants;
    using Common.Interfaces;
    using Domain.Entities.Identity;
    using FluentValidation;
    using FluentValidation.AspNetCore;
    using Infrastructure;
    using Infrastructure.Automapper;
    using Infrastructure.Interfaces;
    using Infrastructure.Middlewares;
    using Infrastructure.Services;
    using MediatR;
    using MediatR.Pipeline;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Models;
    using Newtonsoft.Json.Serialization;
    using Persistence;
    using Persistence.Infrastructure;
    using Persistence.Interfaces;
    using Resources;
    using Serilog;
    using Services;

    #endregion

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.FromLogContext()
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Add AutoMapper
            //ToDo: remove second Automapper Profile
            services.AddAutoMapper(typeof(AutoMapperProfile).GetTypeInfo().Assembly,
                typeof(AutomapperProfilerWeb).GetTypeInfo().Assembly);

            //Services Registration
            services.AddSingleton<IEmailSender, MailSender>();
            services.AddSingleton<IMailNotificationService, MailSender>();
            services.AddSingleton<IPersister, AuditablePersister>();

            // Add Application services.
            services.AddSingleton<IStatusFactory, StatusFactory>();
            services.AddSingleton<IAnswerFactory, AnswerFactory>();
            services.AddSingleton<IIpProvider, IpProvider>();
            services.AddSingleton<ISupportedCulturesService>(
                new SupportedCulturesService(GlobalConstants.DefaultCultureId));

            services.AddScoped<ILocalizationUrlService, LocalizationUrlService>();
            services.AddScoped<IUserCreationMessageService, UserCreationMessageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICurrentPrincipalProvider, CurrentPrincipalProvider>();
            services.AddScoped<IAuditableDbContext, BmsSurveyDbContext>();
            services.AddScoped<IRatingControlTypeService, RatingControlTypeService>();
            services.AddScoped<ISurveyDto>(sp => SessionSurveyDto.GetSurveyDto(sp));

            services.AddTransient<BmsSurveyInitializer>();


            // Add MediatR
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddMediatR(typeof(GetAllUsersQuery).GetTypeInfo().Assembly);

            services.AddDbContext<BmsSurveyDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("BmsSurveyDatabase"))
            );


            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<BmsSurveyDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            // custome settings
            services.Configure<IdentityOptions>(options =>
            {
                options.Password = new PasswordOptions
                {
                    RequiredLength = 6
                };
                options.SignIn.RequireConfirmedEmail = true;
            });

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                // enables immediate logout, after updating the user's stat.
                options.ValidationInterval = TimeSpan.Zero;
            });

            /**** Localization configuration ****/
            services.AddSingleton<ILocalizationService<LayoutResource>, LayoutLocalizationService>();
            services.AddSingleton<ILocalizationService<MessageResource>, MessageLocalizationService>();


            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = GlobalConstants.SupportedCultures.Select(x => x.Value).ToList();

                    options.DefaultRequestCulture = new RequestCulture("bg", "bg");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                    options.RequestCultureProviders.Insert(0, new RouteDataRequestCultureProvider
                    {
                        IndexOfCulture = 1,
                        IndexofUICulture = 1
                    });
                    options.RequestCultureProviders.Insert(1, new QueryStringRequestCultureProvider());
                    options.RequestCultureProviders.Insert(2, new CookieRequestCultureProvider());
                });

            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("culture", typeof(LanguageRouteConstraint));
            });

            services.AddDistributedMemoryCache();
            int.TryParse(Configuration["SessionTimeout"], out var sessionTimeout);
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(sessionTimeout);
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.HttpOnly = true;
            });

            services.AddMvc(options => { options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); })
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        var assemblyName = new AssemblyName(typeof(LayoutResource).GetTypeInfo().Assembly.FullName);
                        return factory.Create("LayoutResource", assemblyName.Name);
                    };
                })
                .AddRazorPagesOptions(options => options.Conventions.Add(new LanguagePageRouteModelConvention()))
                .AddJsonOptions(options =>
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddSessionStateTempDataProvider()
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<CreateUserCommandValidator>();
                    ValidatorOptions.DisplayNameResolver = (type, memberInfo, expression) =>
                    {
                        var sp = services.BuildServiceProvider();
                        var localizationService = sp.GetService<ILocalizationService<LayoutResource>>();
                        var displayName = memberInfo.CustomAttributes.FirstOrDefault(attr => attr.AttributeType.Name == "DisplayAttribute")?
                                              .NamedArguments.FirstOrDefault(na => na.MemberName == "Name").TypedValue.Value.ToString() ?? memberInfo.Name;
                        return localizationService.GetLocalizedHtmlString(displayName);
                    };
                });



            //Serilog ILogger registation
            services.AddLogging(builder => { builder.AddSerilog(); });

            // Add Kendo UI services to the services container
            services.AddKendo();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Serilog - Ensure any buffered events are sent at shutdown
            appLifetime.ApplicationStopped.Register(Log.CloseAndFlush);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseRequestLocalization();

            app.UseSession();
            app.UseMiddleware<ApplicationErrorMiddleware>();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "areas",
                    "{culture=bg}/{area:exists}/{controller=Home}/{action}/{id?}"
                );

                routes.MapRoute(
                    "LocalizedDefault",
                    "{culture=bg}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}