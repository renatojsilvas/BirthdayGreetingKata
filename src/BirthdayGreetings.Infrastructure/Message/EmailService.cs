using BirthdayGreetings.Domain;
using System.Net;
using System.Net.Mail;

namespace BirthdayGreetings.Infrastructure
{
    public class EmailService : IMessageService
    {
        private const string SmtpServer = "";
        private const string MailserverLogin = "";
        private const string MailServerPassword = "";
        private const string MailUserName = "";

        public async Task SendMessage(Message message)
        {            
            var client = new SmtpClient(SmtpServer)
            {
                Port = 587,
                Credentials = new NetworkCredential(MailserverLogin, MailServerPassword),
                EnableSsl = true,        
                UseDefaultCredentials = false
            };
            
            var from = new MailAddress(MailserverLogin, MailUserName, System.Text.Encoding.UTF8);            
            var to = new MailAddress(message.Recipient.EmailAddress.Address);
            
            var messageContent = new MailMessage(@from, to)
            {
                Body = message.Body,
                Subject = message.Subject,
            };

            await client.SendMailAsync(messageContent);
        }
    }
}
