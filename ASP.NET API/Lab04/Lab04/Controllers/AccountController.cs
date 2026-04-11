using Lab04.DTOs;
using Lab04.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Lab04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccounttRepository _studentRepository;
        public AccountController(IAccounttRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        [HttpPost("register")]
        public async Task<ActionResult> Register(AddStudentDTO stdto)
        {
            if (stdto is null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _studentRepository.RegisterAsync(stdto);
            if (result.Succeeded)
                return Created();

            return BadRequest(result.Errors);
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDataDTo data)
        {
            if (data is null)
                return BadRequest();

            var token = await _studentRepository.LoginAsync(data);
            if (token is null)
                return Unauthorized();

            return Ok(token);
        }
    }
}
