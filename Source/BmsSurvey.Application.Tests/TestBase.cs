//  ------------------------------------------------------------------------------------------------
//   <copyright file="TestBase.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Tests
{
    using System;
    using System.Security.Claims;
    using System.Security.Principal;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Persistence;
    using Persistence.Infrastructure;
    using Persistence.Interfaces;

    internal class TestBase
    {
        public BmsSurveyDbContext GetDbContext(bool useSqlLite = false)
        {
            var builder = new DbContextOptionsBuilder<BmsSurveyDbContext>();
            var stubIIdentity = new Mock<IIdentity>();
            stubIIdentity.SetupGet(x => x.Name).Returns("TestUser");
            var stubIPrincipal = new Mock<IPrincipal>();
            stubIPrincipal.SetupGet(x => x.Identity).Returns(stubIIdentity.Object);
            var stubPrincipalProvider = new Mock<ICurrentPrincipalProvider>();
            stubPrincipalProvider.Setup(x => x.GetCurrentPrincipal()).Returns(stubIPrincipal.Object);

            if (useSqlLite)
            {
                builder.UseSqlite("DataSource=:memory:", x => { });
            }
            else
            {
                builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            }

            var dbContext = new BmsSurveyDbContext(builder.Options,
                new AuditablePersister(), 
                stubPrincipalProvider.Object);

            if (useSqlLite)
            {
                // SQLite needs to open connection to the DB.
                // Not required for in-memory-database.
                dbContext.Database.OpenConnection();
            }

            dbContext.Database.EnsureCreated();

            return dbContext;
        }
    }
}