using System.Net;
using System.Net.Mail;

namespace Bricks_auction_application
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message) 
        {
            var mail = "klocxpowiadomienia@outlook.com";
            var pw = "sGw&)*Q8-DMQ9rk";

            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)
            };

            return client.SendMailAsync(
                new MailMessage(from: mail,
                                to: email,
                                subject,
                                message
                                ));
        }
    }
}
