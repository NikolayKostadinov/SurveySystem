namespace BmsSurvey.Application.Managers.Commands
{
    using MediatR;

    public class ChangeEmployeeReportToCommand : IRequest
    {
        public int EmployeeId { get; set; }

        public int ManagerId { get; set; }
    }
}
