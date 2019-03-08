//-----------------------------------------------------------------------
// <copyright file="CreateUserCommandValidatorTests.cs" company="Business Management System Ltd.">
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
    using Application.Users.Commands.CreateUser;
    using Application.Users.Commands.EditUser;
    using Common.Abstract;
    using FluentValidation.TestHelper;

    #endregion

    /// <summary>
    /// Summary description for CreateUserCommandValidatorTests
    /// </summary>
    [TestFixture]
    public class CreateUserCommandValidatorTests
    {
        private CreateUserCommandValidator validator;

        [SetUp]
        public void Setup()
        {
            validator = new CreateUserCommandValidator();
        }


        [Test]
        public void Should_have_error_when_email_is_empty()
        {

            validator.ShouldHaveValidationErrorFor(user => user.Email, string.Empty);
            var errors = validator.Validate(new CreateUserCommand()).Errors;

            Assert.IsTrue(errors.Any(err => err.PropertyName == "Email" && err.ErrorCode.Equals("NotEmptyValidator")));
        }

        [Test]
        public void Should_have_error_when_email_is_not_correct_format()
        {

            validator.ShouldHaveValidationErrorFor(user => user.Email, "test");
            var errors = validator.Validate(new CreateUserCommand() { Email = "test" }).Errors;

            Assert.IsTrue(errors.Any(err => err.PropertyName == "Email" && err.ErrorCode.Equals("EmailValidator")));
        }

        [Test]
        public void Should_have_error_when_first_name_is_empty()
        {

            validator.ShouldHaveValidationErrorFor(user => user.FirstName, string.Empty);
            var errors = validator.Validate(new CreateUserCommand()).Errors;

            Assert.IsTrue(errors.Any(err => err.PropertyName == "FirstName" && err.ErrorCode.Equals("NotEmptyValidator")));
        }

        [Test]
        public void Should_have_error_when_last_name_is_empty()
        {

            validator.ShouldHaveValidationErrorFor(user => user.LastName, string.Empty);
            var errors = validator.Validate(new CreateUserCommand()).Errors;

            Assert.IsTrue(errors.Any(err => err.PropertyName == "LastName" && err.ErrorCode.Equals("NotEmptyValidator")));
        }

        [Test]
        public void Should_have_error_when_tab_number_is_empty()
        {

            validator.ShouldHaveValidationErrorFor(user => user.TabNumber, string.Empty);
            var errors = validator.Validate(new CreateUserCommand()).Errors;

            Assert.IsTrue(errors.Any(err => err.PropertyName == "TabNumber" && err.ErrorCode.Equals("NotEmptyValidator")));
        }

        [Test]
        public void Should_have_error_when_tab_number_is_not_correct_format()
        {

            validator.ShouldHaveValidationErrorFor(user => user.TabNumber, "test");
            var errors = validator.Validate(new CreateUserCommand() { TabNumber = "test" }).Errors;

            Assert.IsTrue(errors.Any(err => err.PropertyName == "TabNumber" && err.ErrorCode.Equals("PredicateValidator")));
        }
    }
}