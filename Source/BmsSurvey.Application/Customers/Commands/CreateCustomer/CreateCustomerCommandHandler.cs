using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Customers.Commands.CreateCustomer
{
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Entities;
    using Interfaces;
    using MediatR;
    using Persistence;

    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Unit>
    {
        private readonly BmsSurveyDbContext context;
        private readonly INotificationService notificationService;
        private readonly IMediator mediator;

        public CreateCustomerCommandHandler(
            BmsSurveyDbContext context,
            INotificationService notificationService,
            IMediator mediator)
        {
            this.context = context;
            this.notificationService = notificationService;
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var entity = new Customer
            {
                CustomerId = request.Id,
                Address = request.Address,
                City = request.City,
                CompanyName = request.CompanyName,
                ContactName = request.ContactName,
                ContactTitle = request.ContactTitle,
                Country = request.Country,
                Fax = request.Fax,
                Phone = request.Phone,
                PostalCode = request.PostalCode
            };

            context.Customers.Add(entity);

            await context.SaveChangesAsync(cancellationToken);

            await mediator.Publish(new CustomerCreatedNotification() { CustomerId = entity.CustomerId }, cancellationToken);

            return Unit.Value;
        }
    }
}
