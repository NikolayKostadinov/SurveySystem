//  ------------------------------------------------------------------------------------------------
//   <copyright file="AutoMapperFactory.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Tests.Infrastructure
{
    #region Using

    using Application.Infrastructure.AutoMapper;
    using AutoMapper;

    #endregion

    public static class AutoMapperFactory
    {
        public static IMapper Create()
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new AutoMapperProfile()); });

            return mappingConfig.CreateMapper();
        }
    }
}