using EmailSenderApp.API.Attributes;
using EmailSenderApp.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmailSenderApp.API.Controllers.AuthController
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        List<string> users = new List<string>()
        {
            "Student 1", "" +
            "Student 2","Student 3","Student 4","Student 5","Student 6"
        };

        List<string> teachers = new List<string>()
        {
            "Teacher 1", "Teacher 2","Teacher 3","Teacher 4","Teacher 5","Teacher 6"
        };

        [HttpGet]
        [IdentityFilter(Permission.GetAllStudents)]
        public IActionResult GetStudents()
        {
            return Ok(users);
        }

        [HttpGet]
        [IdentityFilter(Permission.GetAllTeachers)]
        public IActionResult GetTeachers()
        {
            return Ok(teachers);
        }

        [HttpPost]
        [IdentityFilter(Permission.CreateStudent)]
        public IActionResult CreateStudent()
        {
            return Ok("Create bo'ldi");
        }

        [HttpDelete]
        [IdentityFilter(Permission.DeleteStudent)]
        public IActionResult DeleteStudent()
        {
            return Ok("Delete bo'ldi");
        }
    }
}
