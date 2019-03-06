//-----------------------------------------------------------------------
// <copyright file="QuestionsForSurveyQueryHandlerTests.cs" company="Business Management System Ltd.">
//     Copyright "2019" (c) Business Management System Ltd. All rights reserved.
// </copyright>
// <author>Nikolay.Kostadinov</author>
// <convention>
// Test method convention
// [UnitOfWork]_[Scenario]_[ExpectedBehavior]
// </convention>
//-----------------------------------------------------------------------

namespace BmsSurvey.Application.Tests.Questions.Queries
{
    #region Using 

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Questions.Models;
    using Application.Questions.Queries.GetAllQuestionsForSurvey;
    using Application.Surveys.Models;
    using Exceptions;
    using Infrastructure;
    using NUnit.Framework;

    #endregion

    /// <summary>
    /// Summary description for QuestionsForSurveyQueryHandlerTests
    /// </summary>
    [TestFixture]
    public class QuestionsForSurveyQueryHandlerTests : QueryTestBase
    {
        [Test]
        public void QuestionsForSurveyQueryHandlerCtor_IfBmsSurveyDbContextIsNull_ArgumentNullException()
        {

            // Act & Assert
            Assert.That(() => new QuestionsForSurveyQueryHandler(null, this.Mapper),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("context"));
        }

        [Test]
        public void QuestionsForSurveyQueryHandlerCtor_IfIMapperIsNull_ArgumentNullException()
        {

            // Act & Assert
            Assert.That(() => new QuestionsForSurveyQueryHandler(this.Context, null),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("mapper"));
        }

        [Test]
        public async Task QuestionsForSurveyQueryHandler_ShouldReturnReport()
        {
            //Arrange
            var context = this.Context;
            var mapper = this.Mapper;
            var question = await context.Questions.FindAsync(1);

            //Act
            var query = new QuestionsForSurveyQuery() { SurveyId = 1 };
            var queryHandler = new QuestionsForSurveyQueryHandler(context, mapper);
            var result = await queryHandler.Handle(query, CancellationToken.None);

            //Assert
            var fResult = result.First(x => x.Id == 1);
            Assert.IsInstanceOf<IEnumerable<QuestionListViewModel>>(result);
            Assert.AreEqual(10, result.Count());
            Assert.AreEqual(question.DisplayNumber, fResult.DisplayNumber);
            Assert.AreEqual(question.QuestionType, fResult.QuestionType);
            Assert.AreEqual(question.Text, fResult.Text);
            Assert.AreEqual(question.CreatedFrom, fResult.CreatedFrom);
            Assert.AreEqual(question.ModifiedFrom, fResult.ModifiedFrom);
            Assert.AreEqual(question.ModifiedOn, fResult.ModifiedOn);
            Assert.AreEqual(question.CreatedFrom, fResult.CreatedFrom);
            Assert.AreEqual(question.QuestionType.ToString(), fResult.QuestionTypeStr);
        }


        [Test]
        public void QuestionsForSurveyQueryHandler_ExpiredSurvey_ShouldThrowNotFoundException()
        {
            //Arrange
            var context = this.Context;
            var mapper = this.Mapper;
           
            //Act
            var query = new QuestionsForSurveyQuery() { SurveyId = 2 };
            var queryHandler = new QuestionsForSurveyQueryHandler(context, mapper);

            //Assert
            Assert.That(async () => await queryHandler.Handle(query, CancellationToken.None),
                Throws.TypeOf<NotFoundException>()
                    .With.Property("Message").Contains("survey")
                    .With.Property("Message").Contains("2"));

        }

        [Test]
        public void QuestionsForSurveyQueryHandler_DeletedSurvey_ShouldThrowNotFoundException()
        {
            //Arrange
            var context = this.Context;
            var mapper = this.Mapper;
           
            //Act
            var query = new QuestionsForSurveyQuery() { SurveyId = 3 };
            var queryHandler = new QuestionsForSurveyQueryHandler(context, mapper);

            //Assert
            Assert.That(async () => await queryHandler.Handle(query, CancellationToken.None),
                Throws.TypeOf<NotFoundException>()
                    .With.Property("Message").Contains("survey")
                    .With.Property("Message").Contains("3"));

        }
    }
}