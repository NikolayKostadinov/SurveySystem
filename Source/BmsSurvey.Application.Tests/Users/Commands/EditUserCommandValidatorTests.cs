//-----------------------------------------------------------------------
// <copyright file="EditUserCommandValidatorTests.cs" company="Business Management System Ltd.">
//     Copyright "2019" (c) Business Management System Ltd. All rights reserved.
// </copyright>
// <author>Nikolay.Kostadinov</author>
// <convention>
// Test method convention
// [UnitOfWork]_[Scenario]_[ExpectedBehavior]
// </convention>
//-----------------------------------------------------------------------

namespace BmsSurvey.Application.Tests.Users.Commands
{
    #region Using 

    using System;
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using Application.Surveys.Commands.EditSurvey;
    using Application.Users.Commands.EditUser;
    using Common.Abstract;
    using FluentValidation.TestHelper;
    using Infrastructure;

    #endregion

    /// <summary>
    /// Summary description for EditUserCommandValidatorTests
    /// </summary>
    [TestFixture]
    public class EditUserCommandValidatorTests
    {
        private EditUserCommandValidator validator;

        [SetUp]
        public void Setup()
        {
            validator = new EditUserCommandValidator();
            TimeProvider.Current = new TestTimeProvider();
        }

        [Test]
        public void Should_have_error_when_Id_is_empty()
        {

            validator.ShouldHaveValidationErrorFor(user => user.Id, 0);
            var errors = validator.Validate(new EditUserCommand()).Errors;

            Assert.IsTrue(errors.Any(err => err.PropertyName == "Id" && err.ErrorCode.Equals("NotEmptyValidator")));
        }

        [Test]
        public void Should_have_error_when_email_is_empty()
        {

            validator.ShouldHaveValidationErrorFor(user => user.Email, string.Empty);
            var errors = validator.Validate(new EditUserCommand()).Errors;

            Assert.IsTrue(errors.Any(err => err.PropertyName == "Email" && err.ErrorCode.Equals("NotEmptyValidator")));
        }

        [Test]
        public void Should_have_error_when_email_is_not_correct_format()
        {

            validator.ShouldHaveValidationErrorFor(user => user.Email, "test");
            var errors = validator.Validate(new EditUserCommand(){Email = "test"}).Errors;

            Assert.IsTrue(errors.Any(err => err.PropertyName == "Email" && err.ErrorCode.Equals("EmailValidator")));
        }

        [Test]
        public void Should_have_error_when_first_name_is_empty()
        {

            validator.ShouldHaveValidationErrorFor(user => user.FirstName, string.Empty);
            var errors = validator.Validate(new EditUserCommand()).Errors;

            Assert.IsTrue(errors.Any(err => err.PropertyName == "FirstName" && err.ErrorCode.Equals("NotEmptyValidator")));
        }

        [Test]
        public void Should_have_error_when_last_name_is_empty()
        {

            validator.ShouldHaveValidationErrorFor(user => user.LastName, string.Empty);
            var errors = validator.Validate(new EditUserCommand()).Errors;

            Assert.IsTrue(errors.Any(err => err.PropertyName == "LastName" && err.ErrorCode.Equals("NotEmptyValidator")));
        }

        [Test]
        public void Should_have_error_when_tab_number_is_empty()
        {

            validator.ShouldHaveValidationErrorFor(user => user.TabNumber, string.Empty);
            var errors = validator.Validate(new EditUserCommand()).Errors;

            Assert.IsTrue(errors.Any(err => err.PropertyName == "TabNumber" && err.ErrorCode.Equals("NotEmptyValidator")));
        }

        [Test]
        public void Should_have_error_when_tab_number_is_not_correct_format()
        {

            validator.ShouldHaveValidationErrorFor(user => user.TabNumber, "test");
            var errors = validator.Validate(new EditUserCommand(){TabNumber = "test"}).Errors;

            Assert.IsTrue(errors.Any(err => err.PropertyName == "TabNumber" && err.ErrorCode.Equals("PredicateValidator")));
        }


        //[Test]
        //public void Should_have_error_when_PageSize_is_0()
        //{
        //    validator.ShouldHaveValidationErrorFor(survey => survey.PageSize, 0);
        //    var errors = validator.Validate(new EditSurveyCommand()).Errors;

        //    Assert.IsTrue(errors.Any(err => err.ErrorMessage.Contains("greater than 0")));
        //}


        //[Test]
        //public void Should_have_error_when_ActiveFrom_is_empty()
        //{

        //    validator.ShouldHaveValidationErrorFor(survey => survey.ActiveFrom, new DateTime());
        //    var errors = validator.Validate(new EditSurveyCommand()).Errors;

        //    Assert.IsTrue(errors.Any(err => err.PropertyName == "ActiveFrom" && err.ErrorCode.Equals("NotEmptyValidator")));
        //}

        //[Test]
        //public void Should_have_error_when_ActiveTo_is_empty()
        //{

        //    validator.ShouldHaveValidationErrorFor(survey => survey.ActiveTo, new DateTime());
        //    var errors = validator.Validate(new EditSurveyCommand()).Errors;

        //    Assert.IsTrue(errors.Any(err => err.PropertyName == "ActiveTo" && err.ErrorCode.Equals("NotEmptyValidator")));
        //}

        //[Test]
        //public void Should_have_error_when_ActiveTo_is_less_than_ActiveFrom()
        //{
        //    var errors = validator.Validate(new EditSurveyCommand()
        //    {
        //        ActiveFrom = TimeProvider.Current.UtcNow,
        //        ActiveTo = TimeProvider.Current.UtcNow.AddDays(-1)
        //    }).Errors;

        //    Assert.IsTrue(errors.Any(err => err.PropertyName == "ActiveTo" && err.ErrorCode.Equals("PredicateValidator")));
        //}
    }
}