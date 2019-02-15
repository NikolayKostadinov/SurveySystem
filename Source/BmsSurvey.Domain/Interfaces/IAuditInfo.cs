//  ------------------------------------------------------------------------------------------------
//   <copyright file="IAuditInfo.cs" company="Business Management System Ltd.">
//       Copyright "2018" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Domain.Interfaces
{
    #region Using

    using System;

    #endregion

    public interface IAuditInfo
    {
        DateTime CreatedOn { get; set; }

        bool PreserveCreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }

        string CreatedFrom { get; set; }

        string ModifiedFrom { get; set; }
    }
}