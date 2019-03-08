//-----------------------------------------------------------------------
// <copyright file="GetAllUsersWithDeletedQueryHandlerTests.cs" company="Business Management System Ltd.">
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
    using Application.Users.Queries.GetAllUsersWithDeleted;
    using AutoMapper;
    using Domain.Entities.Identity;
    using Infrastructure;
    using static Infrastructure.IdentityMockHelpers;

    #endregion

    /// <summary>
    /// Summary description for GetAllUsersWithDeletedQueryHandlerTests
    /// </summary>
    [TestFixture]
    public class GetAllUsersWithDeletedQueryHandlerTests
    {
        [Test]
        public void GetAllUsersWithDeletedQueryHandlerCtor_UserManagerIsNull_ThrowsArgumentNullException()
        {
            var stubUserManager = MockUserManager<User>();
            var stubMapper = new Mock<IMapper>();

            //Act & Assert
            Assert.That(
                () => new GetAllUsersWithDeletedQueryHandler(null, stubMapper.Object),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("userManager"));
        }

        [Test]
        public void GetAllUsersWithDeletedQueryHandlerCtor_IMapperIsNull_ThrowsArgumentNullException()
        {
            var stubUserManager = MockUserManager<User>();
            var stubMapper = new Mock<IMapper>();

            //Act & Assert
            Assert.That(
                () => new GetAllUsersWithDeletedQueryHandler(stubUserManager.Object, null),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("mapper"));
        }

        [Test]
        public async Task GetAllUsersWithDeletedQueryHandler_ValidCommand_AllNotDeletedUsers()
        {
            var stubUserManager = MockUserManager<User>();
            var users = new List<User> { new User { Id = 1 }, new User { Id = 2 }, new User { Id = 3, IsDeleted = true } }.AsQueryable();

            stubUserManager.SetupGet(x => x.Users).Returns(users);
            var stubMapper = AutoMapperFactory.Create();
            var queryHandler = new GetAllUsersWithDeletedQueryHandler(stubUserManager.Object, stubMapper);

            //Act 
            var query = new GetAllUsersWithDeletedQuery();
            var result = await queryHandler.Handle(query, CancellationToken.None);

            //Assert
            Assert.AreEqual(3, result.Count());
            Assert.True(result.Any(x => x.IsDeleted), "Deleted record not in entity result!");
        }
    }
}