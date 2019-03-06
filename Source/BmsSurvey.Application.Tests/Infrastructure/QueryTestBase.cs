namespace BmsSurvey.Application.Tests.Infrastructure
{
    using System;
    using AutoMapper;
    using Common.Abstract;
    using Moq;
    using Persistence;
    using Surveys.Commands;

    public class QueryTestBase : IDisposable
    {
        private readonly BmsSurveyDbContext context;
        private readonly IMapper mapper;

        public QueryTestBase(TimeProvider testTimeProvider = null)
        {
            if (testTimeProvider != null) TimeProvider.Current = testTimeProvider;

            this.context = BmsSurveyContextFactory.Create();
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