//  ------------------------------------------------------------------------------------------------
//   <copyright file="BmsSurveyDbContext.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Persistence
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Extensions;
    using Common.Interfaces;
    using Domain.Abstract;
    using Domain.Entities;
    using Domain.Entities.Identity;
    using Domain.Entities.Utility;
    using Domain.Interfaces;
    using Infrastructure;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    #endregion

    public class BmsSurveyDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>, IAuditableDbContext
    {

        private readonly IPersister persister;
        private readonly ICurrentPrincipalProvider principalProvider;
        public BmsSurveyDbContext(DbContextOptions<BmsSurveyDbContext> options, IPersister persister, ICurrentPrincipalProvider principalProviderParam)
            : base(options)
        {
            this.persister = persister ?? throw new ArgumentNullException(nameof(persister));
            this.principalProvider = principalProviderParam ?? throw new ArgumentNullException(nameof(principalProviderParam));
        }



        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<EmployeeTerritory> EmployeeTerritories { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Region> Region { get; set; }

        public DbSet<Shipper> Shippers { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Territory> Territories { get; set; }

        public DbSet<AuditLogRecord> AuditLogRecords { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BmsSurveyDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies();
            }
        }

        public DbContext DbContext => this;
        public override int SaveChanges()
        {
            var currentPrincipal = this.principalProvider.GetCurrentPrincipal();
            var userName = currentPrincipal?.Identity.Name??"System Change";
            persister.PrepareSaveChanges(this, userName);
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.Factory.StartNew(() => this.SaveChanges());
        }

        public IEfStatus SaveChangesWithValidation()
        {
            var result = new EfStatus();
            try
            {
                result.ResultRecordsCount = SaveChanges(); //then update it
            }
            catch (ValidationException ex)
            {
                //this.logger.Error(ex.Message, this, ex);
                return result.SetEfErrors(new List<ValidationResult> { ex.ValidationResult });
            }
            catch (DbUpdateException ex)
            {
                //this.logger.Error(ex.Message, this, ex);
                var sqlException = ex.InnerException?.InnerException as SqlException;

                if (sqlException != null && (sqlException.Number == 2601 || sqlException.Number == 2627))
                    return result.SetEfErrors(new List<ValidationResult>
                    {
                        new ValidationResult("UnicConstraintViolation")
                    });
                return result.SetEfErrors(new List<ValidationResult> { new ValidationResult("SavingDataError") });
            }
            //else it isn't an exception we understand so it throws in the normal way

            return result;
        }

        public void BulkInsert<T>(IEnumerable<T> entities) where T : class
        {
            var userName = Thread.CurrentPrincipal.Identity.Name ?? "System Change";

            if (entities.FirstOrDefault() is IAuditInfo)
                entities.ForEach(x =>
                {
                    ((IAuditInfo)x).CreatedFrom = userName;
                    ((IAuditInfo)x).CreatedOn = DateTime.Now;
                });

            this.BulkInsert(entities);
        }

    }
}