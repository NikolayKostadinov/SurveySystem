namespace BmsSurvey.Domain.Entities.Identity
{
    using Interfaces;
    using Microsoft.AspNetCore.Identity;

    public class UserRole : IdentityUserRole<int>, IEntity
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}