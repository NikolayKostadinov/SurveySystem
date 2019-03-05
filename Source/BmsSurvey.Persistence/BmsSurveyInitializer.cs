//  ------------------------------------------------------------------------------------------------
//   <copyright file="BmsSurveyInitializer.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Persistence
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Abstract;
    using Domain.Entities;
    using Domain.Entities.Identity;
    using Microsoft.AspNetCore.Identity;

    #endregion

    public class BmsSurveyInitializer
    {
        private static readonly Role[] Roles =
        {
            new Role("Administrator", "Administration of the system"),
            new Role("SurveySupporter", "Person who can create and modify surveys and their questions!")
        };

        private readonly BmsSurveyDbContext context;
        private readonly RoleManager<Role> roleManager;
        private readonly UserManager<User> userManager;

        public BmsSurveyInitializer(BmsSurveyDbContext context, UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        public async Task SeedEverythingAsync()
        {
            context.Database.EnsureCreated();

            if (context.Surveys.Any()) return;


            await SeedSurveysAsync();
            await SeedUsersAndRolesAsync();

        }

        private async Task SeedSurveysAsync()
        {
            var questions = new List<Question>
            {
                new Question
                {
                    //Id = 1,
                    DisplayNumber = 1,
                    QuestionType = QuestionType.Rate1to5Stars,
                    Text = "Въпросът за живота вселената и всичко останало?"
                },

                new Question
                {
                    //Id = 2,
                    DisplayNumber = 2,
                    QuestionType = QuestionType.YesOrNo,
                    Text = "Харесва ли ви \"Въпросът за живота вселената и всичко останало?\""
                },
                new Question
                {
                    //Id = 3,
                    DisplayNumber = 3,
                    QuestionType = QuestionType.LowMidHigh,
                    Text = "Колко ви харесва \"Въпросът за живота вселената и всичко останало?\""
                },
                new Question
                {
                    //Id = 4,
                    DisplayNumber = 5,
                    QuestionType = QuestionType.Rate1to5Stars,
                    Text = "Въпросът за живота вселената и всичко останало?"
                },

                new Question
                {
                    //Id = 5,
                    DisplayNumber = 4,
                    QuestionType = QuestionType.YesOrNo,
                    Text = "Харесва ли ви \"Въпросът за живота вселената и всичко останало?\""
                },
                new Question
                {
                    //Id = 6,
                    DisplayNumber = 6,
                    QuestionType = QuestionType.LowMidHigh,
                    Text = "Колко ви харесва \"Въпросът за живота вселената и всичко останало?\""
                },
                new Question
                {
                    //Id = 7,
                    DisplayNumber = 7,
                    QuestionType = QuestionType.Rate1to5Stars,
                    Text = "Въпросът за живота вселената и всичко останало?"
                },

                new Question
                {
                    //Id = 9,
                    DisplayNumber = 8,
                    QuestionType = QuestionType.YesOrNo,
                    Text = "Харесва ли ви \"Въпросът за живота вселената и всичко останало?\""
                },
                new Question
                {
                    //Id = 8,
                    DisplayNumber = 9,
                    QuestionType = QuestionType.LowMidHigh,
                    Text = "Колко ви харесва \"Въпросът за живота вселената и всичко останало?\""
                }
            };

            var survey = new Survey
            {
                //Id=1,
                SurveyTitle = "Проучване за Пътеводителя",
                ActiveFrom = TimeProvider.Current.UtcNow,
                ActiveTo = TimeProvider.Current.UtcNow.AddDays(30),
                Questions = questions
            };

            context.Surveys.Add(survey);
            await context.SaveChangesAsync();
        }

        private async Task SeedUsersAndRolesAsync()
        {
            foreach (var role in Roles)
                if (!await roleManager.RoleExistsAsync(role.Name))
                    await roleManager.CreateAsync(role);

            if (await userManager.FindByNameAsync("Administrator") == null)
            {
                var user = new User
                { UserName = "Administrator", Email = "Nikolay.Kostadinov@bmsys.eu", EmailConfirmed = true };

                await userManager.CreateAsync(user, "K@lvad0s");

                await userManager.AddToRoleAsync(user, "Administrator");
            }
        }
    }
}