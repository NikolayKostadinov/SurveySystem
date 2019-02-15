namespace BmsSurvey.Application.Reports.Queries
{
    using System.Collections.Generic;
    using MediatR;
    using Models;

    public class EmployeesWithManagersQuery : IRequest<IEnumerable<EmployeeManagerModel>>
    {
    }
}
