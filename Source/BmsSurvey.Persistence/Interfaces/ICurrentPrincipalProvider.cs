//  ------------------------------------------------------------------------------------------------
//   <copyright file="ICurrentPrincipalProvider.cs" company="Business Management System Ltd.">
//       Copyright "" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------
namespace BmsSurvey.Persistence.Interfaces
{
    using System.Security.Principal;

    public interface ICurrentPrincipalProvider
    {
        IPrincipal GetCurrentPrincipal();
    }
}