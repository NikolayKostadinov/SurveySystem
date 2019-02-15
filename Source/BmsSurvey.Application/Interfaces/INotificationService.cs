namespace BmsSurvey.Application.Interfaces
{
    using System.Threading.Tasks;
    using Notifications.Models;

    public interface INotificationService
    {
        Task SendAsync(Message message);
    }
}
