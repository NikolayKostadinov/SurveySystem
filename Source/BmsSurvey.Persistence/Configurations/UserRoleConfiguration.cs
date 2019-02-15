using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Persistence.Configurations
{
    using Domain.Entities.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.Ignore(ur => ur.Id);
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });
        }
    }
}
