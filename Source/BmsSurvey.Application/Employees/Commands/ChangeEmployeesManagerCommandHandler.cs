namespace BmsSurvey.Application.Employees.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Persistence;

    public class ChangeEmployeesManagerCommandHandler : IRequestHandler<ChangeEmployeesManagerCommand>
    {
        private readonly BmsSurveyDbContext _context;

        public ChangeEmployeesManagerCommandHandler(BmsSurveyDbContext context)
        {
            _context = context;
        }

        public Task<Unit> Handle(ChangeEmployeesManagerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
