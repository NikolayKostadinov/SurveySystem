//-----------------------------------------------------------------------
// <copyright file="EditQuestionCommandValidatorTests.cs" company="Business Management System Ltd.">
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
    using Application.Questions.Commands.EditQuestion;
    using Common.Abstract;
    using Domain.Entities;
    using FluentValidation.TestHelper;
    using Infrastructure;

    #endregion

    /// <summary>
    /// Summary description for EditQuestionCommandValidatorTests
    /// </summary>
    [TestFixture]
    public class EditQuestionCommandValidatorTests
    {
        private EditQuestionCommandValidator validator;


        [SetUp]
        public void Setup()
        {
            var context = BmsSurveyContextFactory.Create();
            validator = new EditQuestionCommandValidator(context);
            TimeProvider.Current = new TestTimeProvider();
        }

        [Test]
        public void Should_have_error_when_Id_is_0()
        {
            validator.ShouldHaveValidationErrorFor(question => question.Id, 0);

            var errors = validator.Validate(new EditQuestionCommand()).Errors;
            Assert.IsTrue(errors.Any(err => err.PropertyName == "Id" && err.ErrorCode.Equals("NotEmptyValidator")));
        }

        [Test]
        public void Should_have_error_when_Text_is_empty()
        {
            validator.ShouldHaveValidationErrorFor(question => question.Text, string.Empty);
            var errors = validator.Validate(new EditQuestionCommand()).Errors;
            Assert.IsTrue(errors.Any(err => err.PropertyName == "Text" && err.ErrorCode.Equals("NotEmptyValidator")));
        }

        [Test]
        public void Should_have_error_when_DisplayNumber_is_empty()
        {
            validator.ShouldHaveValidationErrorFor(question => question.DisplayNumber, 0);
            var errors = validator.Validate(new EditQuestionCommand()).Errors;
            Assert.IsTrue(errors.Any(err => err.PropertyName == "DisplayNumber" && err.ErrorCode.Equals("NotEmptyValidator")));
        }

        [Test]
        public void Should_have_error_when_QueryType_is_OutOfRange()
        {
            validator.ShouldHaveValidationErrorFor(question => question.QuestionType, (QuestionType)6);
            var errors = validator.Validate(new EditQuestionCommand() { QuestionType = (QuestionType)6 }).Errors;
            Assert.IsTrue(errors.Any(err => err.PropertyName == "QuestionType" && err.ErrorCode.Equals("PredicateValidator")));
        }
    }
}