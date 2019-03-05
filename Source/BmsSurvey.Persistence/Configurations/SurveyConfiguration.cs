using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Persistence.Configurations
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    class SurveyConfiguration : IEntityTypeConfiguration<Survey>
    {
        public void Configure(EntityTypeBuilder<Survey> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id).UseSqlServerIdentityColumn();

            builder.Property(s => s.SurveyTitle)
                .IsRequired();

            builder.Property(s => s.PageSize)
                .HasDefaultValue(3)
                .IsRequired();

            builder.Ignore(s => s.IsActive);

            builder.HasMany(s => s.Questions)
                .WithOne(q => q.Survey)
                .HasForeignKey(q => q.SurveyId)
                .IsRequired();
        }
    }
}
