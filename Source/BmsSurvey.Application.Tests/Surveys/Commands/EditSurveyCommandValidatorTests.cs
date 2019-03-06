//-----------------------------------------------------------------------
// <copyright file="EditSurveyCommandValidatorTests.cs" company="Business Management System Ltd.">
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
    using Application.Surveys.Commands.EditSurvey;
    using Common.Abstract;
    using FluentValidation.TestHelper;

    #endregion


    /// <summary>
    /// Summary description for EditSurveyCommandValidatorTests
    /// </summary>
    [TestFixture]
    public class EditSurveyCommandValidatorTests
    {
        private EditSurveyCommandValidator validator;

        [SetUp]
        public void Setup()
        {
            validator = new EditSurveyCommandValidator();
            TimeProvider.Current = new TestTimeProvider();
        }

        [Test]
        public void Should_have_error_when_Text_is_null()
        {
            validator.ShouldHaveValidationErrorFor(survey => survey.SurveyTitle, null as string);
        }

        [Test]
        public void Should_have_error_when_Text_is_empty()
        {
            validator.ShouldHaveValidationErrorFor(survey => survey.SurveyTitle, string.Empty);
        }

        [Test]
        public void Should_not_have_error_when_name_is_specified()
        {
            validator.ShouldNotHaveValidationErrorFor(survey => survey.SurveyTitle, "TestTitle");
        }

        [Test]
        public void Should_have_error_when_PageSize_is_0()
        {
            validator.ShouldHaveValidationErrorFor(survey => survey.PageSize, 0);
            var errors = validator.Validate(new EditSurveyCommand()).Errors;

            Assert.IsTrue(errors.Any(err => err.ErrorMessage.Contains("greater than 0")));
        }

        [Test]
        public void Should_have_error_when_Id_is_empty()
        {

            validator.ShouldHaveValidationErrorFor(survey => survey.ActiveFrom, new DateTime());
            var errors = validator.Validate(new EditSurveyCommand()).Errors;

            Assert.IsTrue(errors.Any(err => err.PropertyName == "Id" && err.ErrorCode.Equals("NotEmptyValidator")));
        }
        [Test]
        public void Should_have_error_when_ActiveFrom_is_empty()
        {

            validator.ShouldHaveValidationErrorFor(survey => survey.ActiveFrom, new DateTime());
            var errors = validator.Validate(new EditSurveyCommand()).Errors;

            Assert.IsTrue(errors.Any(err => err.PropertyName == "ActiveFrom" && err.ErrorCode.Equals("NotEmptyValidator")));
        }

        [Test]
        public void Should_have_error_when_ActiveTo_is_empty()
        {

            validator.ShouldHaveValidationErrorFor(survey => survey.ActiveTo, new DateTime());
            var errors = validator.Validate(new EditSurveyCommand()).Errors;

            Assert.IsTrue(errors.Any(err => err.PropertyName == "ActiveTo" && err.ErrorCode.Equals("NotEmptyValidator")));
        }

        [Test]
        public void Should_have_error_when_ActiveTo_is_less_than_ActiveFrom()
        {
            var errors = validator.Validate(new EditSurveyCommand()
            {
                ActiveFrom = TimeProvider.Current.UtcNow,
                ActiveTo = TimeProvider.Current.UtcNow.AddDays(-1)
            }).Errors;

            Assert.IsTrue(errors.Any(err => err.PropertyName == "ActiveTo" && err.ErrorCode.Equals("PredicateValidator")));
        }
    }
}