//-----------------------------------------------------------------------
// <copyright file="ChangeUserCultureCommandHandler.cs" company="Business Management System Ltd.">
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
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Users.Commands.ChangeUserCulture;
    using Application.Users.Commands.SendConfirmationEmail;
    using Domain.Entities.Identity;
    using Exceptions;
    using Interfaces;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using static Infrastructure.IdentityMockHelpers;

    #endregion

    /// <summary>
    /// Summary description for ChangeUserCultureCommandHandler
    /// </summary>
    [TestFixture]
    public class ChangeUserCultureCommandHandlerTests
    {
        [Test]
        public void ChangeUserCultureCommandHandlerCtor_UserManagerIsNull_ThrowsArgumentNullException()
        {
            var stubUserManager = MockUserManager<User>();
            var stubSupportedCulturesService = new Mock<ISupportedCulturesService>();

            //Act & Assert
            Assert.That(
                () => new ChangeUserCultureCommandHandler(null, stubSupportedCulturesService.Object),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("userManager"));
        }


        [Test]
        public void ChangeUserCultureCommandHandlerCtor_ISupportedCulturesServiceIsNull_ThrowsArgumentNullException()
        {
            var stubUserManager = MockUserManager<User>();
            var stubSupportedCulturesService = new Mock<ISupportedCulturesService>();

            //Act & Assert
            Assert.That(
                () => new ChangeUserCultureCommandHandler(stubUserManager.Object, null),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("supportedCulturesService"));
        }

        [Test]
        public async Task ChangeUserCultureCommandHandler_ValidCommand_ChangesUserCulture()
        {
            //Arrange
            var user = new User() { Id = 1, UserName = "UserForUpdate" };
            var mockUserManager = MockUserManager<User>();
            mockUserManager.Setup(um => um.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(user));
            mockUserManager.Setup(um => um.UpdateAsync(It.IsAny<User>())).Returns(Task.FromResult(IdentityResult.Success));
            var stubSupportedCulturesService = new Mock<ISupportedCulturesService>();
            stubSupportedCulturesService.Setup(x => x.IsCultureSupported(It.IsAny<string>())).Returns(true);
            var commandHandler = new ChangeUserCultureCommandHandler(mockUserManager.Object, stubSupportedCulturesService.Object);
            var stubIIdentity = new Mock<IIdentity>();
            stubIIdentity.SetupGet(x => x.Name).Returns(user.UserName);
            var stubIPrincipal = new Mock<IPrincipal>();
            stubIPrincipal.SetupGet(x => x.Identity).Returns(stubIIdentity.Object);

            //Act
            var command = new ChangeUserCultureCommand(stubIPrincipal.Object, "bg");
            await commandHandler.Handle(command, CancellationToken.None);

            //Assert

            mockUserManager.Verify(im => im.FindByNameAsync(
                    It.Is<string>(name => name == user.UserName))
                , Times.Once);

            mockUserManager.Verify(im => im.UpdateAsync(
                    It.Is<User>(usr => usr.Id == 1 && usr.CultureId == "bg"))
                , Times.Once);
        }

        [Test]
        public void  ChangeUserCultureCommandHandler_NotExistUser_NotFoundException()
        {
            var user = new User() { Id = 1, UserName = "UserForUpdate" };
            var mockUserManager = MockUserManager<User>();
            mockUserManager.Setup(um => um.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            mockUserManager.Setup(um => um.UpdateAsync(It.IsAny<User>())).Returns(Task.FromResult(IdentityResult.Success));
            var stubSupportedCulturesService = new Mock<ISupportedCulturesService>();
            stubSupportedCulturesService.Setup(x => x.IsCultureSupported(It.IsAny<string>())).Returns(true);
            var commandHandler = new ChangeUserCultureCommandHandler(mockUserManager.Object, stubSupportedCulturesService.Object);
            var stubIIdentity = new Mock<IIdentity>();
            stubIIdentity.SetupGet(x => x.Name).Returns(user.UserName);
            var stubIPrincipal = new Mock<IPrincipal>();
            stubIPrincipal.SetupGet(x => x.Identity).Returns(stubIIdentity.Object);
            var command = new ChangeUserCultureCommand(stubIPrincipal.Object, "bg");

            //Act & Assert
            Assert.That(
                async () => await commandHandler.Handle(command, CancellationToken.None),
                Throws.TypeOf<NotFoundException>()
                    .With.Message.Match("user")
                    .With.Message.Match(user.UserName)
                    .With.Property("Key").EqualTo(user.UserName));
        }

        [Test]
        public void ChangeUserCultureCommandHandler_UpdateAsyncFails_OperationFailedException()
        {
            var user = new User() { Id = 1, UserName = "UserForUpdate" };
            var mockUserManager = MockUserManager<User>();
            mockUserManager.Setup(um => um.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(user));
            mockUserManager.Setup(um => um.UpdateAsync(It.IsAny<User>())).Returns(Task.FromResult(IdentityResult.Failed(new IdentityError(){Description = "Error1"})));
            var stubSupportedCulturesService = new Mock<ISupportedCulturesService>();
            stubSupportedCulturesService.Setup(x => x.IsCultureSupported(It.IsAny<string>())).Returns(true);
            var commandHandler = new ChangeUserCultureCommandHandler(mockUserManager.Object, stubSupportedCulturesService.Object);
            var stubIIdentity = new Mock<IIdentity>();
            stubIIdentity.SetupGet(x => x.Name).Returns(user.UserName);
            var stubIPrincipal = new Mock<IPrincipal>();
            stubIPrincipal.SetupGet(x => x.Identity).Returns(stubIIdentity.Object);
            var command = new ChangeUserCultureCommand(stubIPrincipal.Object, "bg");

            //Act & Assert
            Assert.That(
                async () => await commandHandler.Handle(command, CancellationToken.None),
                Throws.TypeOf<OperationFailedException>()
                    .With.Property("Errors").Contains("Error1"));
        }

        [Test]
        public void ChangeUserCultureCommandHandler_UnsupportedCulture_OperationFailedException()
        {
            var user = new User() { Id = 1, UserName = "UserForUpdate" };
            var mockUserManager = MockUserManager<User>();
            mockUserManager.Setup(um => um.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(user));
            mockUserManager.Setup(um => um.UpdateAsync(It.IsAny<User>())).Returns(Task.FromResult(IdentityResult.Success));
            var stubSupportedCulturesService = new Mock<ISupportedCulturesService>();
            stubSupportedCulturesService.Setup(x => x.IsCultureSupported(It.IsAny<string>())).Returns(false);
            var commandHandler = new ChangeUserCultureCommandHandler(mockUserManager.Object, stubSupportedCulturesService.Object);
            var stubIIdentity = new Mock<IIdentity>();
            stubIIdentity.SetupGet(x => x.Name).Returns(user.UserName);
            var stubIPrincipal = new Mock<IPrincipal>();
            stubIPrincipal.SetupGet(x => x.Identity).Returns(stubIIdentity.Object);
            var command = new ChangeUserCultureCommand(stubIPrincipal.Object, "bg");

            //Act & Assert
            Assert.That(
                async () => await commandHandler.Handle(command, CancellationToken.None),
                Throws.TypeOf<OperationFailedException>()
                    .With.Message.Matches("'bg'{1}"));
        }
    }
}