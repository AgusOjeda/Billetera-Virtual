using CV.Authentication.Application.Interfaces;
using CV.Authentication.Domain.Common;
using Microsoft.Extensions.Options;
using MimeKit;
using RestSharp;
using System.Net.Mail;

namespace CV.Authentication.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfig _appSettings;
        
        public EmailService(IOptions<EmailConfig> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        
        public bool SendEmail(string to, string subject, string body)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(_appSettings.EMAIL);
            message.To.Add(new MailAddress(to));
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            var smptClient = new SmtpClient("smtp.gmail.com") {
                Port = 587,
                Credentials = new System.Net.NetworkCredential(_appSettings.EMAIL, _appSettings.PASSWORD),
                EnableSsl = true
            };
            smptClient.Send(message);

            return true;

        }
    }
}