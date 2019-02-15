//  ------------------------------------------------------------------------------------------------
//   <copyright file="AuditInfo.cs" company="Business Management System Ltd.">
//       Copyright "2018" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Domain.Abstract
{
    #region Using

    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces;

    #endregion

    public abstract class AuditInfo : IAuditInfo
    {
        public DateTime CreatedOn { get; set; }

        /// <summary>
        ///     Specifies whether or not the CreatedOn property should be automatically set.
        /// </summary>
        [NotMapped]
        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string CreatedFrom { get; set; }

        public string ModifiedFrom { get; set; }
    }
}