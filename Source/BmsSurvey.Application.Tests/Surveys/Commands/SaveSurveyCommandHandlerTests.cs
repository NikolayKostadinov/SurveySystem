//-----------------------------------------------------------------------
// <copyright file="SaveSurveyCommandHandlerTests.cs" company="Business Management System Ltd.">
//     Copyright "2019" (c) Business Management System Ltd. All rights reserved.
// </copyright>
// <author>Nikolay.Kostadinov</author>
// <convention>
// Test method convention
// [UnitOfWork]_[Scenario]_[ExpectedBehavior]
// </convention>
//-----------------------------------------------------------------------

namespace BmsSurvey.Application.Tests.Surveys.Commands
{
    #region Using 

    using System;
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;
    using Answers.Models;
    using Application.Surveys.Commands.DeleteSurvey;
    using Application.Surveys.Commands.SaveSurvey;
    using Application.Surveys.Models;
    using Common.Abstract;
    using Domain.Abstract;
    using Domain.Entities;
    using Domain.Entities.Answers;
    using Exceptions;
    using Infrastructure;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Services;

    #endregion

    /// <summary>
    /// Summary description for SaveSurveyCommandHandlerTests
    /// </summary>
    [TestFixture]
    public class SaveSurveyCommandHandlerTests : QueryTestBase
    {
        private readonly SaveSurveyCommandHandler commandHandler;
        private readonly List<AnswerViewModel> answers;
        public SaveSurveyCommandHandlerTests()
        : base(new TestTimeProvider())
        {
            this.answers = new List<AnswerViewModel>
            {
                new AnswerViewModel {QuestionId = 1, Value = "3"},
                new AnswerViewModel {QuestionId = 2, Value = "True"},
                new AnswerViewModel {QuestionId = 3, Value = "2"},
                new AnswerViewModel {QuestionId = 5, Value = "False"}
            };

            var stubSurveyDto = new Mock<ISurveyDto>();
            stubSurveyDto.Setup(x => x.Answers).Returns(this.answers.ToDictionary(x => x.QuestionId));

            var stubAnswerFactory = new Mock<IAnswerFactory>();
            stubAnswerFactory.Setup(x => x.GetAnswer(It.IsAny<QuestionType>(), It.IsAny<AnswerViewModel>()))
                .Returns((QuestionType qt, AnswerViewModel avm) => new FreeTextAnswer() { QuestionId = avm.QuestionId, Value = avm.Value });

            this.commandHandler = new SaveSurveyCommandHandler(this.Context, stubSurveyDto.Object, stubAnswerFactory.Object);
        }

        [Test]
        public void SaveSurveyCommandHandlerCtor_IfBmsSurveyDbContextIsNull_ArgumentNullException()
        {
            var stubSurveyDto = new Mock<ISurveyDto>();
            var stubAnswerFactory = new Mock<IAnswerFactory>();
            // Act & Assert
            Assert.That(() => new SaveSurveyCommandHandler(null, stubSurveyDto.Object, stubAnswerFactory.Object),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("context"));
        }

        [Test]
        public void SaveSurveyCommandHandlerCtor_IfISurveyDtoIsNull_ArgumentNullException()
        {
            var stubSurveyDto = new Mock<ISurveyDto>();
            var stubAnswerFactory = new Mock<IAnswerFactory>();
            // Act & Assert
            Assert.That(() => new SaveSurveyCommandHandler(this.Context, null, stubAnswerFactory.Object),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("surveyDto"));
        }

        [Test]
        public void SaveSurveyCommandHandlerCtor_IfIAnswerFactoryIsNull_ArgumentNullException()
        {
            var stubSurveyDto = new Mock<ISurveyDto>();
            var stubAnswerFactory = new Mock<IAnswerFactory>();
            // Act & Assert
            Assert.That(() => new SaveSurveyCommandHandler(this.Context, stubSurveyDto.Object, null),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("answerFactory"));
        }

        [Test]
        public async Task SaveSurveyCommandHandler_ExistSurvey_ShouldPersistAnswers()
        {
            // Arrange
            var surveyId = 1;
            var userName = "TestUser";
            var surveyEmail = "test@test.com";
            var surveyIpAddress = "111.111.111.111";

            var command =
                new SaveSurveyCommand(
                id: surveyId,
                userName: userName,
                eMail: surveyEmail,
                ipAddress: surveyIpAddress);

            // Act
            await commandHandler.Handle(command, CancellationToken.None);
            var survey = await this.Context.Surveys
                .Include(s => s.CompletedSurveys)
                .Include(s => s.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(s => s.Id == 1);
            var dbAnswers = survey.Questions.SelectMany(q => q.Answers).ToArray();
            var completedSurvey = survey.CompletedSurveys.First();

            // Assert
            Assert.AreEqual(4, answers.Count);
            Assert.AreEqual(TimeProvider.Current.UtcNow, dbAnswers[0].CreatedOn);
            Assert.AreEqual(userName, dbAnswers[0].CreatedFrom);
            Assert.AreEqual(answers[0].Value, ((FreeTextAnswer)dbAnswers[0]).Value);
            Assert.AreEqual(answers[1].Value, ((FreeTextAnswer)dbAnswers[1]).Value);
            Assert.AreEqual(answers[2].Value, ((FreeTextAnswer)dbAnswers[2]).Value);
            Assert.AreEqual(answers[3].Value, ((FreeTextAnswer)dbAnswers[3]).Value);
            Assert.AreEqual(surveyId,completedSurvey.SurveyId);
            Assert.AreEqual(surveyEmail,completedSurvey.Email);
            Assert.AreEqual(surveyIpAddress,completedSurvey.IpAddress);
        }

        [Test]
        public void SaveSurveyCommandHandler_IfSurveyIsNotActive_ShouldThrowNotFoundException()
        {
            // Arrange
            var surveyId = 2;
            var userName = "TestUser";
            var surveyEmail = "test@test.com";
            var surveyIpAddress = "111.111.111.111";

            var command =
                new SaveSurveyCommand(
                    id: surveyId,
                    userName: userName,
                    eMail: surveyEmail,
                    ipAddress: surveyIpAddress);

            //Assert
            Assert.That(async () => await commandHandler.Handle(command, CancellationToken.None),
                Throws.TypeOf<NotFoundException>()
                    .With.Property("Message").Contains("survey")
                    .With.Property("Message").Contains("2"));
        }

        [Test]
        public void SaveSurveyCommandHandler_IfSurveyIsDeleted_ShouldThrowNotFoundException()
        {
            // Arrange
            var surveyId = 3;
            var userName = "TestUser";
            var surveyEmail = "test@test.com";
            var surveyIpAddress = "111.111.111.111";

            var command =
                new SaveSurveyCommand(
                    id: surveyId,
                    userName: userName,
                    eMail: surveyEmail,
                    ipAddress: surveyIpAddress);

            //Assert
            Assert.That(async () => await commandHandler.Handle(command, CancellationToken.None),
                Throws.TypeOf<NotFoundException>()
                    .With.Property("Message").Contains("survey")
                    .With.Property("Message").Contains("3"));
        }
    }
}