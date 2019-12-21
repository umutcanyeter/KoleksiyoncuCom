using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoleksiyoncuCom.WebUi.EmailServices
{
    public class EmailSender : IEmailSender
    {
        private const string SendGridKey = "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";
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(SendGridKey, subject, htmlMessage, email);
        }

        private Task Execute(string sendGridKey, string subject, string message, string email)
        {
            var client = new SendGridClient(sendGridKey);

            var msg = new SendGridMessage()
            {
                From = new EmailAddress("info@koleksiyoncu.com", "Koleksiyoncu.com.tr"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };

            msg.AddTo(new EmailAddress(email));
            return client.SendEmailAsync(msg);
        }
    }
}
