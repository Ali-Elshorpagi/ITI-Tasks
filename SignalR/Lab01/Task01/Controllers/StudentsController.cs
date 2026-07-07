using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Task01.Hubs;
using Task01.Models;
using Task01.Services.Abstracts;

namespace Task01.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController(IStudentService studentService, IHubContext<StudentHub> hub) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await studentService.GetAllStudentsAsync();
            return Ok(students);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await studentService.GetStudentByIdAsync(id);
            if (student is null)
                return NotFound();
            return Ok(student);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Student student)
        {
            var result = await studentService.AddStudentAsync(student);
            if (result != "Student Added Successfully")
                return BadRequest(result);

            await hub.Clients.All.SendAsync("StudentAdded", new
            {
                student.Id,
                student.Name,
                student.Age,
                student.DepartmentId
            });

            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Student student)
        {
            if (id != student.Id)
                return BadRequest();

            var success = await studentService.UpdateStudentAsync(student);
            if (!success)
                return Conflict("A student with the same name already exists in this department.");

            await hub.Clients.All.SendAsync("StudentUpdated", new
            {
                student.Id,
                student.Name,
                student.Age,
                student.DepartmentId
            });

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await studentService.DeleteStudentAsync(id);

            await hub.Clients.All.SendAsync("StudentDeleted", id);

            return NoContent();
        }
    }
}
