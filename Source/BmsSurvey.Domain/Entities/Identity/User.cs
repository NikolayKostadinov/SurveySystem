//  ------------------------------------------------------------------------------------------------
//   <copyright file="User.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Domain.Entities.Identity
{
    #region Using

    using System;
    using System.Collections.Generic;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;

    #endregion

    public class User : IdentityUser<int>, IAuditInfo, IDeletableEntity
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }

        [PersonalData] public string FirstName { get; set; }

        [PersonalData] public string SirName { get; set; }

        [PersonalData] public string LastName { get; set; }

        public string FullName
        {
            get
            {
                var result =
                    $"{FirstName ?? string.Empty}{(string.IsNullOrWhiteSpace(SirName) ? string.Empty : " " + SirName)}{(string.IsNullOrWhiteSpace(LastName) ? string.Empty : " " + LastName)}";

                return string.IsNullOrEmpty(result?.Trim()) ? UserName : result;
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