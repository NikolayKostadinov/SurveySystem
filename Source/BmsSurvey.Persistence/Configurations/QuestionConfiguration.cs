using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Persistence.Configurations
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class QuestionConfiguration: IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(q => new { q.Id});

            builder.ToTable("Questions");

            builder.Property(q => q.DisplayNumber).IsRequired();

            builder.Property(q => q.Text).IsRequired();

            builder.HasMany(q => q.Answers)
                .WithOne(a=>a.Question)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Question_Answer");
        }


    }
}
