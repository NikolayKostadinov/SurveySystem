//  ------------------------------------------------------------------------------------------------
//   <copyright file="IDbContext.cs" company="Business Management System Ltd.">
//       Copyright "2018" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Persistence.Interfaces
{
    #region Using

    using System;
    using System.Collections.Generic;
    using Common.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    #endregion

    public interface IDbContext : IDisposable
    {
        DbContext DbContext { get; }

        //string UserName { get; set; }

        int SaveChanges();

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        DbSet<T> Set<T>() where T : class;

        IEfStatus SaveChangesWithValidation();

        void BulkInsert<T>(IEnumerable<T> entities) where T : class;
    }
}