//  ------------------------------------------------------------------------------------------------
//   <copyright file="CommandTestBase.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Tests.Infrastructure
{
    using System;
    using AutoMapper;
    using NUnit.Framework.Constraints;
    using Persistence;
    using Tests;

    public class CommandTestBase : IDisposable
    {
        private readonly BmsSurveyDbContext context;
        private readonly IMapper mapper;

        public CommandTestBase()
        {
            this.context = BmsSurveyContextFactory.Create(empty: true);
            this.mapper = AutoMapperFactory.Create();
        }

        public void Dispose()
        {
            BmsSurveyContextFactory.Destroy(context);
        }

        protected BmsSurveyDbContext Context => this.context;
        protected IMapper Mapper => this.mapper;
    }
}