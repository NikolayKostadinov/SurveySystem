namespace BmsSurvey.Infrastructure
{
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Application.Notifications.Models;

    public class NotificationService : INotificationService
    {
        public Task SendAsync(Message message)
        {
            return Task.CompletedTask;
        }
    }
}
