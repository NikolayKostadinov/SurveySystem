//-----------------------------------------------------------------------
// <copyright file="GetSurveyByIdQueryHandlerTests.cs" company="Business Management System Ltd.">
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
    using Application.Surveys.Models;
    using Application.Surveys.Queries.GetSurveyById;
    using Infrastructure;

    #endregion

    /// <summary>
    /// Summary description for GetSurveyByIdQueryHandlerTests
    /// </summary>
    [TestFixture]
    public class GetSurveyByIdQueryHandlerTests : QueryTestBase
    {
        [Test]
        public void GetSurveyByIdQueryHandlerCtor_IfBmsSurveyDbContextIsNull_ArgumentNullException()
        {

            // Act & Assert
            Assert.That(() => new GetSurveyByIdQueryHandler(null, this.Mapper),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("context"));
        }

        [Test]
        public void GetSurveyByIdQueryHandlerCtor_IfIMapperIsNull_ArgumentNullException()
        {

            // Act & Assert
            Assert.That(() => new GetSurveyByIdQueryHandler(this.Context, null),
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
            var query = new GetSurveyByIdQuery(id:1);
            var queryHandler = new GetSurveyByIdQueryHandler(context, mapper);
            var result = queryHandler.Handle(query, CancellationToken.None).Result;

            //Assert
            Assert.IsInstanceOf<SurveySimpleViewModel>(result);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.IsTrue(result.SurveyTitle.Contains("Проучване за Пътеводителя"));
        }
    }
}