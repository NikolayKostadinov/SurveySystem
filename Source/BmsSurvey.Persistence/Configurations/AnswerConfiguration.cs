using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Persistence.Configurations
{
    using Domain.Abstract;
    using Domain.Entities;
    using Domain.Entities.Answers;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasKey(a => a.Id);

            builder.ToTable("Answers")
                .HasDiscriminator<int>("AnswerType")
                .HasValue<Rate1to5StarsAnswer>(0)
                .HasValue<YesOrNoAnswer>(1)
                .HasValue<LowMidHighAnswer>(2);
        }
    }

    public class LowMidHighAnswerConfiguration : IEntityTypeConfiguration<LowMidHighAnswer>
    {
        public void Configure(EntityTypeBuilder<LowMidHighAnswer> builder)
        {
            builder.Property(a=>a.Value).HasColumnName("LowMidHighAnswer_Value");
        }
    }

    public class YesOrNoAnswerConfiguration : IEntityTypeConfiguration<YesOrNoAnswer>
    {
        public void Configure(EntityTypeBuilder<YesOrNoAnswer> builder)
        {
            builder.Property(a => a.Value).HasColumnName("YesOrNoAnswer_Value");
        }
    }

    public class Rate1to5StarsAnswerConfiguration : IEntityTypeConfiguration<Rate1to5StarsAnswer>
    {
        public void Configure(EntityTypeBuilder<Rate1to5StarsAnswer> builder)
        {
            builder.Property(a => a.Value).HasColumnName("Rate1to5StarsAnswer_Value");
        }
    }
}
