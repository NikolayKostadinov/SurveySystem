namespace BmsSurvey.Persistence
{
    using Infrastructure;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class BmsSurveyDbContextFactory : DesignTimeDbContextFactoryBase<BmsSurveyDbContext>
    {
        protected override BmsSurveyDbContext CreateNewInstance(DbContextOptions<BmsSurveyDbContext> options,
            IPersister persister, ICurrentPrincipalProvider principalProvider)
        {
            return new BmsSurveyDbContext(options, persister, principalProvider);
        }

    }
}
