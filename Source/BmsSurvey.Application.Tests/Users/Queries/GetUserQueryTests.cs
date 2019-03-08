//-----------------------------------------------------------------------
// <copyright file="GetUserQueryTests.cs" company="Business Management System Ltd.">
//     Copyright "2019" (c) Business Management System Ltd. All rights reserved.
// </copyright>
// <author>Nikolay.Kostadinov</author>
// <convention>
// Test method convention
// [UnitOfWork]_[Scenario]_[ExpectedBehavior]
// </convention>
//-----------------------------------------------------------------------

namespace BmsSurvey.Application.Tests.Users.Queries
{
    #region Using 

    using System;
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using Application.Users.Queries.GetUser;

    #endregion

    /// <summary>
    /// Summary description for GetUserQueryTests
    /// </summary>
    [TestFixture]
    public class GetUserQueryTests
    {
        [Test]
        public void DeleteUserCommandCtor_IPrincipalIsNull_ThrowsArgumentNullException()
        {
            //Act & Assert
            Assert.That(
                () => new GetUserQuery( null),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("user"));
        }
    }
}