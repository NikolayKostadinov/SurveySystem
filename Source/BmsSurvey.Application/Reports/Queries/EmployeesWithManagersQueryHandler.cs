﻿namespace BmsSurvey.Application.Reports.Queries
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Dapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Persistence;

    public class EmployeesWithManagersQueryHandler : IRequestHandler<EmployeesWithManagersQuery, IEnumerable<EmployeeManagerModel>>
    {
        private readonly BmsSurveyDbContext _context;

        public EmployeesWithManagersQueryHandler(BmsSurveyDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<EmployeeManagerModel>> Handle(EmployeesWithManagersQuery request, CancellationToken cancellationToken)
        {
            var sql = @"
SELECT e.EmployeeId as EmployeeId, e.FirstName as EmployeeFirstName, e.LastName as EmployeeLastName, e.Title as EmployeeTitle,
	   m.EmployeeId as ManagerId, m.FirstName as ManagerFirstName, m.LastName as ManagerLastName, m.Title as ManagerTitle
FROM employees AS e
JOIN employees AS m ON e.ReportsTo = m.EmployeeID
WHERE e.ReportsTo is not null";

            return _context.Database.GetDbConnection()
                .QueryAsync<EmployeeManagerModel>(sql);
        }
    }
}
