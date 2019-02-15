namespace BmsSurvey.Domain.Entities.Utility
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class AuditLogRecord: IEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public string EntityName { get; set; }

        public int EntityId { get; set; }

        public string FieldName{ get; set; }

        public EntityState OperationType { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }

        public string UserName { get; set; }

        public override string ToString() => $"{this.Id} | {this.TimeStamp} | {this.EntityName}| {this.EntityId} | {this.OperationType} | {this.FieldName} | {this.OldValue} | {this.NewValue} | {this.UserName}";
    }

}
