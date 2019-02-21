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
                .HasValue<Rate1To5StarsAnswer>(0)
                .HasValue<YesOrNoAnswer>(1)
                .HasValue<LowMidHighAnswer>(2)
                .HasValue<FreeTextAnswer>(3);
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

    public class Rate1Тo5StarsAnswerConfiguration : IEntityTypeConfiguration<Rate1To5StarsAnswer>
    {
        public void Configure(EntityTypeBuilder<Rate1To5StarsAnswer> builder)
        {
            builder.Property(a => a.Value).HasColumnName("Rate1to5StarsAnswer_Value");
        }
    }

    public class FreeTextAnswerConfiguration : IEntityTypeConfiguration<FreeTextAnswer>
    {
        public void Configure(EntityTypeBuilder<FreeTextAnswer> builder)
        {
            builder.Property(a => a.Value).HasColumnName("FreeTextAnswerAnswer_Value");
        }
    }
}
