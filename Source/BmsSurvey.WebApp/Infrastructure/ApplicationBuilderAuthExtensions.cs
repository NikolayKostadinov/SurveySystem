namespace BmsSurvey.WebApp.Infrastructure
{
    using System.Threading.Tasks;
    using Domain.Entities.Identity;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderAuthExtensions
    {
        private static Role[] roles = {
            new Role("Administrator","Administration of the system"),
            new Role("Employee", "Person who can register register his overtime!"),
            new Role("OvertimeReporter", "Person who can run all the reports!")
        };
        public static async Task SeedDatabaseAsync(this IApplicationBuilder app)
        {
            var serviceFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = serviceFactory.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role.Name))
                    {
                        await roleManager.CreateAsync(role);
                    }
                }

                if (await userManager.FindByNameAsync("Administrator") == null)
                {
                    var user = new User() { UserName = "Administrator", Email = "Nikolay.Kostadinov@bmsys.eu", EmailConfirmed = true };

                    await userManager.CreateAsync(user, "K@lvad0s");

                    await userManager.AddToRoleAsync(user, "Administrator");
                }
            }
        }
    }
}
