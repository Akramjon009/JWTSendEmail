using EmailSenderApp.Application.Services.AuthServices;
using EmailSenderApp.Domain.Entites.Models.AuthModels;
using Microsoft.AspNetCore.Mvc;

namespace EmailSenderApp.API.Controllers.AuthController
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IAuthService _authService;

        public IdentityController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var result = _authService.GenerateToken(user);

            return Ok(result);
        }

    }
}
