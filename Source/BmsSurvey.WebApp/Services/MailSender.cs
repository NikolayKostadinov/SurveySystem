//  ------------------------------------------------------------------------------------------------
//   <copyright file="MailSender.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.WebApp.Services
{
    #region Using

    using System;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.Extensions.Configuration;

    #endregion

    public class MailSender : IEmailSender
    {
        private readonly IConfiguration configuration;

        public MailSender(IConfiguration configurationParam)
        {
            configuration = configurationParam ?? throw new ArgumentNullException(nameof(configurationParam));
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.Run(() => SendMail(email, subject, htmlMessage));
        }

        private void SendMail(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(configuration.GetSection("EmailSender:SnmpServer").Value);

            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(configuration.GetSection("EmailSender:From").Value);
            mailMessage.To.Add(email);
            mailMessage.Body = htmlMessage;
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = subject;
            client.Send(mailMessage);
        }
    }
}