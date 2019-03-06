//-----------------------------------------------------------------------
// <copyright file="DeleteUserCommandTests.cs" company="Business Management System Ltd.">
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
    using Application.Users.Commands.DeleteUser;
    using NUnit.Framework;

    #endregion

    /// <summary>
    /// Summary description for DeleteUserCommandTests
    /// </summary>
    [TestFixture]
    public class DeleteUserCommandTests
    {
        [Test]
        public void DeleteUserCommandCtor_IPrincipalIsNull_ThrowsArgumentNullException()
        {
            //Act & Assert
            Assert.That(
                () => new DeleteUserCommand(1, null),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("currentUser"));
        }
    }
}