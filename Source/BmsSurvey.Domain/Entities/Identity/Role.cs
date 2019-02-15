namespace BmsSurvey.Domain.Entities.Identity
{
    using System;
    using System.Collections.Generic;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;

    public class Role: IdentityRole<int>, IAuditInfo, IDeletableEntity
    {
        public Role() : base()
        {
            this.UserRoles = new HashSet<UserRole>();
        }

        public Role(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        public string Description { get; set; }

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
