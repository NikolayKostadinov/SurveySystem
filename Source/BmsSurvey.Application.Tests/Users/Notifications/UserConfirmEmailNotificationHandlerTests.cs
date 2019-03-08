//-----------------------------------------------------------------------
// <copyright file="UserConfirmEmailNotificationHandlerTests.cs" company="Business Management System Ltd.">
//     Copyright "2019" (c) Business Management System Ltd. All rights reserved.
// </copyright>
// <author>Nikolay.Kostadinov</author>
// <convention>
// Test method convention
// [UnitOfWork]_[Scenario]_[ExpectedBehavior]
// </convention>
//-----------------------------------------------------------------------

namespace BmsSurvey.Application.Tests.Users.Notifications
{
    #region Using 

    using System;
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Notifications.Models;
    using Application.Users.Notifications.UserConfirmEmail;
    using Domain.Entities.Identity;
    using Interfaces;

    #endregion

    /// <summary>
    /// Summary description for UserConfirmEmailNotificationHandlerTests
    /// </summary>
    [TestFixture]
    public class UserConfirmEmailNotificationHandlerTests
    {
        [Test]
        public void UserConfirmEmailNotificationHandlerCtor_MailNotificationServiceIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var stubMailNotificationService = new Mock<IMailNotificationService>();
            var stubCreationMessageService = new Mock<IUserCreationMessageService>();

            //Act & Assert
            Assert.That(
                () => new UserConfirmEmailNotificationHandler(null, stubCreationMessageService.Object),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("mailNotificationService"));
        }

        [Test]
        public void UserConfirmEmailNotificationHandlerCtor_CreationMessageServiceIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var stubMailNotificationService = new Mock<IMailNotificationService>();
            var stubCreationMessageService = new Mock<IUserCreationMessageService>();

            //Act & Assert
            Assert.That(
                () => new UserConfirmEmailNotificationHandler(stubMailNotificationService.Object, null),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("creationMessageService"));
        }

        [Test]
        public async Task UserConfirmEmailNotificationHandler_normalNotification_WillSendEmail()
        {
            // Arrange
            var user = new User()
            {
                Id = 2,
                UserName = "SurveyUser",
                Email = "test1@test.com",
                UserRoles = new List<UserRole>() { new UserRole() { UserId = 2, RoleId = 2 } }
            };
            var message = new Message()
            { From = "test@test.com", To = user.Email, Subject = "Test notification", Body = "Some sample text" };
            var mockMailNotificationService = new Mock<IMailNotificationService>();
            var mockCreationMessageService = new Mock<IUserCreationMessageService>();
            mockCreationMessageService.Setup(x => x.GetMessageAsync(It.IsAny<User>())).Returns(Task.FromResult(message));
            var notificationHandler =
                new UserConfirmEmailNotificationHandler(mockMailNotificationService.Object, mockCreationMessageService.Object);

            //Act
            var notification = new UserConfirmEmailNotification(user);
            await notificationHandler.Handle(notification, CancellationToken.None);

            //Assert
            mockCreationMessageService.Verify(im =>
                im.GetMessageAsync(It.Is<User>(usr => usr.Id == user.Id
                                                    && usr.UserName == user.UserName)));
            mockMailNotificationService.Verify(im =>
                im.SendAsync(It.Is<Message>(m => m.To == message.To
                                                 && m.From == message.From
                                                 && m.Subject == message.Subject
                                                 && m.Body == message.Body)));
        }
    }
}
