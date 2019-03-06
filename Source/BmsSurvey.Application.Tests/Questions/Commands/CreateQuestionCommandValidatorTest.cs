//  ------------------------------------------------------------------------------------------------
//   <copyright file="CreateQuestionCommandValidatorTest.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Tests.Questions.Commands
{
    #region Using

    using Application.Questions.Commands.CreateQuestion;
    using Common.Abstract;
    using FluentValidation.TestHelper;
    using Infrastructure;
    using Microsoft.EntityFrameworkCore.Internal;
    using NUnit.Framework;
    using System.Linq;
    using Domain.Entities;

    #endregion

    /// <summary>
    ///     Summary description for CreateQuestionCommandValidatorTest
    /// </summary>
    [TestFixture]
    public class CreateQuestionCommandValidatorTest
    {
        [SetUp]
        public void Setup()
        {
            validator = new CreateQuestionCommandValidator();
            TimeProvider.Current = new TestTimeProvider();
        }

        private CreateQuestionCommandValidator validator;

        [Test]
        public void Should_have_error_when_SurveyId_is_0()
        {
            validator.ShouldHaveValidationErrorFor(question => question.SurveyId, 0);

            var errors = validator.Validate(new CreateQuestionCommand()).Errors;
             Assert.IsTrue(errors.Any(err => err.PropertyName == "SurveyId" && err.ErrorCode.Equals("NotEmptyValidator")));
        }

        [Test]
        public void Should_have_error_when_Text_is_empty()
        {
            validator.ShouldHaveValidationErrorFor(question => question.Text, string.Empty);
            var errors = validator.Validate(new CreateQuestionCommand()).Errors;
            Assert.IsTrue(errors.Any(err => err.PropertyName == "Text" && err.ErrorCode.Equals("NotEmptyValidator")));
        }

        [Test]
        public void Should_have_error_when_DisplayNumber_is_empty()
        {
            validator.ShouldHaveValidationErrorFor(question => question.DisplayNumber, 0);
            var errors = validator.Validate(new CreateQuestionCommand()).Errors;
            Assert.IsTrue(errors.Any(err => err.PropertyName == "DisplayNumber" && err.ErrorCode.Equals("NotEmptyValidator")));
        }

        [Test]
        public void Should_have_error_when_QueryType_is_OutOfRange()
        {
            validator.ShouldHaveValidationErrorFor(question => question.QuestionType, (QuestionType)6);
            var errors = validator.Validate(new CreateQuestionCommand(){QuestionType = (QuestionType)6}).Errors;
            Assert.IsTrue(errors.Any(err => err.PropertyName == "QuestionType" && err.ErrorCode.Equals("PredicateValidator")));
        }
    }
}