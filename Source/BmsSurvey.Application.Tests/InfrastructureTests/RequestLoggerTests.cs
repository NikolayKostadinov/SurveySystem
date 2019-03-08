//-----------------------------------------------------------------------
// <copyright file="RequestLoggerTests.cs" company="Business Management System Ltd.">
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
    using Microsoft.Extensions.Logging;

    #endregion

    /// <summary>
    /// Summary description for RequestLoggerTests
    /// </summary>
    [TestFixture]
    public class RequestLoggerTests
    {
        [Test]
        public void RequestLoggerCtor_LoggerIsNull_ThrowsArgumentNullException()
        {
            //Act & Assert
            Assert.That(
                () => new RequestLogger<TestRequest>(null),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("logger"));
        }

        [Test]
        public async Task RequestLoggerCtor_ValidRequest_WillBeLogged()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<TestRequest>>();
            var requestLogger = new RequestLogger<TestRequest>(mockLogger.Object);
            var testRequest = new TestRequest() { Id = 1, Name = "Test" };

            //Act 
            await requestLogger.Process(testRequest, CancellationToken.None);

            //Assert
            mockLogger.Verify(im => im.Log<object>(It.Is<LogLevel>(ll=>ll == LogLevel.Information), It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()));

        }
    }
}