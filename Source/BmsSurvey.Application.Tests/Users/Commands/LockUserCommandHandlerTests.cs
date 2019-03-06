//-----------------------------------------------------------------------
// <copyright file="LockUserCommandHandlerTests.cs" company="Business Management System Ltd.">
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
    using Application.Services;
    using Application.Users.Commands.LockUser;
    using Common.Interfaces;
    using Domain.Entities.Identity;
    using Exceptions;
    using Infrastructure;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Resources;
    using static Infrastructure.IdentityMockHelpers;

    #endregion

    /// <summary>
    /// Summary description for LockUserCommandHandlerTests
    /// </summary>
    [TestFixture]
    public class LockUserCommandHandlerTests
    {
        [Test]
        public void LockUserCommandHandlerCtor_UserManagerIsNull_ThrowsArgumentNullException()
        {
            //Act & Assert
            Assert.That(
                () => new LockUserCommandHandler(null),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("userManager"));
        }

        [Test]
        public async Task LockAsync_User1_User1IsLocked()
        {
            //Arrange
            var mockUserManager = MockUserManager<User>();
            mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(new User() { Id = 1 }));
            mockUserManager.Setup(um => um.SetLockoutEnabledAsync(It.IsAny<User>(), It.IsAny<bool>())).Returns(Task.FromResult(IdentityResult.Success));
            mockUserManager.Setup(um => um.SetLockoutEndDateAsync(It.IsAny<User>(), It.IsAny<DateTimeOffset?>())).Returns(Task.FromResult(IdentityResult.Success));
            mockUserManager.Setup(um => um.UpdateSecurityStampAsync(It.IsAny<User>())).Returns(Task.FromResult(IdentityResult.Success));
           var lockUserCommandHandler = new LockUserCommandHandler(mockUserManager.Object);

            //Act
            await lockUserCommandHandler.Handle(new LockUserCommand(1,LockOperation.Lock), new CancellationToken());

            //Assert
            mockUserManager.Verify(im => im.SetLockoutEnabledAsync(
                    It.Is<User>(usr => usr.Id == 1),
                    It.Is<bool>(enabled => enabled == true))
                , Times.Once);
            mockUserManager.Verify(im => im.SetLockoutEndDateAsync(
                    It.Is<User>(usr => usr.Id == 1),
                    It.Is<DateTimeOffset?>(lockOutEnd => lockOutEnd == DateTimeOffset.MaxValue))
                , Times.Once);
        }

        [Test]
        public async Task UnLockAsync_User1_User1IsUnLocked()
        {
            //Arrange
            var mockUserManager = MockUserManager<User>();
            mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(new User() { Id = 1 }));
            mockUserManager.Setup(um => um.SetLockoutEnabledAsync(It.IsAny<User>(), It.IsAny<bool>())).Returns(Task.FromResult(IdentityResult.Success));
            mockUserManager.Setup(um => um.SetLockoutEndDateAsync(It.IsAny<User>(), It.IsAny<DateTimeOffset?>())).Returns(Task.FromResult(IdentityResult.Success));
            mockUserManager.Setup(um => um.UpdateSecurityStampAsync(It.IsAny<User>())).Returns(Task.FromResult(IdentityResult.Success));
            var lockUserCommandHandler = new LockUserCommandHandler(mockUserManager.Object);

            //Act
            await lockUserCommandHandler.Handle(new LockUserCommand(1, LockOperation.Unlock), new CancellationToken());

            //Assert
            mockUserManager.Verify(im => im.SetLockoutEnabledAsync(
                    It.Is<User>(usr => usr.Id == 1),
                    It.Is<bool>(enabled => enabled == true))
                , Times.Once);
            mockUserManager.Verify(im => im.SetLockoutEndDateAsync(
                    It.Is<User>(usr => usr.Id == 1),
                    It.Is<DateTimeOffset?>(lockOutEnd => lockOutEnd == null))
                , Times.Once);
        }

        [Test]
        public void LockAsync_UserNotExist_WillThrowNotFoundException()
        {
            //Arrange
            var mockUserManager = MockUserManager<User>();
            var lockUserCommandHandler = new LockUserCommandHandler(mockUserManager.Object);

            //Act & Assert
            Assert.That(
                async () => await lockUserCommandHandler.Handle(new LockUserCommand(1, LockOperation.Unlock), new CancellationToken()),
                Throws.TypeOf<NotFoundException>()
                    .With.Property("Message").Contains("user")
                    .With.Property("Message").Contains("1"));

        }

        [Test]
        public void LockAsync_ErrorInSetLockoutEnabledAsync_WillThrowOperationFailedException()
        {
            //Arrange
            var mockUserManager = MockUserManager<User>();
            mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(new User() { Id = 1 }));
            mockUserManager.Setup(um => um.SetLockoutEnabledAsync(It.IsAny<User>(), It.IsAny<bool>())).Returns(Task.FromResult(IdentityResult.Failed(new IdentityError[] { new IdentityError() { Description = "Error1" } })));
            var lockUserCommandHandler = new LockUserCommandHandler(mockUserManager.Object);

            //Act & Assert
            Assert.That(
                async () => await lockUserCommandHandler.Handle(new LockUserCommand(1, LockOperation.Unlock), new CancellationToken()),
                Throws.TypeOf<OperationFailedException>()
                    .With.Property("Errors").Contains("Error1"));

        }

        [Test]
        public void LockAsync_ErrorInSetLockoutEndDateAsync_WillThrowOperationFailedException()
        {
            //Arrange
            var mockUserManager = MockUserManager<User>();
            mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(new User() { Id = 1 }));
            mockUserManager.Setup(um => um.SetLockoutEnabledAsync(It.IsAny<User>(), It.IsAny<bool>())).Returns(Task.FromResult(IdentityResult.Success));
            mockUserManager.Setup(um => um.SetLockoutEndDateAsync(It.IsAny<User>(), It.IsAny<DateTimeOffset?>())).Returns(Task.FromResult(IdentityResult.Failed(new IdentityError[] { new IdentityError() { Description = "Error2" } })));
            var lockUserCommandHandler = new LockUserCommandHandler(mockUserManager.Object);

            //Act & Assert
            Assert.That(
                async () => await lockUserCommandHandler.Handle(new LockUserCommand(1, LockOperation.Unlock), new CancellationToken()),
                Throws.TypeOf<OperationFailedException>()
                    .With.Property("Errors").Contains("Error2"));

        }

        [Test]
        public void LockAsync_ErrorInUpdateSecurityStampAsync_WillThrowOperationFailedException()
        {
            //Arrange
            var mockUserManager = MockUserManager<User>();
            mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(new User() { Id = 1 }));
            mockUserManager.Setup(um => um.SetLockoutEnabledAsync(It.IsAny<User>(), It.IsAny<bool>())).Returns(Task.FromResult(IdentityResult.Success));
            mockUserManager.Setup(um => um.SetLockoutEndDateAsync(It.IsAny<User>(), It.IsAny<DateTimeOffset?>())).Returns(Task.FromResult(IdentityResult.Success));
            mockUserManager.Setup(um => um.UpdateSecurityStampAsync(It.IsAny<User>())).Returns(Task.FromResult(IdentityResult.Failed(new IdentityError[] { new IdentityError() { Description = "Error3" } })));
            var lockUserCommandHandler = new LockUserCommandHandler(mockUserManager.Object);

            //Act & Assert
            Assert.That(
                async () => await lockUserCommandHandler.Handle(new LockUserCommand(1, LockOperation.Unlock), new CancellationToken()),
                Throws.TypeOf<OperationFailedException>()
                    .With.Property("Errors").Contains("Error3"));

        }


    }
}