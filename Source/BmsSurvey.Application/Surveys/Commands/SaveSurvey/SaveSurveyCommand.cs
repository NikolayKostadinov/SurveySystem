//  ------------------------------------------------------------------------------------------------
//   <copyright file="SaveSurveyCommand.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Surveys.Commands.SaveSurvey
{
    #region Using

    using Common.Interfaces;
    using MediatR;

    #endregion

    public class SaveSurveyCommand : IRequest
    {
        private readonly int id;
        private readonly string userName;
        private readonly string eMail;
        private readonly string ipAddress;

        public SaveSurveyCommand(int id, string userName, string eMail, string ipAddress)
        {
            this.id = id;
            this.userName = userName;
            this.eMail = eMail;
            this.ipAddress = ipAddress;
        }

        public int Id => this.id;
        public string UserName => this.userName;

        public string EMail => this.eMail;

        public string IpAddress => this.ipAddress;
    }
}