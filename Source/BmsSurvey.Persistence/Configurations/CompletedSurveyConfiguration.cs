using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Persistence.Configurations
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    class CompletedSurveyConfiguration : IEntityTypeConfiguration<CompletedSurvey>
    {
        public void Configure(EntityTypeBuilder<CompletedSurvey> builder)
        {
            builder.HasKey(cs => cs.Id);

            builder.Property(cs => cs.Id).UseSqlServerIdentityColumn();

            builder.Property(cs => cs.IpAddress)
                .IsRequired();

            builder.HasOne(cs => cs.Survey)
                .WithMany(s => s.CompletedSurveys)
                .HasForeignKey(cs => cs.SurveyId);
            builder.HasIndex(cs => new {cs.SurveyId, cs.IpAddress})
                .IsUnique(true);
        }
    }
}
