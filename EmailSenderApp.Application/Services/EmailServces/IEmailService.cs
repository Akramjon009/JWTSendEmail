using EmailSenderApp.Domain.Entites.Models;

namespace EmailSenderApp.Application.Services.EmailServces
{
    public interface IEmailService
    {
        public Task SendEmailAsync(EmailModel model);
    }
}
