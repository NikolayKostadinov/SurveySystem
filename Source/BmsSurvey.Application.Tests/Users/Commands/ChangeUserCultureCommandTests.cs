//-----------------------------------------------------------------------
// <copyright file="ChangeUserCultureCommandTests.cs" company="Business Management System Ltd.">
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
    using Application.Users.Commands.ChangeUserCulture;

    #endregion

    /// <summary>
    /// Summary description for ChangeUserCultureCommandTests
    /// </summary>
    [TestFixture]
    public class ChangeUserCultureCommandTests
    {
        [Test]
        public void ChangeUserCultureCommand_IPrincipalIsNull_ThrowsArgumentNullException()
        {
            //Act & Assert
            Assert.That(
                () => new ChangeUserCultureCommand(null, "bg"),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("user"));
        }
    }
}