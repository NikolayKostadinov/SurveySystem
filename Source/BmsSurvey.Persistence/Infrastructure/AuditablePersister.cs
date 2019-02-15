//  ------------------------------------------------------------------------------------------------
//   <copyright file="AuditablePersister.cs" company="Business Management System Ltd.">
//       Copyright "2018" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Persistence.Infrastructure
{
    #region Using

    using System.Linq;
    using Common.Abstract;
    using Domain.Entities.Utility;
    using Domain.Interfaces;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    #endregion

    public class AuditablePersister : IPersister
    {
        /// <summary>
        ///     Gets the added entities.
        /// </summary>
        /// <param name="data">The AuditableDbContext</param>
        /// <param name="userName">The name of the user hwo made change</param>
        /// <returns></returns>
        //private readonly IManyToManyRelationService manyToManyRelationServicel;

        //public AuditablePersister(IManyToManyRelationService manyToManyRelationServiceParam)
        //{
        //    manyToManyRelationServicel = manyToManyRelationServiceParam;
        //}
        public void PrepareSaveChanges(IAuditableDbContext data, string userName)
        {
            ApplyAuditInfoRules(data, userName);
            ApplyDeletableEntityRules(data, userName);
            //ManyToManyTraditional(data, userName);
        }

        //private void ManyToManyTraditional(IAuditableDbContext data, string userName)
        //{
        //    //todo: Fix ManyToMany standard case logging
        //    var result = data.DbContext.GetRelationships().ToList();
        //    if (result.Count() > 0)
        //    {
        //        var item1Type = ObjectContext.GetObjectType(result[0].Item1.GetType());
        //        var item2Type = ObjectContext.GetObjectType(result[0].Item2.GetType());
        //    }

        //    Debug.WriteLine("test");
        //}

        private void ApplyAuditInfoRules(IAuditableDbContext data, string userName)
        {
            // Prevents multiple enumerations
            var changedDbEntries = data.DbContext.ChangeTracker.Entries() as EntityEntry[]
                                   ?? data.DbContext.ChangeTracker.Entries().ToArray();
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in changedDbEntries.Where(e =>
                e.Entity is IAuditInfo &&
                (e.State == EntityState.Added
                 || e.State == EntityState.Modified)))
            {
                // Remove deleted entities
                if (entry.Entity.GetType().GetInterface(typeof(IDeletableEntity).FullName) != null
                    && ((IDeletableEntity) entry.Entity).IsDeleted)
                    continue;

                var entity = (IAuditInfo) entry.Entity;
                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = TimeProvider.Current.UtcNow;
                        entity.CreatedFrom = userName;
                    }
                }
                else
                {
                    GetChangedProperties(entry, userName, data.AuditLogRecords);
                    //manyToManyRelationServicel.GetChangedManyToManyRelations(changedDbEntries, entry,
                    //    data.AuditLogRecords, userName);
                    entity.ModifiedOn = TimeProvider.Current.UtcNow;
                    entity.ModifiedFrom = userName;
                }
            }
        }

        private void ApplyDeletableEntityRules(IAuditableDbContext data, string userName)
        {
            foreach (
                var entry in
                data.DbContext.ChangeTracker
                    .Entries()
                    .Where(e => e.Entity is IDeletableEntity && e.State == EntityState.Modified &&
                                ((IDeletableEntity) e.Entity).IsDeleted
                                || e.Entity is IDeletableEntity && e.State == EntityState.Deleted))
            {
                var entity = (IDeletableEntity) entry.Entity;
                entity.IsDeleted = true;
                entity.DeletedOn = TimeProvider.Current.UtcNow;
                entity.DeletedFrom = userName;
                entry.State = EntityState.Modified;
            }
        }

        private void GetChangedProperties(EntityEntry entry, string userName, DbSet<AuditLogRecord> auditRecords)
        {
            var entityName = entry.Metadata.Name;
            var newValues = entry.CurrentValues.Clone();
            var oldValues = entry.GetDatabaseValues();
            foreach (var propertyName in entry.CurrentValues.Properties.Select(x => x.Name))
            {
                var property = entry.Property(propertyName);
                var newValue = newValues.GetValue<object>(propertyName) ?? string.Empty;
                var oldValue = entry.State == EntityState.Modified
                    ? oldValues.GetValue<object>(propertyName) ?? string.Empty
                    : string.Empty;

                if (newValue != oldValue)
                    auditRecords.Add(new AuditLogRecord
                    {
                        TimeStamp = TimeProvider.Current.UtcNow,
                        EntityName = entityName,
                        EntityId = ((IEntity) entry.Entity).Id,
                        FieldName = propertyName,
                        OperationType = entry.State,
                        OldValue = oldValue.ToString(),
                        NewValue = newValue.ToString(),
                        UserName = userName
                    });
            }
        }
    }
}