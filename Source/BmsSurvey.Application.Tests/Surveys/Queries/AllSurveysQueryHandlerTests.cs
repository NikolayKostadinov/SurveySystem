//-----------------------------------------------------------------------
// <copyright file="AllSurveysQueryHandlerTests.cs" company="Business Management System Ltd.">
//     Copyright "2019" (c) Business Management System Ltd. All rights reserved.
// </copyright>
// <author>Nikolay.Kostadinov</author>
// <convention>
// Test method convention
// [UnitOfWork]_[Scenario]_[ExpectedBehavior]
// </convention>
//-----------------------------------------------------------------------

namespace BmsSurvey.Application.Tests.Surveys.Queries
{
    #region Using 

    using System;
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Application.Surveys.Queries.GetAllSurveys;
    using Common.Abstract;
    using Domain.Entities.Identity;
    using Infrastructure;
    using Microsoft.AspNetCore.Identity;
    using Persistence;

    #endregion


    /// <summary>
    /// Summary description for AllSurveysQueryHandlerTests
    /// </summary>
    [TestFixture]
    public class AllSurveysQueryHandlerTests : QueryTestBase
    {
        [Test]
        public void AllSurveysQueryHandlerCtor_IfBmsSurveyDbContextIsNull_ArgumentNullException() {

            // Act & Assert
            Assert.That(() => new AllSurveysQueryHandler(null, this.Mapper), 
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("context"));
        }

        [Test]
        public void AllSurveysQueryHandlerCtor_IfIMapperIsNull_ArgumentNullException() {

            // Act & Assert
            Assert.That(() => new AllSurveysQueryHandler(this.Context, null), 
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("mapper"));
        }

        [Test]
        public void AllSurveysQueryHandler_ShouldReturnReport()
        {
            //Arrange
            var context = this.Context;
            var mapper = this.Mapper;

            //Act
            var query = new AllSurveysQuery();
            var queryHandler = new AllSurveysQueryHandler(context, mapper);
            var result = queryHandler.Handle(query, CancellationToken.None).Result.ToArray();

            //Assert
            Assert.AreEqual(2, result.Count(), "Should return 2 records!");
            Assert.IsTrue(result.Any(r => r.SurveyTitle.Contains("Проучване за Пътеводителя")), "Should return normal record!");
            Assert.IsTrue(result.Any(r => r.SurveyTitle.Contains("Изтекло проучване")), "Should return expired record!");
            Assert.IsFalse(result.Any(r => r.SurveyTitle.Contains("Изтрито проучване")), "Should not return deleted record!");
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual("Test", result[0].Description);
            Assert.AreEqual(4, result[0].PageSize);
            Assert.AreEqual(TimeProvider.Current.UtcNow.Date, result[0].ActiveFrom);
            Assert.AreEqual(TimeProvider.Current.UtcNow.AddDays(30).Date, result[0].ActiveTo);
            Assert.AreEqual(true, result[0].IsActive);
            Assert.AreEqual("TestUser", result[0].CreatedFrom);
            Assert.AreEqual(null, result[0].ModifiedFrom);
            Assert.AreEqual(null, result[0].ModifiedOn);

        }
    }
}