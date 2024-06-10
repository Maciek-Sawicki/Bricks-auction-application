using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;

namespace Bricks_auction_application
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpConfiguration _smtpConfiguration;

        public EmailSender(IConfiguration configuration)
        {
            _smtpConfiguration = configuration.GetSection("Smtp").Get<SmtpConfiguration>();
        }

        public async Task SendEmailAsync(string email, string subject, string message, bool isHtml = false)
        {
            var client = new SmtpClient(_smtpConfiguration.Server, _smtpConfiguration.Port)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(_smtpConfiguration.User, _smtpConfiguration.Password)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpConfiguration.User),
                Subject = subject,
                Body = message
            };

            if (isHtml)
            {
                mailMessage.IsBodyHtml = true;
            }

            mailMessage.To.Add(email);

            await client.SendMailAsync(mailMessage);
        }
    }
}
