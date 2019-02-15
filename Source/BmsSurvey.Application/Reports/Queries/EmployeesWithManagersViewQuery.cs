namespace BmsSurvey.Application.Reports.Queries
{
    using System.Collections.Generic;
    using MediatR;
    using Models;

    public class EmployeesWithManagersViewQuery : IRequest<IEnumerable<EmployeeManagerModel>>
    {
    }
}
