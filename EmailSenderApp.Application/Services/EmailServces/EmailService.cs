using EmailSenderApp.Domain.Entites.Models;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace EmailSenderApp.Application.Services.EmailServces
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(EmailModel model)
        {
            string path = @"D:\SelfStudy\frontend\versitka\planeta\sender.html";

            using (var stream = new StreamReader(path))
            {
                model.Body = await stream.ReadToEndAsync();
            }

            var emailSettings = _config.GetSection("EmailSettings");
            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailSettings["Sender"], emailSettings["SenderName"]),
                Subject = model.Subject,
                Body = model.Body,
                IsBodyHtml = true,

            };
            mailMessage.To.Add(model.To);

            using var smtpClient = new SmtpClient(emailSettings["MailServer"], int.Parse(emailSettings["MailPort"]))
            {
                Port = Convert.ToInt32(emailSettings["MailPort"]),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(emailSettings["Sender"], emailSettings["Password"]),
                EnableSsl = true,
            };

            //smtpClient.UseDefaultCredentials = true;

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
