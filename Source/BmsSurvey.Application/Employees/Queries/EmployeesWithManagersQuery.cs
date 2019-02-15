namespace BmsSurvey.Application.Employees.Queries
{
    using System.Collections.Generic;
    using MediatR;
    using Models;

    public class EmployeesWithManagersQuery : IRequest<IEnumerable<EmployeeManagerModel>>
    {
    }
}
