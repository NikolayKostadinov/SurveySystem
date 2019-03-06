//-----------------------------------------------------------------------
// <copyright file="DeleteQuestionCommandHandlerTests.cs" company="Business Management System Ltd.">
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
    using Application.Questions.Commands.DeleteQuestion;
    using Common.Abstract;
    using Domain.Entities;
    using Infrastructure;
    using Surveys.Commands;
    using TestTimeProvider = Infrastructure.TestTimeProvider;

    #endregion

    /// <summary>
    /// Summary description for DeleteQuestionCommandHandlerTests
    /// </summary>
    [TestFixture]
    public class DeleteQuestionCommandHandlerTests : CommandTestBase
    {
        private DeleteQuestionCommandHandler commandHandler;

        public DeleteQuestionCommandHandlerTests()
        {
            this.commandHandler = new DeleteQuestionCommandHandler(this.Context);
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
                        QuestionType = QuestionType.YesOrNo
                    }
                }
            };


            this.Context.Surveys.Add(survey);
            this.Context.SaveChanges();
        }

        [Test]
        public void DeleteQuestionCommandHandlerCtor_IfBmsSurveyDbContextIsNull_ArgumentNullException()
        {

            // Act & Assert
            Assert.That(() => new DeleteQuestionCommandHandler(null),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("context"));
        }

        [Test]
        public async Task DeleteSurveyCommandHandler_ExistSurvey_ShouldDeleteSurvey()
        {
            // Arrange
            TimeProvider.Current = new TestTimeProvider();
            var command = new DeleteQuestionCommand
            {
                Id = 1,
            };

            // Act
            await commandHandler.Handle(command, CancellationToken.None);
            var question = await this.Context.Questions.FindAsync(1);

            // Assert
            Assert.IsTrue(question.IsDeleted);
            Assert.AreEqual(TimeProvider.Current.UtcNow, question.DeletedOn);
            Assert.AreEqual("TestUser", question.DeletedFrom);
        }
    }
}