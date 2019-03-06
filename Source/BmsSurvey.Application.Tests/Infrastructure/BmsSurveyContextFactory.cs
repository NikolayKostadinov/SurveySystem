//  ------------------------------------------------------------------------------------------------
//   <copyright file="TestBase.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Tests.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Security.Principal;
    using Common.Abstract;
    using Domain.Entities;
    using Domain.Entities.Identity;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Persistence;
    using Persistence.Infrastructure;
    using Persistence.Interfaces;

    public class BmsSurveyContextFactory
    {
        public static BmsSurveyDbContext Create(bool useSqlLite = false, bool empty = false)
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

            if (!empty)
            {
                SeedTestData(dbContext);
            }

            return dbContext;
        }

        private static void SeedTestData(BmsSurveyDbContext context)
        {
            var questions = new List<Question>
            {
                new Question{Id = 1,DisplayNumber = 1,QuestionType = QuestionType.Rate1to5Stars,Text = "Въпросът за живота вселената и всичко останало?"},
                new Question{Id = 2,DisplayNumber = 2,QuestionType = QuestionType.YesOrNo,Text = "Харесва ли ви \"Въпросът за живота вселената и всичко останало?\""},
                new Question{Id = 3,DisplayNumber = 3,QuestionType = QuestionType.LowMidHigh,Text = "Колко ви харесва \"Въпросът за живота вселената и всичко останало?\""},
                new Question{Id = 4,DisplayNumber = 5,QuestionType = QuestionType.Rate1to5Stars,Text = "Въпросът за живота вселената и всичко останало?"},
                new Question{Id = 5,DisplayNumber = 4,QuestionType = QuestionType.YesOrNo,Text = "Харесва ли ви \"Въпросът за живота вселената и всичко останало?\""},
                new Question{Id = 6,DisplayNumber = 6,QuestionType = QuestionType.LowMidHigh,Text = "Колко ви харесва \"Въпросът за живота вселената и всичко останало?\""},
                new Question{Id = 7,DisplayNumber = 7,QuestionType = QuestionType.Rate1to5Stars,Text = "Въпросът за живота вселената и всичко останало?"},
                new Question{Id = 9,DisplayNumber = 8,QuestionType = QuestionType.YesOrNo,Text = "Харесва ли ви \"Въпросът за живота вселената и всичко останало?\""},
                new Question{Id = 8,DisplayNumber = 9,QuestionType = QuestionType.LowMidHigh,Text = "Колко ви харесва \"Въпросът за живота вселената и всичко останало?\""},
                new Question{Id = 10,DisplayNumber = 10,QuestionType = QuestionType.FreeText,Text = "Коментирайте \"Въпросът за живота вселената и всичко останало?\""}
            };

            var completedSurveys = new List<CompletedSurvey>();

            var surveys = new List<Survey>
            {
                new Survey{ Id = 1, SurveyTitle = "Проучване за Пътеводителя",Description = "Test",PageSize = 4, ActiveFrom = TimeProvider.Current.UtcNow.Date, ActiveTo = TimeProvider.Current.UtcNow.Date.AddDays(30), Questions = questions, CompletedSurveys = completedSurveys},
                new Survey{ Id = 2, SurveyTitle = "Изтекло проучване", ActiveFrom = TimeProvider.Current.UtcNow.AddDays(-30), ActiveTo = TimeProvider.Current.UtcNow.AddDays(-1)},
                new Survey{ Id = 3, SurveyTitle = "Изтрито проучване", ActiveFrom = TimeProvider.Current.UtcNow, ActiveTo = TimeProvider.Current.UtcNow.AddDays(30), IsDeleted = true},
            };

            var roles = new List<Role>()
            {
                new Role(){Id = 1, Name = "Administrator",NormalizedName = "Administrator".ToUpper()},
                new Role(){Id = 2, Name = "SurveySupporter",NormalizedName = "SurveySupporter".ToUpper()},
            };

            var users = new List<User>
            {
                new User(){Id = 1, UserName = "Admin",Email = "test@test.com", UserRoles = new List<UserRole>(){new UserRole(){UserId = 1,RoleId = 1}}},
                new User(){Id = 2, UserName = "SurveyUser",Email = "test1@test.com",UserRoles = new List<UserRole>(){new UserRole(){UserId = 2,RoleId = 2}}},
                new User(){Id = 3, UserName = "User",Email = "test2@test.com"},
                new User(){Id = 4, UserName = "DeletedUser",Email = "test3@test.com", IsDeleted = true}
            };


            context.Surveys.AddRange(surveys);
            context.Roles.AddRange(roles);
            context.Users.AddRange(users);
            context.SaveChanges();
        }

        public static void Destroy(BmsSurveyDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}