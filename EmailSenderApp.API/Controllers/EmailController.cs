using EmailSenderApp.Application.Services.EmailServces;
using EmailSenderApp.Domain.Entites.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmailSenderApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] EmailModel model)
        {

            await _emailService.SendEmailAsync(model);

            return Ok("Muvoffaqiyatli email yuborildi");
        }

        [HttpGet]
        public async Task<IActionResult> GetMail()
        {

            return Ok("Muvoffaqiyatli email yuborildi");
        }


    }
}
