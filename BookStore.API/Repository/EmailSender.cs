using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using BookStore.API.Models;
using Microsoft.Extensions.Logging;

namespace BookStore.API.Repository
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings EmailOptions;
        private readonly ILogger<EmailSender> logger;

        public EmailSender(IOptions<EmailSettings> options, ILogger<EmailSender> logger)
        {

            this.EmailOptions = options.Value;
            this.logger = logger;
        }

        public async Task sendemail(string email, string subject, string body, IList<IFormFile> attachments = null)
        {
            MimeMessage emails = new()
            {
                Sender = MailboxAddress.Parse(EmailOptions.Email),
                Subject = subject,

            };
            emails.To.Add(MailboxAddress.Parse(email));
            emails.From.Add(new MailboxAddress(EmailOptions.DisplayName, EmailOptions.Email));
            var smtp = new SmtpClient();
            smtp.Connect(EmailOptions.Host, EmailOptions.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(EmailOptions.Email, EmailOptions.Password);
            await smtp.SendAsync(emails);
            logger.LogTrace("Done send");
            smtp.Disconnect(true);
        }
    }
}
