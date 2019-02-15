namespace BmsSurvey.Application.Interfaces
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Common.Interfaces;
    using Domain.Entities.Identity;

    public interface IUserService
    {
        IStatus UpdateUserCulture(int userId, string uICulture);
        string GetUserId(ClaimsPrincipal user);
        IQueryable<User> GetAllUsers();
        IQueryable<User> GetAllUsersWithDeleted();
        Task<object> LockAsync(int id);
        Task<object> UnLockAsync(int id);
        Task<IStatus> DeleteAsync(int id, string userName);
        Task<IStatus> UpdateUser(User user, string userName);
        Task<IStatus> CreateAsync(User user, string password);
        User GetUser(ClaimsPrincipal user);
    }
}