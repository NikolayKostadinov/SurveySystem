//-----------------------------------------------------------------------
// <copyright file="EditQuestionCommandHandlerTest.cs" company="Business Management System Ltd.">
//     Copyright "2019" (c) Business Management System Ltd. All rights reserved.
// </copyright>
// <author>Nikolay.Kostadinov</author>
// <convention>
// Test method convention
// [UnitOfWork]_[Scenario]_[ExpectedBehavior]
// </convention>
//-----------------------------------------------------------------------

namespace BmsSurvey.Application.Tests.Questions.Commands
{
    #region Using 

    using System;
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Questions.Commands.EditQuestion;
    using Common.Abstract;
    using Domain.Entities;
    using Exceptions;
    using Infrastructure;

    #endregion

    /// <summary>
    /// Summary description for EditQuestionCommandHandlerTest
    /// </summary>
    [TestFixture]
    public class EditQuestionCommandHandlerTest : CommandTestBase
    {
        private EditQuestionCommandHandler commandHandler;

        public EditQuestionCommandHandlerTest()
        {
            this.commandHandler = new EditQuestionCommandHandler(this.Context, this.Mapper);
            var survey = new Survey()
            {
                Id = 1,
                SurveyTitle = "TestCommand",
                ActiveFrom = TimeProvider.Current.UtcNow.AddDays(-1),
                ActiveTo = TimeProvider.Current.UtcNow,
                PageSize = 3,
                Description = "Test",
                Questions = new List<Question>
                {
                    new Question
                    {
                        Id = 1,
                        DisplayNumber=1,
                        Text = "InitialText",
                        QuestionType = QuestionType.YesOrNo
                    }
                }
            };


            this.Context.Surveys.Add(survey);
            this.Context.SaveChanges();
        }

        [Test]
        public void EditSurveyCommandHandlerCtor_IfBmsSurveyDbContextIsNull_ArgumentNullException()
        {

            // Act & Assert
            Assert.That(() => new EditQuestionCommandHandler(null, this.Mapper),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("context"));
        }

        [Test]
        public void EditSurveyCommandHandlerCtor_IfIMapperIsNull_ArgumentNullException()
        {

            // Act & Assert
            Assert.That(() => new EditQuestionCommandHandler(this.Context, null),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("mapper"));
        }

        [Test]
        public async Task EditQuestionCommandHandler_ValidQuestion_ShouldUpdateSurvey()
        {
            // Arrange
            TimeProvider.Current = new TestTimeProvider();
            var command = new EditQuestionCommand
            {
                Id = 1,
                DisplayNumber = 2,
                Text = "ChangedText",
                QuestionType = QuestionType.Rate1to5Stars
            };

            var surveyId = (await this.Context.Questions.FindAsync(1)).SurveyId;

            // Act
            await commandHandler.Handle(command, CancellationToken.None);
            var question = await this.Context.Questions.FindAsync(1);

            // Assert
            Assert.IsFalse(question.IsDeleted);
            Assert.AreEqual(command.DisplayNumber, question.DisplayNumber);
            Assert.AreEqual(command.QuestionType, question.QuestionType);
            Assert.AreEqual(command.Text, question.Text);
            Assert.AreEqual(command.Text, question.Text);
            Assert.AreEqual(surveyId, question.SurveyId);
            Assert.AreEqual(TimeProvider.Current.UtcNow, question.ModifiedOn);
            Assert.AreEqual("TestUser", question.ModifiedFrom);
        }

        [Test]
        public void EditQuestionCommandHandler_IfQuestionNotFound_ShouldThrowNotFoundException()
        {
            //Arrange
            var command = new EditQuestionCommand
            {
                Id = 2,
                DisplayNumber = 2,
                Text = "ChangedText",
                QuestionType = QuestionType.Rate1to5Stars
            };

            //Act & Assert
            Assert.That(async () => await this.commandHandler.Handle(command, CancellationToken.None),
                Throws.TypeOf<NotFoundException>()
                    .With.Property("Message").Contains("question")
                    .With.Property("Message").Contains("2"));
        }

        [Test]
        public void EditQuestionCommandHandler_IfQuestionIsDeleted_ShouldThrowNotFoundException()
        {
            //Arrange
            var deletedQuestion = new Question()
            {
                Id = 2,
                DisplayNumber = 2,
                Text = "ChangedText",
                QuestionType = QuestionType.Rate1to5Stars,
                SurveyId = 1,
                IsDeleted = true,
            };
            this.Context.Questions.Add(deletedQuestion);
            this.Context.SaveChanges();

            var command = new EditQuestionCommand
            {
                Id = 2,
                DisplayNumber = 2,
                Text = "ChangedText",
                QuestionType = QuestionType.Rate1to5Stars
            };

            //Act & Assert
            Assert.That(async () => await this.commandHandler.Handle(command, CancellationToken.None),
                Throws.TypeOf<NotFoundException>()
                    .With.Property("Message").Contains("question")
                    .With.Property("Message").Contains("2"));

        }
    }
}