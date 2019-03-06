//-----------------------------------------------------------------------
// <copyright file="MessageLocalizationServiceTests.cs" company="Business Management System Ltd.">
//     Copyright "2019" (c) Business Management System Ltd. All rights reserved.
// </copyright>
// <author>Nikolay.Kostadinov</author>
// <convention>
// Test method convention
// [UnitOfWork]_[Scenario]_[ExpectedBehavior]
// </convention>
//-----------------------------------------------------------------------

namespace BmsSurvey.Application.Tests.Services
{
    #region Using 

    using System;
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using Application.Services;
    using Microsoft.Extensions.Localization;

    #endregion

    /// <summary>
    /// Summary description for MessageLocalizationServiceTests
    /// </summary>
    [TestFixture]
    public class MessageLocalizationServiceTests
    {
        [Test]
        public void GetLocalizedHtmlString_Key_ReturnAstring()
        {
            //Arrange
            var stubLocalizer = new Mock<IStringLocalizer>();
            stubLocalizer.Setup(x => x[It.IsAny<string>()]).Returns(new LocalizedString("LocalizedStringName", "LocalizedStringValue"));
            var stubLocalizerFactory = new Mock<IStringLocalizerFactory>();
            stubLocalizerFactory.Setup(x => x.Create(It.IsAny<string>(), It.IsAny<string>())).Returns(stubLocalizer.Object);
            var service = new MessageLocalizationService(stubLocalizerFactory.Object);

            //Act
            var result = service.GetLocalizedHtmlString("TEST");

            //Assert
            Assert.AreEqual("LocalizedStringValue", result.Value);
        }

        [Test]
        public void GetLocalizedHtmlString_KeyAndParameter_ReturnAstring()
        {
            //Arrange
            var stubLocalizer = new Mock<IStringLocalizer>();
            stubLocalizer.Setup(x => x[It.IsAny<string>(), It.IsAny<object[]>()])
                .Returns((string key, object[] parameter) => new LocalizedString("LocalizedStringName", $"LocalizedStringValue {parameter[0]}"));
            var stubLocalizerFactory = new Mock<IStringLocalizerFactory>();
            stubLocalizerFactory.Setup(x => x.Create(It.IsAny<string>(), It.IsAny<string>())).Returns(stubLocalizer.Object);
            var service = new MessageLocalizationService(stubLocalizerFactory.Object);

            //Act
            var result = service.GetLocalizedHtmlString("TEST", "parameter");

            //Assert
            Assert.IsTrue(result.Value.Contains("LocalizedStringValue"));
            Assert.IsTrue(result.Value.Contains("parameter"));
        }
    }
}