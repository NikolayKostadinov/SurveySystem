namespace BmsSurvey.Domain.Entities.Identity
{
    using System;
    using System.Collections.Generic;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;
    using ValueObjects;

    public class User: IdentityUser<int>, IAuditInfo, IDeletableEntity
    {
        public User()
        {
            this.UserRoles = new HashSet<UserRole>();
        }

        [PersonalData]
        public string FirstName { get; set; }

        [PersonalData]
        public string SirName { get; set; }

        [PersonalData]
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                var result =
                    $"{this.FirstName ?? string.Empty}{(string.IsNullOrWhiteSpace(this.SirName) ? string.Empty : " " + this.SirName)}{(string.IsNullOrWhiteSpace(this.LastName) ? string.Empty : " " + this.LastName)}";

                return string.IsNullOrEmpty(result?.Trim()) ? this.UserName : result;
            }
        }

        public string CultureId { get; set; }

        public string TabNumber { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public DateTime CreatedOn { get; set; }
        public bool PreserveCreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string CreatedFrom { get; set; }
        public string ModifiedFrom { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string DeletedFrom { get; set; }

    }
}
