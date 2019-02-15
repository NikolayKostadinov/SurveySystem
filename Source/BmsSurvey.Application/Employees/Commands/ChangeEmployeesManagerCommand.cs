namespace BmsSurvey.Application.Employees.Commands
{
    using MediatR;

    public class ChangeEmployeesManagerCommand : IRequest
    {
        public int EmployeeId { get; set; }

        public int ManagerId { get; set; }
    }
}
