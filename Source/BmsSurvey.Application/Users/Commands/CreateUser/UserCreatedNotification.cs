namespace BmsSurvey.Application.Users.Commands.CreateUser
{
    using System.Threading;
    using System.Threading.Tasks;
    using Castle.Core.Logging;
    using Interfaces;
    using MediatR;
    using Notifications.Models;

    public class UserCreatedNotification : INotification
    {
        public int UserId { get; set; }

        public class CustomerCreatedHandler : INotificationHandler<UserCreatedNotification>
        {
            private readonly INotificationService _notification;

            public CustomerCreatedHandler(INotificationService notification)
            {
                _notification = notification;
            }

            public async Task Handle(UserCreatedNotification notification, CancellationToken cancellationToken)
            {
                await _notification.SendAsync(new Message());
            }
        }
    }
}
