namespace BmsSurvey.Application.Reports.Queries
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Dapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Persistence;

    public class EmployeesWithManagersViewQueryHandler : IRequestHandler<EmployeesWithManagersViewQuery, IEnumerable<EmployeeManagerModel>>
    {
        private readonly BmsSurveyDbContext _context;

        public EmployeesWithManagersViewQueryHandler(BmsSurveyDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<EmployeeManagerModel>> Handle(EmployeesWithManagersViewQuery request, CancellationToken cancellationToken)
        {
            var sql = "select * from viewEmployeesWithManagers";

            return _context.Database.GetDbConnection()
                .QueryAsync<EmployeeManagerModel>(sql);
        }
    }
}
