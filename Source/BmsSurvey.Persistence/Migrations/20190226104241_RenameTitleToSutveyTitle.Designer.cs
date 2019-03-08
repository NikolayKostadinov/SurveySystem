﻿// <auto-generated />
using System;
using BmsSurvey.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BmsSurvey.Persistence.Migrations
{
    [DbContext(typeof(BmsSurveyDbContext))]
    [Migration("20190226104241_RenameTitleToSutveyTitle")]
    partial class RenameTitleToSutveyTitle
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BmsSurvey.Domain.Abstract.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnswerType");

                    b.Property<string>("CreatedFrom");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("DeletedFrom");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("ModifiedFrom");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<int>("QuestionId");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");

                    b.HasDiscriminator<int>("AnswerType");
                });

            modelBuilder.Entity("BmsSurvey.Domain.Entities.CompletedSurvey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedFrom");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("DeletedFrom");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Email");

                    b.Property<string>("IpAddress")
                        .IsRequired();

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("ModifiedFrom");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<int>("SurveyId");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId", "IpAddress")
                        .IsUnique();

                    b.ToTable("CompletedSurveys");
                });

            modelBuilder.Entity("BmsSurvey.Domain.Entities.Identity.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("CreatedFrom");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("DeletedFrom");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("ModifiedFrom");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedName");

                    b.Property<bool>("PreserveCreatedOn");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("BmsSurvey.Domain.Entities.Identity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("CreatedFrom");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("CultureId");

                    b.Property<string>("DeletedFrom");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("ModifiedFrom");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<bool>("PreserveCreatedOn");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("SirName");

                    b.Property<string>("TabNumber");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BmsSurvey.Domain.Entities.Identity.UserRole", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("BmsSurvey.Domain.Entities.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedFrom");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("DeletedFrom");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<int>("DisplayNumber");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("ModifiedFrom");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<int>("QuestionType");

                    b.Property<int>("SurveyId");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("BmsSurvey.Domain.Entities.Survey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ActiveFrom");

                    b.Property<DateTime>("ActiveTo");

                    b.Property<string>("CreatedFrom");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("DeletedFrom");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("ModifiedFrom");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<int>("PageSize")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(3);

                    b.Property<string>("SurveyTitle")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("BmsSurvey.Domain.Entities.Utility.AuditLogRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EntityId");

                    b.Property<string>("EntityName");

                    b.Property<string>("FieldName");

                    b.Property<string>("NewValue");

                    b.Property<string>("OldValue");

                    b.Property<int>("OperationType");

                    b.Property<DateTime>("TimeStamp");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("AuditLogRecords");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("BmsSurvey.Domain.Entities.Answers.FreeTextAnswer", b =>
                {
                    b.HasBaseType("BmsSurvey.Domain.Abstract.Answer");

                    b.Property<string>("Value")
                        .HasColumnName("FreeTextAnswerAnswer_Value");

                    b.HasDiscriminator().HasValue(3);
                });

            modelBuilder.Entity("BmsSurvey.Domain.Entities.Answers.LowMidHighAnswer", b =>
                {
                    b.HasBaseType("BmsSurvey.Domain.Abstract.Answer");

                    b.Property<int>("Value")
                        .HasColumnName("LowMidHighAnswer_Value");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("BmsSurvey.Domain.Entities.Answers.Rate1To5StarsAnswer", b =>
                {
                    b.HasBaseType("BmsSurvey.Domain.Abstract.Answer");

                    b.Property<int>("Value")
                        .HasColumnName("Rate1to5StarsAnswer_Value");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("BmsSurvey.Domain.Entities.Answers.YesOrNoAnswer", b =>
                {
                    b.HasBaseType("BmsSurvey.Domain.Abstract.Answer");

                    b.Property<bool>("Value")
                        .HasColumnName("YesOrNoAnswer_Value");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("BmsSurvey.Domain.Abstract.Answer", b =>
                {
                    b.HasOne("BmsSurvey.Domain.Entities.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .HasConstraintName("FK_Question_Answer")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("BmsSurvey.Domain.Entities.CompletedSurvey", b =>
                {
                    b.HasOne("BmsSurvey.Domain.Entities.Survey", "Survey")
                        .WithMany("CompletedSurveys")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BmsSurvey.Domain.Entities.Identity.UserRole", b =>
                {
                    b.HasOne("BmsSurvey.Domain.Entities.Identity.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BmsSurvey.Domain.Entities.Identity.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("BmsSurvey.Domain.Entities.Question", b =>
                {
                    b.HasOne("BmsSurvey.Domain.Entities.Survey", "Survey")
                        .WithMany("Questions")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}