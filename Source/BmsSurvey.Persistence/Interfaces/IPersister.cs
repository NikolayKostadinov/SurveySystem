//  ------------------------------------------------------------------------------------------------
//   <copyright file="IPersister.cs" company="Business Management System Ltd.">
//       Copyright "2018" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Persistence.Interfaces
{
    #region Using

    #endregion

    public interface IPersister
    {
        void PrepareSaveChanges(IAuditableDbContext data, string userName);
    }
}