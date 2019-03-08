//-----------------------------------------------------------------------
// <copyright file="RequestValidationBehaviorTests.cs" company="Business Management System Ltd.">
//     Copyright "2019" (c) Business Management System Ltd. All rights reserved.
// </copyright>
// <author>Nikolay.Kostadinov</author>
// <convention>
// Test method convention
// [UnitOfWork]_[Scenario]_[ExpectedBehavior]
// </convention>
//-----------------------------------------------------------------------

namespace BmsSurvey.Application.Tests.InfrastructureTests
{
    #region Using 

    using System;
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Infrastructure;
    using Exceptions;
    using FluentValidation;
    using Infrastructure;
    using MediatR;
    using ValidationException = Exceptions.ValidationException;

    #endregion

    /// <summary>
    /// Summary description for RequestValidationBehaviorTests
    /// </summary>
    [TestFixture]
    public class RequestValidationBehaviorTests
    {
        [Test]
        public void RequestValidator_ValidationError_ThrowValidationException()
        {
            // Arrange
            var stubValidators = new List<IValidator<TestRequest>>() { new TestRequestValidator() };
            var behavior = new RequestValidationBehavior<TestRequest, Unit>(stubValidators);
            var request = new TestRequest();

            //Act & Assert
            Assert.That(
                async () => await behavior.Handle(request, CancellationToken.None, null),
                Throws.TypeOf<ValidationException>()
                    .With.Property("Failures").ContainKey("Id")
                    .With.Property("Failures").ContainValue(new[] { "Id empty" })
                    .With.Property("Failures").ContainKey("Name")
                    .With.Property("Failures").ContainValue(new[] { "Name empty" })
                );
        }

        [Test]
        public async Task RequestValidator_ValidRequest_ReturnNext()
        {
            // Arrange
            var stubValidators = new List<IValidator<TestRequest>>() { new TestRequestValidator() };
            var behavior = new RequestValidationBehavior<TestRequest, Unit>(stubValidators);
            var request = new TestRequest() { Id = 1, Name = "Test" };
            RequestHandlerDelegate<Unit> next = MockRequeshanhlerDelegate;

            //Act
            var result = await behavior.Handle(request, CancellationToken.None, next);

            //& Assert
            Assert.IsInstanceOf<Unit>(result);
        }

        private Task<Unit> MockRequeshanhlerDelegate()
        {
            return Unit.Task;
        }
    }
}