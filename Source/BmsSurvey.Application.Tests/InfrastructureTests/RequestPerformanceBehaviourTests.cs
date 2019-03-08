//-----------------------------------------------------------------------
// <copyright file="RequestPerformanceBehaviourTests.cs" company="Business Management System Ltd.">
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
    using Infrastructure;
    using MediatR;
    using Microsoft.Extensions.Logging;

    #endregion

    /// <summary>
    /// Summary description for RequestPerformanceBehaviourTests
    /// </summary>
    [TestFixture]
    public class RequestPerformanceBehaviourTests
    {
        [Test]
        public void RequestPerformanceBehaviourCtor_LoggerIsNull_ThrowsArgumentNullException()
        {
            //Act & Assert
            Assert.That(
                () => new RequestPerformanceBehaviour<TestRequest,Unit>(null),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("logger"));
        }

        [Test]
        public async Task RequestPerformanceBehaviour_RequestLongOver500ms_WillBeLogged()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<TestRequest>>();
            var behaviour = new RequestPerformanceBehaviour<TestRequest, Unit>(mockLogger.Object);
            var testRequest = new TestRequest() { Id = 1, Name = "Test" };
            RequestHandlerDelegate<Unit> next = MockRequeshanhlerDelegate;
            //Act 
            await behaviour.Handle(testRequest, CancellationToken.None, next);

            //Assert
            mockLogger.Verify(im => im.Log<object>(It.Is<LogLevel>(ll => ll == LogLevel.Warning), It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()));

        }

        private Task<Unit> MockRequeshanhlerDelegate()
        {
            Thread.Sleep(510);
            return Unit.Task;
        }
    }
}