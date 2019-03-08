//-----------------------------------------------------------------------
// <copyright file="IsSurveyCompletedQueryHandlerTests.cs" company="Business Management System Ltd.">
//     Copyright "2019" (c) Business Management System Ltd. All rights reserved.
// </copyright>
// <author>Nikolay.Kostadinov</author>
// <convention>
// Test method convention
// [UnitOfWork]_[Scenario]_[ExpectedBehavior]
// </convention>
//-----------------------------------------------------------------------

namespace BmsSurvey.Application.Tests.CompletedSurvey.Queries
{
    #region Using 

    using System;
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.CompletedSurvey.Queries.IsSurveyCompeted;
    using Common.Abstract;
    using Domain.Entities;
    using Infrastructure;
    using Surveys.Commands;

    #endregion

    /// <summary>
    /// Summary description for IsSurveyCompletedQueryHandlerTests
    /// </summary>
    [TestFixture]
    public class IsSurveyCompletedQueryHandlerTests : QueryTestBase
    {
        private readonly IsSurveyCompletedQueryHandler queryHandler;

        public IsSurveyCompletedQueryHandlerTests()
        :base(new TestTimeProvider())
        {
            this.queryHandler = new IsSurveyCompletedQueryHandler(this.Context);
        }

        [Test]
        public void IsSurveyCompletedQueryHandlerCtor_IfBmsSurveyDbContextIsNull_ArgumentNullException()
        {

            // Act & Assert
            Assert.That(() => new IsSurveyCompletedQueryHandler(null),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("context"));
        }

        [Test]
        public async Task IsSurveyCompletedQueryHandler_NotCompletedSurvey_False()
        {
            //Arrange
            var query = new IsSurveyCompletedQuery(1,"127.0.0.1");
            //Act

            var result = await queryHandler.Handle(query, CancellationToken.None);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task IsSurveyCompletedQueryHandler_CompletedSurvey_True()
        {
            //Arrange
            var completedSurvey = new CompletedSurvey() {SurveyId = 1, IpAddress = "127.0.0.1"};
            this.Context.CompletedSurveys.Add(completedSurvey);
            this.Context.SaveChanges();
            var query = new IsSurveyCompletedQuery(1, "127.0.0.1");
            //Act

            var result = await queryHandler.Handle(query, CancellationToken.None);

            //Assert
            Assert.IsTrue(result);
            this.Context.CompletedSurveys.Remove(completedSurvey);
            this.Context.SaveChanges();
        }

        [Test]
        public async Task IsSurveyCompletedQueryHandler_NewIp_False()
        {
            //Arrange
            var completedSurvey = new CompletedSurvey() {SurveyId = 1, IpAddress = "127.0.0.1"};
            this.Context.CompletedSurveys.Add(completedSurvey);
            this.Context.SaveChanges();
            var query = new IsSurveyCompletedQuery(1, "127.0.0.2");
            //Act

            var result = await queryHandler.Handle(query, CancellationToken.None);

            //Assert
            Assert.IsFalse(result);
            this.Context.CompletedSurveys.Remove(completedSurvey);
            this.Context.SaveChanges();
        }

        [Test]
        public async Task IsSurveyCompletedQueryHandler_DeletedSurvey_False()
        {
            //Arrange
            var completedSurvey = new CompletedSurvey() { SurveyId = 3, IpAddress = "127.0.0.1" };
            this.Context.CompletedSurveys.Add(completedSurvey);
            this.Context.SaveChanges();
            var query = new IsSurveyCompletedQuery(3, "127.0.0.1");
            //Act

            var result = await queryHandler.Handle(query, CancellationToken.None);

            //Assert
            Assert.IsTrue(result);
            this.Context.CompletedSurveys.Remove(completedSurvey);
            this.Context.SaveChanges();
        }

        [Test]
        public async Task IsSurveyCompletedQueryHandler_InActiveSurvey_Faslse()
        {
            //Arrange
            var completedSurvey = new CompletedSurvey() { SurveyId = 2, IpAddress = "127.0.0.1" };
            this.Context.CompletedSurveys.Add(completedSurvey);
            this.Context.SaveChanges();
            var query = new IsSurveyCompletedQuery(2, "127.0.0.1");
            //Act

            var result = await queryHandler.Handle(query, CancellationToken.None);

            //Assert
            Assert.IsTrue(result);
            this.Context.CompletedSurveys.Remove(completedSurvey);
            this.Context.SaveChanges();
        }

    }
}