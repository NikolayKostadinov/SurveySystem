//-----------------------------------------------------------------------
// <copyright file="CreateSurveyCommandValidator.cs" company="Business Management System Ltd.">
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
    using Application.Surveys.Commands.CreateSurvey;
    using Common.Abstract;
    using FluentValidation.TestHelper;

    #endregion



    /// <summary>
    /// Summary description for CreateSurveyCommandValidator
    /// </summary>
    [TestFixture]
    public class CreateSurveyCommandValidatorTests
    {
        private CreateSurveyCommandValidator validator;

        [SetUp]
        public void Setup()
        {
            validator = new CreateSurveyCommandValidator();
            TimeProvider.Current = new TestTimeProvider();
        }

        [Test]
        public void Should_have_error_when_SurveyTitle_is_null()
        {
            validator.ShouldHaveValidationErrorFor(survey => survey.SurveyTitle, null as string);
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
            var errors = validator.Validate(new CreateSurveyCommand()).Errors;

            Assert.IsTrue(errors.Any(err => err.ErrorMessage.Contains("greater than 0")));
        }

        [Test]
        public void Should_have_error_when_ActiveFrom_is_NotEmptyValidator()
        {

            validator.ShouldHaveValidationErrorFor(survey => survey.ActiveFrom, new DateTime());
            var errors = validator.Validate(new CreateSurveyCommand()).Errors;

            Assert.IsTrue(errors.Any(err => err.PropertyName == "ActiveFrom" && err.ErrorCode.Equals("NotEmptyValidator")));
        }

        [Test]
        public void Should_have_error_when_ActiveTo_is_NotEmptyValidator()
        {

            validator.ShouldHaveValidationErrorFor(survey => survey.ActiveTo, new DateTime());
            var errors = validator.Validate(new CreateSurveyCommand()).Errors;

            Assert.IsTrue(errors.Any(err => err.PropertyName == "ActiveTo" && err.ErrorCode.Equals("NotEmptyValidator")));
        }

        [Test]
        public void Should_have_error_when_ActiveToLessThanActiveFrom_is_PredicateValidator()
        {
            var errors = validator.Validate(new CreateSurveyCommand()
            {
                ActiveFrom = TimeProvider.Current.UtcNow,
                ActiveTo = TimeProvider.Current.UtcNow.AddDays(-1)
            }).Errors;

            Assert.IsTrue(errors.Any(err => err.PropertyName == "ActiveTo" && err.ErrorCode.Equals("PredicateValidator")));
        }

    }
}