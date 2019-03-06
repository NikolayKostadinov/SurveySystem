//-----------------------------------------------------------------------
// <copyright file="CreateQuestionCommandHandlerTests.cs" company="Business Management System Ltd.">
//     Copyright "2019" (c) Business Management System Ltd. All rights reserved.
// </copyright>
// <author>Nikolay.Kostadinov</author>
// <convention>
// Test method convention
// [UnitOfWork]_[Scenario]_[ExpectedBehavior]
// </convention>
//-----------------------------------------------------------------------

using System;
using BmsSurvey.Common.Abstract;

namespace BmsSurvey.Application.Tests.Questions.Commands
{
    #region Using 

    using System;
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mime;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Questions.Commands.CreateQuestion;
    using Common.Abstract;
    using Domain.Entities;
    using Infrastructure;

    #endregion

    /// <summary>
    /// Summary description for CreateQuestionCommandHandlerTests
    /// </summary>
    [TestFixture]
    public class CreateQuestionCommandHandlerTests : CommandTestBase
    {
        private CreateQuestionCommandHandler commandHandler;

        public CreateQuestionCommandHandlerTests()
        {
            this.commandHandler = new CreateQuestionCommandHandler(this.Context, this.Mapper);
            var survey = new Survey()
            {
                Id = 1,
                SurveyTitle = "TestCommand",
                ActiveFrom = TimeProvider.Current.UtcNow.AddDays(-1),
                ActiveTo = TimeProvider.Current.UtcNow,
                PageSize = 3,
                Description = "Test"
            };
            this.Context.Surveys.Add(survey);
            this.Context.SaveChanges();
        }

        [Test]
        public void CreateQuestionCommandHandlerCtor_IfBmsSurveyDbContextIsNull_ArgumentNullException()
        {

            // Act & Assert
            Assert.That(() => new CreateQuestionCommandHandler(null, this.Mapper),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("context"));
        }

        [Test]
        public void CreateQuestionCommandHandlerCtor_IfIMapperIsNull_ArgumentNullException()
        {

            // Act & Assert
            Assert.That(() => new CreateQuestionCommandHandler(this.Context, null),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("mapper"));
        }

        [Test]
        public async Task CreateQuestionCommandHandler_NewSurvey_ShouldCreateSurvey()
        {
            // Arrange
            TimeProvider.Current = new TestTimeProvider();
            var command = new CreateQuestionCommand {
                DisplayNumber = 1,
                Text = "TestCommand",
                SurveyId = 1,
                QuestionType = QuestionType.YesOrNo
            };

            // Act
            await commandHandler.Handle(command, CancellationToken.None);
            var question = await this.Context.Questions.FindAsync(1);
            
            // Assert
            Assert.AreEqual(1, this.Context.Questions.Count());
            Assert.IsFalse(question.IsDeleted);
            Assert.AreEqual(command.DisplayNumber, question.DisplayNumber);
            Assert.AreEqual(command.Text, question.Text);
            Assert.AreEqual(command.QuestionType, question.QuestionType);
            Assert.AreEqual(command.SurveyId,question.SurveyId);
            Assert.AreEqual(TimeProvider.Current.UtcNow, question.CreatedOn);
            Assert.AreEqual("TestUser", question.CreatedFrom);
        }
    }
}