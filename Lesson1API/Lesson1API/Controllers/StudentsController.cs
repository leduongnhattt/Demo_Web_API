using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lesson1API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        //Get
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] name = new string[] { "John", "Jane", "Mark", "Emily", "David" };
            return Ok(name);
        }
    }
}
