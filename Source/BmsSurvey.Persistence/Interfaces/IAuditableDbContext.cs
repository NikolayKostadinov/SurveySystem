//  ------------------------------------------------------------------------------------------------
//   <copyright file="IAuditableDbContext.cs" company="Business Management System Ltd.">
//       Copyright "2018" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Persistence.Interfaces
{
    #region Using

    using Domain.Entities.Utility;
    using Microsoft.EntityFrameworkCore;

    #endregion

    /// <summary>
    /// </summary>
    public interface IAuditableDbContext
    {
        DbSet<AuditLogRecord> AuditLogRecords { get; set; }
        DbContext DbContext { get; }
    }
}