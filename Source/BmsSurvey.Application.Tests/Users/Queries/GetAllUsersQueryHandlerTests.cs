//-----------------------------------------------------------------------
// <copyright file="GetAllUsersQueryHandlerTests.cs" company="Business Management System Ltd.">
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
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Users.Queries.GetAllUsers;
    using AutoMapper;
    using Domain.Entities.Identity;
    using Infrastructure;
    using static Infrastructure.IdentityMockHelpers;
    #endregion

    /// <summary>
    /// Summary description for GetAllUsersQueryHandlerTests
    /// </summary>
    [TestFixture]
    public class GetAllUsersQueryHandlerTests
    {
        [Test]
        public void GetAllUsersQueryHandlerCtor_UserManagerIsNull_ThrowsArgumentNullException()
        {
            var stubUserManager = MockUserManager<User>();
            var stubMapper = new Mock<IMapper>();

            //Act & Assert
            Assert.That(
                () => new GetAllUsersQueryHandler(null, stubMapper.Object),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("userManager"));
        }

        [Test]
        public void GetAllUsersQueryHandlerCtor_IMapperIsNull_ThrowsArgumentNullException()
        {
            var stubUserManager = MockUserManager<User>();
            var stubMapper = new Mock<IMapper>();

            //Act & Assert
            Assert.That(
                () => new GetAllUsersQueryHandler(stubUserManager.Object, null),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("mapper"));
        }

        [Test]
        public async Task GetAllUsersQueryHandler_ValidCommand_AllNotDeletedUsers()
        {
            var stubUserManager = MockUserManager<User>();
            var users = new List<User>
            {
                new User(){Id = 1, UserName = "Admin",Email = "test@test.com", UserRoles = new List<UserRole>(){new UserRole(){UserId = 1,RoleId = 1}}},
                new User(){Id = 2, UserName = "SurveyUser",Email = "test1@test.com",UserRoles = new List<UserRole>(){new UserRole(){UserId = 2,RoleId = 2}}},
                new User(){Id = 3, UserName = "User",Email = "test2@test.com"},
                new User(){Id = 4, UserName = "DeletedUser",Email = "test3@test.com", IsDeleted = true},
                new User(){Id = 5, UserName = "LockedUser", LockoutEnd = TestTimeProvider.Current.UtcNow.AddDays(1), TabNumber = "16637", FirstName = "TestFN", SirName = "TestSN",LastName = "TestLn", EmailConfirmed = true, Email = "test3@test.com", IsDeleted = false, UserRoles = new List<UserRole>(){new UserRole(){UserId = 5,RoleId = 1},new UserRole(){UserId = 5,RoleId = 2}}}
            }.AsQueryable();

            stubUserManager.SetupGet(x => x.Users).Returns(users);
            var stubMapper = AutoMapperFactory.Create();
            var queryHandler = new GetAllUsersQueryHandler(stubUserManager.Object, stubMapper);

            //Act 
            var query = new GetAllUsersQuery();
            var result = await queryHandler.Handle(query, CancellationToken.None);

            //Assert
            Assert.AreEqual(4, result.Count());
            Assert.IsFalse(result.Any(x => x.IsDeleted), "Deleted record in entity result!");

            var expectedUser = users.FirstOrDefault(x => x.Id == 5);
            var testUser = result.FirstOrDefault(x => x.Id == 5);

            Assert.AreEqual(expectedUser.Id, testUser.Id);
            Assert.AreEqual(true, testUser.IsLocked);
            Assert.AreEqual(expectedUser.TabNumber, testUser.TabNumber);
            Assert.AreEqual(expectedUser.UserName, testUser.UserName);
            Assert.AreEqual(expectedUser.Email, testUser.Email);
            Assert.AreEqual(expectedUser.FirstName, testUser.FirstName);
            Assert.AreEqual(expectedUser.SirName, testUser.SirName);
            Assert.AreEqual(expectedUser.LastName, testUser.LastName);
            Assert.AreEqual(expectedUser.FullName, testUser.FullName);
            Assert.AreEqual(2, testUser.Roles.Count);
            Assert.AreEqual(expectedUser.IsDeleted, testUser.IsDeleted);
            Assert.AreEqual(expectedUser.EmailConfirmed, testUser.EmailConfirmed);
        }
    }
}