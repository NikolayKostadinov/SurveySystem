namespace BmsSurvey.Application.Interfaces
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Common.Interfaces;
    using Domain.Entities.Identity;
    using Models;

    public interface IUserService
    {
        Task<LockStatusDto> LockAsync(int id);
        Task<LockStatusDto> UnLockAsync(int id);
        Task<IStatus> DeleteAsync(int id, string userName);
        //Task<IStatus> UpdateUser(User user, string userName);
        //User GetUser(ClaimsPrincipal user);
    }
}