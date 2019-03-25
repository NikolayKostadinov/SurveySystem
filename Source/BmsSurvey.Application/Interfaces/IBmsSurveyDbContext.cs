//  ------------------------------------------------------------------------------------------------
//   <copyright file="IBmsSurveyDbContext.cs" company="Business Management System Ltd.">
//       Copyright "" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------
namespace BmsSurvey.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Abstract;
    using Domain.Entities;
    using Domain.Entities.Utility;
    using Domain.Infrastructure;
    using Microsoft.EntityFrameworkCore;

    public interface IBmsSurveyDbContext
    {
        // Entities
        DbSet<Survey> Surveys { get; set; }
        DbSet<Question> Questions { get; set; }
        DbSet<Answer> Answers { get; set; }
        DbSet<CompletedSurvey> CompletedSurveys { get; set; }
        DbSet<AuditLogRecord> AuditLogRecords { get; set; }

        DbContext DbContext { get; }


        // Customized methods
        int SaveChanges(string userName);
        int SaveChanges();
        Task<int> SaveChangesAsync(string userName, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
        void BulkInsert<T>(IEnumerable<T> entities) where T : class;
    }
}