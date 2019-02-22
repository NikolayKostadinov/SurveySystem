namespace BmsSurvey.Application.Customers.Commands.CreateCustomer
{
    using System.Threading;
    using System.Threading.Tasks;
    using Interfaces;
    using MediatR;
    using Notifications.Models;
    using Users.Commands.CreateUser;

    public class CustomerCreatedNotification : INotification
    {
        public string CustomerId { get; set; }

        public class CustomerCreatedHandler : INotificationHandler<UserCreatedNotification>
        {
            //private readonly INotificationService _notification;

            //public CustomerCreatedHandler(INotificationService notification)
            //{
            //    _notification = notification;
            //}

            public Task Handle(UserCreatedNotification notification, CancellationToken cancellationToken)
            {
                //await _notification.SendAsync(new Message());
                return Task.CompletedTask;
            }
        }
    }
}
