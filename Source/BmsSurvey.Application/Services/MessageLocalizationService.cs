//  ------------------------------------------------------------------------------------------------
//   <copyright file="MessageLocalizationService.cs" company="Business Management System Ltd.">
//       Copyright "2018" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Services
{
    #region Using

    using System.Reflection;
    using Common.Interfaces;
    using Microsoft.Extensions.Localization;
    using Resources;

    #endregion

    public class MessageLocalizationService: ILocalizationService<MessageResource>
    {
        private readonly IStringLocalizer localizer;

        public MessageLocalizationService(IStringLocalizerFactory factory)
        {
            var type = typeof(MessageResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            localizer = factory.Create("MessageResource", assemblyName.Name);
        }

        public LocalizedString GetLocalizedHtmlString(string key)
        {
            return localizer[key];
        }

        public LocalizedString GetLocalizedHtmlString(string key, string parameter)
        {
            return localizer[key, parameter];
        }
    }
}