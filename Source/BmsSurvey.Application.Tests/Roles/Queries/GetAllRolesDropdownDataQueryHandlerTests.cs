//-----------------------------------------------------------------------
// <copyright file="GetAllRolesDropdownDataQueryHandlerTests.cs" company="Business Management System Ltd.">
//     Copyright "2019" (c) Business Management System Ltd. All rights reserved.
// </copyright>
// <author>Nikolay.Kostadinov</author>
// <convention>
// Test method convention
// [UnitOfWork]_[Scenario]_[ExpectedBehavior]
// </convention>
//-----------------------------------------------------------------------

namespace BmsSurvey.Application.Tests.Roles.Queries
{
    #region Using 

    using System;
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using BmsSurvey.Application.Roles.Queries.GetAllRolesDropdownData;
    using Domain.Entities.Identity;
    using Infrastructure;
    using static Infrastructure.IdentityMockHelpers;
    #endregion

    /// <summary>
    /// Summary description for GetAllRolesDropdownDataQueryHandlerTests
    /// </summary>
    [TestFixture]
    public class GetAllRolesDropdownDataQueryHandlerTests
    {
        [Test]
        public void GetAllRolesDropdownDataQueryHandlerCtor_RoleManagerIsNull_ThrowsArgumentNullException()
        {
            //Arrange
            var stubRoleManager = MockRoleManager<Role>();
            var stubMapper = new Mock<IMapper>();
            //Act & Assert
            Assert.That(
                () => new GetAllRolesDropdownDataQueryHandler(null, stubMapper.Object),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("roleManager"));
        }

        [Test]
        public void GetAllRolesDropdownDataQueryHandlerCtor_IMapperIsNull_ThrowsArgumentNullException()
        {
            //Arrange
            var stubRoleManager = MockRoleManager<Role>();
            var stubMapper = new Mock<IMapper>();
            //Act & Assert
            Assert.That(
                () => new GetAllRolesDropdownDataQueryHandler(stubRoleManager.Object, null),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Property("ParamName").Contains("mapper"));
        }

        [Test]
        public async Task GetAllRolesDropdownDataQueryHandler_ValidQuery_ReturnAllNotDeletedRoles()
        {
            //Arrange
            var stubRoleManager = MockRoleManager<Role>();
            stubRoleManager.SetupGet(rm => rm.Roles)
                .Returns(new List<Role>
                {
                    new Role {Id = 1, Name = "Role1", IsDeleted = false},
                    new Role {Id = 2, Name = "Role2", IsDeleted = false},
                    new Role {Id = 3, Name = "DeletedRole", IsDeleted = true},
                }.AsQueryable());

            var stubMapper = AutoMapperFactory.Create();
            var queryHandler = new GetAllRolesDropdownDataQueryHandler(stubRoleManager.Object, stubMapper);
            
            //Act

            var result = await queryHandler.Handle(new GetAllRolesDropdownDataQuery(), CancellationToken.None);

            //Act & Assert

            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Select(x=>x.Id).ToArray().Contains(1));
            Assert.IsTrue(result.Select(x=>x.Id).ToArray().Contains(2));
            Assert.IsFalse(result.Any(r=>r.Name == "DeletedRole"),"Contains deleted records!");
        }
    }
}