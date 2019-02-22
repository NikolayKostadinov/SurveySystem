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
    using Application.Interfaces;
    using Application.Notifications.Models;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.Extensions.Configuration;

    #endregion

    public class MailSender : IEmailSender, IMailNotificationService
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

            var mailMessage = new MailMessage
            {
                From = new MailAddress(configuration.GetSection("EmailSender:From").Value),
                Body = htmlMessage,
                IsBodyHtml = true,
                Subject = subject
            };

            mailMessage.To.Add(email);
            client.Send(mailMessage);
        }

        public Task SendAsync(Message message)
        {
            return this.SendEmailAsync(message.To, message.Subject, message.Body);
        }
    }
}