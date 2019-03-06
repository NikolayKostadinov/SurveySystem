//-----------------------------------------------------------------------
// <copyright file="DeleteUserCommandHandlerTests.cs" company="Business Management System Ltd.">
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
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Users.Commands.DeleteUser;
    using Domain.Entities.Identity;
    using Exceptions;
    using Microsoft.AspNetCore.Identity;
    using static Infrastructure.IdentityMockHelpers;
    #endregion

    /// <summary>
    /// Summary description for DeleteUserCommandHandlerTests
    /// </summary>
    [TestFixture]
    public class DeleteUserCommandHandlerTests
    {
        [Test]
        public void DeleteUserCommandHandlerCtor_UserManagerIsNull_ThrowsArgumentNullException()
        {
            //Act & Assert
            Assert.That(
                () => new DeleteUserCommandHandler(null),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("userManager"));
        }

        [Test]
        public async Task DeleteUserCommandHandler_UserId1_Deleted()
        {
            // Arrange
            var mockUserManager = MockUserManager<User>();
            mockUserManager.Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("2");
            mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(new User(){Id = 1, UserName = "UserForDelete"}));
            mockUserManager.Setup(um => um.DeleteAsync(It.IsAny<User>())).Returns(Task.FromResult(IdentityResult.Success));
            mockUserManager.Setup(um => um.UpdateSecurityStampAsync(It.IsAny<User>())).Returns(Task.FromResult(IdentityResult.Success));
            var commandHandler = new DeleteUserCommandHandler(mockUserManager.Object);

            // Act
            var command = new DeleteUserCommand(1, new ClaimsPrincipal());
            await commandHandler.Handle(command, CancellationToken.None);
          
            //Assert

            mockUserManager.Verify(im =>
                im.DeleteAsync(It.Is<User>(usr => usr.Id == 1 && usr.UserName == "UserForDelete")),Times.Once);

            mockUserManager.Verify(im =>
                im.UpdateSecurityStampAsync(It.Is<User>(usr => usr.Id == 1 && usr.UserName == "UserForDelete")),Times.Once);
        }

        [Test]
        public void DeleteUserCommandHandler_UserDeleteSelf_OperationFailedException()
        {
            // Arrange
            var mockUserManager = MockUserManager<User>();
            mockUserManager.Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("1");
            mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(new User(){Id = 1, UserName = "UserForDelete"}));
            mockUserManager.Setup(um => um.DeleteAsync(It.IsAny<User>())).Returns(Task.FromResult(IdentityResult.Success));
            mockUserManager.Setup(um => um.UpdateSecurityStampAsync(It.IsAny<User>())).Returns(Task.FromResult(IdentityResult.Success));
            var commandHandler = new DeleteUserCommandHandler(mockUserManager.Object);
            var stubIdentity = new Mock<IIdentity>();
            stubIdentity.SetupGet(x => x.Name).Returns("TestUser");
            var stubPrincipal = new Mock<IPrincipal>();
            stubPrincipal.SetupGet(sp => sp.Identity).Returns(stubIdentity.Object);
            var command = new DeleteUserCommand(1, new ClaimsPrincipal(stubPrincipal.Object));

            // Act & Assert
            Assert.That(async () => await commandHandler.Handle(command, CancellationToken.None),
            Throws.TypeOf<OperationFailedException>()
                .With.Property("Errors").Contains("Cannot delete same user TestUser")
            );
        }


        [Test]
        public void DeleteUserCommandHandler_FindByIdAsyncNull_NotFoundException()
        {
            // Arrange
            var mockUserManager = MockUserManager<User>();
            mockUserManager.Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("2");
            mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult<User>(null));
            mockUserManager.Setup(um => um.DeleteAsync(It.IsAny<User>())).Returns(Task.FromResult(IdentityResult.Success));
            mockUserManager.Setup(um => um.UpdateSecurityStampAsync(It.IsAny<User>())).Returns(Task.FromResult(IdentityResult.Success));
            var commandHandler = new DeleteUserCommandHandler(mockUserManager.Object);
            var command = new DeleteUserCommand(1, new ClaimsPrincipal());

            // Act & Assert
            Assert.That(async () => await commandHandler.Handle(command, CancellationToken.None),
            Throws.TypeOf<NotFoundException>()
                .With.Property("Message").Contains("user")
                .With.Property("Message").Contains("1"));
        }


        [Test]
        public void DeleteUserCommandHandler_DeleteAsyncFails_OperationFailedException()
        {
            // Arrange
            var mockUserManager = MockUserManager<User>();
            mockUserManager.Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("2");
            mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(new User() { Id = 1, UserName = "UserForDelete" }));
            mockUserManager.Setup(um => um.DeleteAsync(It.IsAny<User>())).Returns(Task.FromResult(IdentityResult.Failed(new IdentityError(){Description = "Error1"})));
            mockUserManager.Setup(um => um.UpdateSecurityStampAsync(It.IsAny<User>())).Returns(Task.FromResult(IdentityResult.Success));
            var commandHandler = new DeleteUserCommandHandler(mockUserManager.Object);
            var command = new DeleteUserCommand(1, new ClaimsPrincipal());

            // Act & Assert
            Assert.That(async () => await commandHandler.Handle(command, CancellationToken.None),
                Throws.TypeOf<OperationFailedException>()
                    .With.Property("Errors").Contains("Error1"));
        }

    }
}