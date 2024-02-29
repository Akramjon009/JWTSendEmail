using EmailSenderApp.Domain.Entites.Models.AuthModels;

namespace EmailSenderApp.Application.Services.AuthServices
{
    public interface IAuthService
    {
        public Task<string> GenerateToken(User user);
    }
}
