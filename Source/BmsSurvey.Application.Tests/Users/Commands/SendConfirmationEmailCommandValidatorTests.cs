//-----------------------------------------------------------------------
// <copyright file="SendConfirmationEmailCommandValidatorTests.cs" company="Business Management System Ltd.">
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
    using Application.Users.Commands.SendConfirmationEmail;
    using FluentValidation.TestHelper;

    #endregion

    /// <summary>
    /// Summary description for SendConfirmationEmailCommandValidatorTests
    /// </summary>
    [TestFixture]
    public class SendConfirmationEmailCommandValidatorTests
    {
        private SendConfirmationEmailCommandValidator validator;

        [SetUp]
        public void Setup()
        {
            validator = new SendConfirmationEmailCommandValidator();
        }

        [Test]
        public void Should_have_error_when_Id_is_empty()
        {
            validator.ShouldHaveValidationErrorFor(user => user.Id, 0);
            var errors = validator.Validate(new SendConfirmationEmailCommand()).Errors;

            Assert.IsTrue(errors.Any(err => err.PropertyName == "Id" && err.ErrorCode.Equals("NotEmptyValidator")));
        }
    }
}