//  ------------------------------------------------------------------------------------------------
//   <copyright file="IDeletableEntity.cs" company="Business Management System Ltd.">
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

    public interface IDeletableEntity : IEntity
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOn { get; set; }

        string DeletedFrom { get; set; }
    }
}