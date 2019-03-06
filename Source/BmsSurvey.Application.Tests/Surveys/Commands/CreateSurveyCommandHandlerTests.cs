//  ------------------------------------------------------------------------------------------------
//   <copyright file="CreateSurveyCommandHandlerTests.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Tests.Surveys.Commands
{
    #region Using

    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Surveys.Commands.CreateSurvey;
    using Common.Abstract;
    using Infrastructure;
    using NUnit.Framework;

    #endregion

    /// <summary>
    ///     Summary description for CreateSurveyCommandHandlerTests
    /// </summary>
    [TestFixture]
    public class CreateSurveyCommandHandlerTests : CommandTestBase
    {
        private readonly CreateSurveyCommandHandler commandHandler;

        public CreateSurveyCommandHandlerTests()
        {
            commandHandler = new CreateSurveyCommandHandler(Context, Mapper);
        }

        [Test]
        public async Task CreateSurveyCommandHandler_NewSurvey_ShouldCreateSurvey()
        {
            // Arrange
            TimeProvider.Current = new TestTimeProvider();
            var command = new CreateSurveyCommand
            {
                SurveyTitle = "TestCommand",
                ActiveFrom = TimeProvider.Current.UtcNow,
                ActiveTo = TimeProvider.Current.UtcNow.AddDays(1),
                PageSize = 4,
                Description = "Test"
            };

            // Act
            await commandHandler.Handle(command, CancellationToken.None);
            var survey = await Context.Surveys.FindAsync(1);
            // Assert
            Assert.AreEqual(1, Context.Surveys.Count());
            Assert.IsTrue(survey.IsActive);
            Assert.IsFalse(survey.IsDeleted);
            Assert.AreEqual(command.SurveyTitle, survey.SurveyTitle);
            Assert.AreEqual(command.ActiveFrom.Date, survey.ActiveFrom.Date);
            Assert.AreEqual(command.ActiveTo.Date, survey.ActiveTo.Date);
            Assert.AreEqual(command.PageSize, survey.PageSize);
            Assert.AreEqual(command.Description, survey.Description);
            Assert.AreEqual(TimeProvider.Current.UtcNow, survey.CreatedOn);
            Assert.AreEqual("TestUser", survey.CreatedFrom);
        }

        [Test]
        public void CreateSurveyCommandHandlerCtor_IfBmsSurveyDbContextIsNull_ArgumentNullException()
        {
            // Act & Assert
            Assert.That(() => new CreateSurveyCommandHandler(null, Mapper),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("context"));
        }

        [Test]
        public void CreateSurveyCommandHandlerCtor_IfIMapperIsNull_ArgumentNullException()
        {
            // Act & Assert
            Assert.That(() => new CreateSurveyCommandHandler(Context, null),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("mapper"));
        }
    }

    public class TestTimeProvider : TimeProvider
    {
        public override DateTime UtcNow => new DateTime(2019, 2, 25, 5, 30, 12);
    }
}