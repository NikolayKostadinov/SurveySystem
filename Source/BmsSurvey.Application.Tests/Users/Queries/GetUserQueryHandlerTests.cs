//-----------------------------------------------------------------------
// <copyright file="GetUserQueryHandlerTests.cs" company="Business Management System Ltd.">
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
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Users.Queries.GetAllUsers;
    using Application.Users.Queries.GetUser;
    using Domain.Entities.Identity;
    using Exceptions;
    using static Infrastructure.IdentityMockHelpers;

    #endregion

    /// <summary>
    /// Summary description for GetUserQueryHandlerTests
    /// </summary>
    [TestFixture]
    public class GetUserQueryHandlerTests
    {
        [Test]
        public void GetUserQueryHandlerCtor_UserManagerIsNull_ThrowsArgumentNullException()
        {
            var stubUserManager = MockUserManager<User>();

            //Act & Assert
            Assert.That(
                () => new GetUserQueryHandler(null),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("userManager"));
        }

        [Test]
        public async Task GetUserQueryHandler_ValidCommand_AllNotDeletedUsers()
        {
            var stubUserManager = MockUserManager<User>();
            var users = new List<User> { new User { Id = 1, UserName = "user1" }, new User { Id = 2, UserName = "user2" }, new User { Id = 3, UserName = "user3", IsDeleted = true } }.AsQueryable();
            stubUserManager.SetupGet(x => x.Users).Returns(users);
            var stubIIdentity = new Mock<IIdentity>();
            stubIIdentity.SetupGet(x => x.Name).Returns("user1");
            var stubIPrincipal = new Mock<IPrincipal>();
            stubIPrincipal.SetupGet(x => x.Identity).Returns(stubIIdentity.Object);
            var queryHandler = new GetUserQueryHandler(stubUserManager.Object);

            //Act 
            var query = new GetUserQuery(new ClaimsPrincipal(stubIPrincipal.Object));
            var user = await queryHandler.Handle(query, CancellationToken.None);

            //Assert
            Assert.AreEqual("user1", user.UserName);
            Assert.AreEqual(1, user.Id);
        }


        [Test]
        public void GetUserQueryHandler_DeletedUser_ThrowsNotFoundException()
        {
            var stubUserManager = MockUserManager<User>();
            var users = new List<User> { new User { Id = 1, UserName = "user1" }, new User { Id = 2, UserName = "user2" }, new User { Id = 3, UserName = "user3", IsDeleted = true } }.AsQueryable();

            stubUserManager.SetupGet(x => x.Users).Returns(users);
            var stubIIdentity = new Mock<IIdentity>();
            stubIIdentity.SetupGet(x => x.Name).Returns("user3");
            var stubIPrincipal = new Mock<IPrincipal>();
            stubIPrincipal.SetupGet(x => x.Identity).Returns(stubIIdentity.Object);
            var queryHandler = new GetUserQueryHandler(stubUserManager.Object);
            var query = new GetUserQuery(new ClaimsPrincipal(stubIPrincipal.Object));

            //Act & Assert

            Assert.That(
                async () => await queryHandler.Handle(query, CancellationToken.None),
                Throws.TypeOf<NotFoundException>()
                    .With.Message.Match("user")
                    .With.Message.Match("user3")
                    .With.Property("Key").EqualTo("user3"));

        }
    }
}