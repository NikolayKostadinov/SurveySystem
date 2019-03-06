//-----------------------------------------------------------------------
// <copyright file="EditSurveyCommandHandlerTests.cs" company="Business Management System Ltd.">
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
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Surveys.Commands.EditSurvey;
    using Common.Abstract;
    using Domain.Entities;
    using Exceptions;
    using Infrastructure;

    #endregion


    /// <summary>
    /// Summary description for EditSurveyCommandHandlerTests
    /// </summary>
    [TestFixture]
    public class EditSurveyCommandHandlerTests:CommandTestBase
    {
        private EditSurveyCommandHandler commandHandler;

        public EditSurveyCommandHandlerTests()
        {
            this.commandHandler = new EditSurveyCommandHandler(this.Context, this.Mapper);
            var editableSurvey = new Survey()
            {
                Id = 1,
                SurveyTitle = "TestCommand",
                ActiveFrom = TimeProvider.Current.UtcNow.AddDays(-1),
                ActiveTo = TimeProvider.Current.UtcNow,
                PageSize = 3,
                Description = "Test"
            };
            this.Context.Surveys.Add(editableSurvey);
            this.Context.SaveChanges();
        }

        [Test]
        public void EditSurveyCommandHandlerCtor_IfBmsSurveyDbContextIsNull_ArgumentNullException()
        {

            // Act & Assert
            Assert.That(() => new EditSurveyCommandHandler(null, this.Mapper),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("context"));
        }

        [Test]
        public void EditSurveyCommandHandlerCtor_IfIMapperIsNull_ArgumentNullException()
        {

            // Act & Assert
            Assert.That(() => new EditSurveyCommandHandler(this.Context, null),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("mapper"));
        }

        [Test]
        public async Task EditSurveyCommandHandler_ValidSurvey_ShouldUpdateSurvey()
        {
            // Arrange
            TimeProvider.Current = new TestTimeProvider();
            var command = new EditSurveyCommand
            {
                Id = 1,
                SurveyTitle = "TestCommandUpdate",
                ActiveFrom = TimeProvider.Current.UtcNow,
                ActiveTo = TimeProvider.Current.UtcNow.AddDays(1),
                PageSize = 5,
                Description = "TestUpdate"
            };

            // Act
            await commandHandler.Handle(command, CancellationToken.None);
            var survey = await this.Context.Surveys.FindAsync(1);
          
            // Assert
            Assert.IsTrue(survey.IsActive);
            Assert.IsFalse(survey.IsDeleted);
            Assert.AreEqual(command.SurveyTitle, survey.SurveyTitle);
            Assert.AreEqual(command.ActiveFrom.Date, survey.ActiveFrom.Date);
            Assert.AreEqual(command.ActiveTo.Date, survey.ActiveTo.Date);
            Assert.AreEqual(command.PageSize, survey.PageSize);
            Assert.AreEqual(command.Description, survey.Description);
            Assert.AreEqual(TimeProvider.Current.UtcNow, survey.ModifiedOn);
            Assert.AreEqual("TestUser", survey.ModifiedFrom);
        }

        [Test]
        public void EditSurveyCommandHandler_IfSurveyNotFound_ShouldThrowNotFoundException()
        {
            //Arrange
            var command = new EditSurveyCommand
            {
                Id = 2,
                SurveyTitle = "TestCommandUpdate",
                ActiveFrom = TimeProvider.Current.UtcNow,
                ActiveTo = TimeProvider.Current.UtcNow.AddDays(1),
                PageSize = 5,
                Description = "TestUpdate"
            };

            //Act & Assert
            Assert.That(async () => await commandHandler.Handle(command, CancellationToken.None),
                Throws.TypeOf<NotFoundException>()
                    .With.Property("Message").Contains("survey")
                    .With.Property("Message").Contains("2"));
        }

        [Test]
        public void EditSurveyCommandHandler_IfSurveyIsDeleted_ShouldThrowNotFoundException()
        {
            //Arrange
            var deletedSurvey = new Survey()
            {
                Id = 2,
                SurveyTitle = "TestCommand",
                ActiveFrom = TimeProvider.Current.UtcNow.AddDays(-1),
                ActiveTo = TimeProvider.Current.UtcNow,
                PageSize = 3,
                Description = "Test",
                IsDeleted = true,
            };
            this.Context.Surveys.Add(deletedSurvey);
            this.Context.SaveChanges();

            var command = new EditSurveyCommand
            {
                Id = 2,
                SurveyTitle = "TestCommandUpdate",
                ActiveFrom = TimeProvider.Current.UtcNow,
                ActiveTo = TimeProvider.Current.UtcNow.AddDays(1),
                PageSize = 5,
                Description = "TestUpdate"
            };

            //Act & Assert
            Assert.That(async () => await commandHandler.Handle(command, CancellationToken.None),
                Throws.TypeOf<NotFoundException>()
                    .With.Property("Message").Contains("survey")
                    .With.Property("Message").Contains("2"));
        }
    }
}