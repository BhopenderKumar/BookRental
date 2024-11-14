using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace BookRental.Infrastructure.Email
{
    public class EmailHelper
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _senderEmail;
        private readonly string _senderPassword;

        public EmailHelper(IConfiguration configuration)
        {
            _smtpServer = configuration["EmailSettings:SmtpServer"];
            _smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"]);
            _senderEmail = configuration["EmailSettings:SenderEmail"];
            _senderPassword = configuration["EmailSettings:SenderPassword"];
        }
        public async Task SendEmailAsync(string recipientEmail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Sender Name", _senderEmail)); // You can add sender name
            message.To.Add(new MailboxAddress("Recipient Name", recipientEmail)); // Recipient email
            message.Subject = subject;

            var bodyPart = new TextPart("plain")
            {
                Text = body
            };

            message.Body = bodyPart;

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpServer, _smtpPort, false); // Use SSL (true) if using port 465
                await client.AuthenticateAsync(_senderEmail, _senderPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
