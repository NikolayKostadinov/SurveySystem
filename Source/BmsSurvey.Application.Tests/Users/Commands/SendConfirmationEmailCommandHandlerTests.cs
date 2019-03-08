//-----------------------------------------------------------------------
// <copyright file="SendConfirmationEmailCommandHandlerTests.cs" company="Business Management System Ltd.">
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
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Roles.Models;
    using Application.Users.Commands.CreateUser;
    using Application.Users.Commands.SendConfirmationEmail;
    using Application.Users.Notifications.UserConfirmEmail;
    using Domain.Entities.Identity;
    using Exceptions;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore.Migrations;
    using static Infrastructure.IdentityMockHelpers;
    #endregion

    /// <summary>
    /// Summary description for SendConfirmationEmailCommandHandlerTests
    /// </summary>
    [TestFixture]
    public class SendConfirmationEmailCommandHandlerTests
    {
        [Test]
        public void SendConfirmationEmailCommandHandlerCtor_UserManagerIsNull_ThrowsArgumentNullException()
        {
            var stubUserManager = MockUserManager<User>();
            var stubMediator = new Mock<IMediator>();

            //Act & Assert
            Assert.That(
                () => new SendConfirmationEmailCommandHandler(null, stubMediator.Object),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("userManager"));
        }


        [Test]
        public void SendConfirmationEmailCommandHandlerCtor_IMediatorIsNull_ThrowsArgumentNullException()
        {
            var stubUserManager = MockUserManager<User>();
            var stubMediator = new Mock<IMediator>();

            //Act & Assert
            Assert.That(
                () => new SendConfirmationEmailCommandHandler(stubUserManager.Object, null),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("mediator"));
        }

        [Test]
        public async Task SendConfirmationEmailCommandHandler_ExistUser_SendEmail()
        {
            // Arrange
            var mockUserManager = MockUserManager<User>();
            var user = new User() { Id = 1, UserName = "UserForUpdate" };
            mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(user));
            var mockMediator = new Mock<IMediator>();
            var commandHandler = new SendConfirmationEmailCommandHandler(mockUserManager.Object, mockMediator.Object);

            // Act
            var command = new SendConfirmationEmailCommand() { Id = 1 };
            var result = await commandHandler.Handle(command, CancellationToken.None);

            //Assert

            mockUserManager.Verify(im => im.FindByIdAsync(
                    It.Is<string>(id => id == user.Id.ToString()))
                , Times.Once);

            mockMediator.Verify(im => im.Publish(It.Is<UserConfirmEmailNotification>(uce => uce.User == user), CancellationToken.None));
        }

        [Test]
        public void SendConfirmationEmailCommandHandler_NotExistUser_NotFoundException()
        {
            // Arrange
            var mockUserManager = MockUserManager<User>();
            var user = new User() { Id = 1, UserName = "UserForUpdate" };
            mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            var mockMediator = new Mock<IMediator>();
            var commandHandler = new SendConfirmationEmailCommandHandler(mockUserManager.Object, mockMediator.Object);
            var command = new SendConfirmationEmailCommand() { Id = 1 };

            //Act & Assert
            Assert.That(
                async () => await commandHandler.Handle(command, CancellationToken.None),
                Throws.TypeOf<NotFoundException>()
                    .With.Message.Match("user")
                    .With.Message.Match("1")
                    .With.Property("Key").EqualTo(user.Id));
        }

    }
}