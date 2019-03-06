//-----------------------------------------------------------------------
// <copyright file="DeleteSurveyCommandHandlerTests.cs" company="Business Management System Ltd.">
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
    using Application.Surveys.Commands.DeleteSurvey;
    using Application.Surveys.Commands.EditSurvey;
    using Common.Abstract;
    using Domain.Entities;
    using Exceptions;
    using Infrastructure;

    #endregion


    /// <summary>
    /// Summary description for DeleteSurveyCommandHandlerTests
    /// </summary>
    [TestFixture]
    public class DeleteSurveyCommandHandlerTests : CommandTestBase
    {
        private DeleteSurveyCommandHandler commandHandler;

        public DeleteSurveyCommandHandlerTests()
        {
            this.commandHandler = new DeleteSurveyCommandHandler(this.Context);
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
        public void DeleteSurveyCommandHandlerCtor_IfBmsSurveyDbContextIsNull_ArgumentNullException()
        {

            // Act & Assert
            Assert.That(() => new DeleteSurveyCommandHandler(null),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("context"));
        }

        [Test]
        public async Task DeleteSurveyCommandHandler_ExistSurvey_ShouldDeleteSurvey()
        {
            // Arrange
            TimeProvider.Current = new TestTimeProvider();
            var command = new DeleteSurveyCommand
            {
                Id = 1,
            };

            // Act
            await commandHandler.Handle(command, CancellationToken.None);
            var survey = await this.Context.Surveys.FindAsync(1);

            // Assert
            Assert.IsTrue(survey.IsDeleted);
            Assert.AreEqual(TimeProvider.Current.UtcNow, survey.DeletedOn);
            Assert.AreEqual("TestUser", survey.DeletedFrom);
        }
    }
}