//-----------------------------------------------------------------------
// <copyright file="CreateUserCommandHandlerTests.cs" company="Business Management System Ltd.">
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
    using Application.Users.Commands.EditUser;
    using Application.Users.Notifications.UserConfirmEmail;
    using AutoMapper;
    using Domain.Entities.Identity;
    using Infrastructure;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using static Infrastructure.IdentityMockHelpers;

    #endregion

    /// <summary>
    /// Summary description for CreateUserCommandHandlerTests
    /// </summary>
    [TestFixture]
    public class CreateUserCommandHandlerTests
    {
        [Test]
        public void CreateUserCommandHandlerCtor_UserManagerIsNull_ThrowsArgumentNullException()
        {
            var stubUserManager = MockUserManager<User>();
            var stubMediator = new Mock<IMediator>();

            //Act & Assert
            Assert.That(
                () => new CreateUserCommandHandler(null, stubMediator.Object),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("userManager"));
        }

       
        [Test]
        public void CreateUserCommandHandlerCtor_IMediatorIsNull_ThrowsArgumentNullException()
        {
            var stubUserManager = MockUserManager<User>();
            var stubMediator = new Mock<IMediator>();

            //Act & Assert
            Assert.That(
                () => new CreateUserCommandHandler(stubUserManager.Object, null),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("mediator"));
        }

        [Test]
        public async Task CreateUserCommandHandler_NewUser_UserCreated()
        {
            // Arrange
            var mockUserManager = MockUserManager<User>();
            var user = new User() { UserName = "UserForUpdate" };
            mockUserManager.Setup(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Success));
            mockUserManager.Setup(um => um.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(user));
            mockUserManager.Setup(um => um.AddToRolesAsync(It.IsAny<User>(), It.IsAny<IEnumerable<string>>())).Returns(Task.FromResult(IdentityResult.Success));
            mockUserManager.Setup(um => um.UpdateSecurityStampAsync(It.IsAny<User>())).Returns(Task.FromResult(IdentityResult.Success));
            var stubMapper = AutoMapperFactory.Create();
            var mockMediator = new Mock<IMediator>();
            var commandHandler = new CreateUserCommandHandler(mockUserManager.Object,mockMediator.Object);

            // Act
            var command = new CreateUserCommand() { UserName = user.UserName, TabNumber = "16637", FirstName = "TestFName", SirName = "TestSirName", LastName = "TestLastName", Email = "updated@test.com", Roles = new List<RoleSimpleViewModel>() { new RoleSimpleViewModel() { Id = 1, Name = "testRole" } } };
            var result = await commandHandler.Handle(command, CancellationToken.None);

            //Assert

            mockUserManager.Verify(im => im.FindByNameAsync(
                    It.Is<string>(name => name == user.UserName))
                , Times.Once);

            mockUserManager.Verify(im => im.CreateAsync(
                    It.Is<User>(usr => usr.UserName == user.UserName),
                    It.IsAny<string>())
                , Times.Once);

            mockUserManager.Verify(im => im.AddToRolesAsync(
                    It.Is<User>(usr => usr.UserName == user.UserName),
                    It.Is<IEnumerable<string>>(roles => roles.Count() == 1 && roles.Contains("testRole")))
                , Times.Once);

            //mockUserManager.Verify(im => im.UpdateSecurityStampAsync(
            //        It.Is<User>(usr => usr.UserName == user.UserName))
            //    , Times.Once);

            mockMediator.Verify(im => im.Publish(It.Is<UserConfirmEmailNotification>(uce => uce.User == user), CancellationToken.None));
        }


        //[Test]
        //public void EditUserCommandHandlerCtor_NotExistUser_NotFoundException()
        //{
        //    // Arrange
        //    var mockUserManager = MockUserManager<User>();
        //    var user = new User() { Id = 1, UserName = "UserForUpdate" };
        //    mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult<User>(null));
        //    mockUserManager.Setup(um => um.UpdateAsync(It.IsAny<User>())).Returns(Task.FromResult(IdentityResult.Success));
        //    mockUserManager.Setup(um => um.AddToRolesAsync(It.IsAny<User>(), It.IsAny<IEnumerable<string>>())).Returns(Task.FromResult(IdentityResult.Success));
        //    mockUserManager.Setup(um => um.UpdateSecurityStampAsync(It.IsAny<User>())).Returns(Task.FromResult(IdentityResult.Success));
        //    var stubMapper = AutoMapperFactory.Create();
        //    var mockMediator = new Mock<IMediator>();
        //    var commandHandler = new EditUserCommandHandler(mockUserManager.Object, stubMapper, mockMediator.Object);
        //    var command = new EditUserCommand() { Id = 1, Email = "updated@test.com", Roles = new List<RoleSimpleViewModel>() { new RoleSimpleViewModel() { Id = 1, Name = "testRole" } } };

        //    //Act & Assert
        //    Assert.That(
        //        async () => await commandHandler.Handle(command, CancellationToken.None),
        //        Throws.TypeOf<NotFoundException>()
        //            .With.Property("Message").Contains("user")
        //            .With.Property("Message").Contains("1"));
        //}

        //[Test]
        //public void EditUserCommandHandlerCtor_UpdateAsyncFails_OperationFailedException()
        //{
        //    // Arrange
        //    var mockUserManager = MockUserManager<User>();
        //    var user = new User() { Id = 1, UserName = "UserForUpdate" };
        //    mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(user));
        //    mockUserManager.Setup(um => um.UpdateAsync(It.IsAny<User>())).Returns(Task.FromResult(IdentityResult.Failed(new IdentityError() { Description = "Error1" })));
        //    mockUserManager.Setup(um => um.AddToRolesAsync(It.IsAny<User>(), It.IsAny<IEnumerable<string>>())).Returns(Task.FromResult(IdentityResult.Success));
        //    mockUserManager.Setup(um => um.UpdateSecurityStampAsync(It.IsAny<User>())).Returns(Task.FromResult(IdentityResult.Success));
        //    var stubMapper = AutoMapperFactory.Create();
        //    var mockMediator = new Mock<IMediator>();
        //    var commandHandler = new EditUserCommandHandler(mockUserManager.Object, stubMapper, mockMediator.Object);
        //    var command = new EditUserCommand() { Id = 1, Email = "updated@test.com", Roles = new List<RoleSimpleViewModel>() { new RoleSimpleViewModel() { Id = 1, Name = "testRole" } } };

        //    //Act & Assert
        //    Assert.That(
        //        async () => await commandHandler.Handle(command, CancellationToken.None),
        //        Throws.TypeOf<OperationFailedException>()
        //            .With.Property("Errors").Contains("Error1"));
        //}

        //[Test]
        //public void EditUserCommandHandlerCtor_AddToRolesAsyncFails_OperationFailedException()
        //{
        //    // Arrange
        //    var mockUserManager = MockUserManager<User>();
        //    var user = new User() { Id = 1, UserName = "UserForUpdate" };
        //    mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(user));
        //    mockUserManager.Setup(um => um.UpdateAsync(It.IsAny<User>())).Returns(Task.FromResult(IdentityResult.Success));
        //    mockUserManager.Setup(um => um.AddToRolesAsync(It.IsAny<User>(), It.IsAny<IEnumerable<string>>())).Returns(Task.FromResult(IdentityResult.Failed(new IdentityError() { Description = "Error2" })));
        //    mockUserManager.Setup(um => um.UpdateSecurityStampAsync(It.IsAny<User>())).Returns(Task.FromResult(IdentityResult.Success));
        //    var stubMapper = AutoMapperFactory.Create();
        //    var mockMediator = new Mock<IMediator>();
        //    var commandHandler = new EditUserCommandHandler(mockUserManager.Object, stubMapper, mockMediator.Object);
        //    var command = new EditUserCommand() { Id = 1, Email = "updated@test.com", Roles = new List<RoleSimpleViewModel>() { new RoleSimpleViewModel() { Id = 1, Name = "testRole" } } };

        //    //Act & Assert
        //    Assert.That(
        //        async () => await commandHandler.Handle(command, CancellationToken.None),
        //        Throws.TypeOf<OperationFailedException>()
        //            .With.Property("Errors").Contains("Error2"));
        //}

        //[Test]
        //public void EditUserCommandHandlerCtor_UpdateSecurityStampAsyncFails_OperationFailedException()
        //{
        //    // Arrange
        //    var mockUserManager = MockUserManager<User>();
        //    var user = new User() { Id = 1, UserName = "UserForUpdate" };
        //    mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(user));
        //    mockUserManager.Setup(um => um.UpdateAsync(It.IsAny<User>())).Returns(Task.FromResult(IdentityResult.Success));
        //    mockUserManager.Setup(um => um.AddToRolesAsync(It.IsAny<User>(), It.IsAny<IEnumerable<string>>())).Returns(Task.FromResult(IdentityResult.Success));
        //    mockUserManager.Setup(um => um.UpdateSecurityStampAsync(It.IsAny<User>())).Returns(Task.FromResult(IdentityResult.Failed(new IdentityError() { Description = "Error3" })));
        //    var stubMapper = AutoMapperFactory.Create();
        //    var mockMediator = new Mock<IMediator>();
        //    var commandHandler = new EditUserCommandHandler(mockUserManager.Object, stubMapper, mockMediator.Object);
        //    var command = new EditUserCommand() { Id = 1, Email = "updated@test.com", Roles = new List<RoleSimpleViewModel>() { new RoleSimpleViewModel() { Id = 1, Name = "testRole" } } };

        //    //Act & Assert
        //    Assert.That(
        //        async () => await commandHandler.Handle(command, CancellationToken.None),
        //        Throws.TypeOf<OperationFailedException>()
        //            .With.Property("Errors").Contains("Error3"));
        //}
    }
}