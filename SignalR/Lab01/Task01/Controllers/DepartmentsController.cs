using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Task01.Hubs;
using Task01.Models;
using Task01.Services.Abstracts;

namespace Task01.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController(IDepartmentService departmentService, IHubContext<StudentHub> hub) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments = await departmentService.GetAllDepartmentsAsync();
            return Ok(departments);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var department = await departmentService.GetDepartmentByIdAsync(id);
            if (department is null)
                return NotFound();
            return Ok(department);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Department department)
        {
            var success = await departmentService.AddDepartmentAsync(department);
            if (!success)
                return Conflict("A department with the same name already exists.");

            await hub.Clients.All.SendAsync("DepartmentAdded", new
            {
                department.Id,
                department.Name,
                department.Description
            });

            return CreatedAtAction(nameof(GetById), new { id = department.Id }, department);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Department department)
        {
            if (id != department.Id)
                return BadRequest();

            var success = await departmentService.EditDepartmentAsync(department);
            if (!success)
                return Conflict("No changes detected.");

            await hub.Clients.All.SendAsync("DepartmentUpdated", new
            {
                department.Id,
                department.Name,
                department.Description
            });

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await departmentService.DeleteDepartmentAsync(id);

            await hub.Clients.All.SendAsync("DepartmentDeleted", id);

            return NoContent();
        }
    }
}
