//-----------------------------------------------------------------------
// <copyright file="SurveyQuestionsQueryHandlerTests.cs" company="Business Management System Ltd.">
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
    using System.Collections.Concurrent;
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Answers.Models;
    using Application.Surveys.Models;
    using Application.Surveys.Queries.GetSurveyById;
    using Application.Surveys.Queries.GetSurveyQuestionsWithAnswers;
    using Exceptions;
    using Infrastructure;

    #endregion


    /// <summary>
    /// Summary description for SurveyQuestionsQueryHandlerTests
    /// </summary>
    [TestFixture]
    public class SurveyQuestionsQueryHandlerTests : QueryTestBase
    {
        [Test]
        public void SurveyQuestionsQueryHandlerCtor_IfBmsSurveyDbContextIsNull_ArgumentNullException()
        {

            // Act & Assert
            Assert.That(() => new SurveyQuestionsQueryHandler(null, new Mock<ISurveyDto>().Object, this.Mapper),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("context"));
        }

        [Test]
        public void SurveyQuestionsQueryHandlerCtor_IfISurveyDtoIsNull_ArgumentNullException()
        {

            // Act & Assert
            Assert.That(() => new SurveyQuestionsQueryHandler(this.Context, null, this.Mapper),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("surveyDto"));
        }

        [Test]
        public void SurveyQuestionsQueryHandlerCtor_IfIMappertIsNull_ArgumentNullException()
        {

            // Act & Assert
            Assert.That(() => new SurveyQuestionsQueryHandler(this.Context, new Mock<ISurveyDto>().Object, null),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("mapper"));
        }

        [Test]
        public void SurveyQuestionsQueryHandler_ShouldReturnReport()
        {
            //Arrange
            var context = this.Context;
            var mapper = this.Mapper;
            var stubSurveyDto = new Mock<ISurveyDto>();
                stubSurveyDto.Setup(x=>x.Answers).Returns(new Dictionary<int, AnswerViewModel> {{1,new AnswerViewModel{QuestionId = 1, Value = "1"}}});
            var dbQuestionFirst = context.Questions.Find(1);

            //Act
            var query = new SurveyQuestionsQuery(id: 1, pageNumber: 1);
            var queryHandler = new SurveyQuestionsQueryHandler(context, stubSurveyDto.Object, mapper);
            var result = queryHandler.Handle(query, CancellationToken.None).Result;

            //Assert
            Assert.IsInstanceOf<SurveyViewModel>(result);
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Questions.Count());
            Assert.AreEqual(1, result.PageNumber);
            Assert.IsFalse(result.IsLastPage);

            var firstQuestion = result.Questions.First(q => q.Id == 1);
            Assert.AreEqual(dbQuestionFirst.Id, firstQuestion.Id);
            Assert.AreEqual(dbQuestionFirst.DisplayNumber, firstQuestion.DisplayNumber);
            Assert.AreEqual(dbQuestionFirst.QuestionType, firstQuestion.QuestionType);
            Assert.AreEqual(true, firstQuestion.IsChecked);
            Assert.AreEqual(dbQuestionFirst.Text, firstQuestion.Text);

        }

        [Test]
        public void SurveyQuestionsQueryHandler_Page4_ShouldReturnIsLastPageTrue()
        {
            //Arrange
            var context = this.Context;
            var mapper = this.Mapper;
            var stubSurveyDto = new Mock<ISurveyDto>();
            //Act
            var query = new SurveyQuestionsQuery(id: 1, pageNumber: 4);
            var queryHandler = new SurveyQuestionsQueryHandler(context, stubSurveyDto.Object, mapper);
            var result = queryHandler.Handle(query, CancellationToken.None).Result;

            //Assert
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Проучване за Пътеводителя", result.SurveyTitle);
            Assert.IsInstanceOf<SurveyViewModel>(result);
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.PageNumber);
            Assert.IsTrue(result.IsLastPage);
        }

        [Test]
        public void SurveyQuestionsQueryHandler_ExpiredSurvey_ShouldThrowNotFoundException()
        {
            //Arrange
            var context = this.Context;
            var mapper = this.Mapper;
            var stubSurveyDto = new Mock<ISurveyDto>();

            //Act
            var query = new SurveyQuestionsQuery(id: 2, pageNumber: 1);
            var queryHandler = new SurveyQuestionsQueryHandler(context, stubSurveyDto.Object, mapper);
            
            //Assert
            Assert.That(async() => await queryHandler.Handle(query, CancellationToken.None),
                Throws.TypeOf<NotFoundException>()
                    .With.Property("Message").Contains("survey")
                    .With.Property("Message").Contains("2"));

        }

        [Test]
        public void SurveyQuestionsQueryHandler_DeletedSurvey_ShouldThrowNotFoundException()
        {
            //Arrange
            var context = this.Context;
            var mapper = this.Mapper;
            var stubSurveyDto = new Mock<ISurveyDto>();

            //Act
            var query = new SurveyQuestionsQuery(id: 3, pageNumber: 1);
            var queryHandler = new SurveyQuestionsQueryHandler(context, stubSurveyDto.Object, mapper);
            
            //Assert
            Assert.That(async() => await queryHandler.Handle(query, CancellationToken.None),
                Throws.TypeOf<NotFoundException>()
                    .With.Property("Message").Contains("survey")
                    .With.Property("Message").Contains("3"));

        }

        [Test]
        public void SurveyQuestionsQueryHandler_NowSurvey_ShouldThrowNotFoundException()
        {
            //Arrange
            var context = this.Context;
            var mapper = this.Mapper;
            var stubSurveyDto = new Mock<ISurveyDto>();

            //Act
            var query = new SurveyQuestionsQuery(id: 4, pageNumber: 1);
            var queryHandler = new SurveyQuestionsQueryHandler(context, stubSurveyDto.Object, mapper);
            
            //Assert
            Assert.That(async() => await queryHandler.Handle(query, CancellationToken.None),
                Throws.TypeOf<NotFoundException>()
                    .With.Property("Message").Contains("survey")
                    .With.Property("Message").Contains("4"));

        }

        [Test]
        public void SurveyQuestionsQueryHandler_Page1WithAnswersInSurveyDto_ShouldReturnValues()
        {
            //Arrange
            var context = this.Context;
            var mapper = this.Mapper;
            var stubSurveyDto = new Mock<ISurveyDto>();
            stubSurveyDto.Setup(x => x.Answers).Returns(new Dictionary<int, AnswerViewModel>
            {
                {1, new AnswerViewModel { QuestionId = 1,Value = "3"}},
                {2, new AnswerViewModel { QuestionId = 2,Value = "True"}},
                {3, new AnswerViewModel { QuestionId = 3,Value = "2"}},
                {5, new AnswerViewModel { QuestionId = 5,Value = "False"}}
            });
            //Act
            var query = new SurveyQuestionsQuery(id: 1, pageNumber: 1);
            var queryHandler = new SurveyQuestionsQueryHandler(context, stubSurveyDto.Object, mapper);
            var result = queryHandler.Handle(query, CancellationToken.None).Result;

            //Assert
            Assert.AreEqual(1, result.PageNumber);
            Assert.AreEqual(4, result.Questions.Count());
            Assert.AreEqual(3.ToString(), result.Questions.First(x => x.DisplayNumber == 1).Value);
            Assert.AreEqual(true.ToString(), result.Questions.First(x => x.DisplayNumber == 2).Value);
            Assert.AreEqual(2.ToString(), result.Questions.First(x => x.DisplayNumber == 3).Value);
            Assert.AreEqual(false.ToString(), result.Questions.First(x => x.DisplayNumber == 4).Value);

        }
    }
}