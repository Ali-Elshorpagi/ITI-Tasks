using AutoMapper;
using Lab03.DTOs;
using Lab03.Models;
using Lab03.UoW;
using Microsoft.AspNetCore.Mvc;

namespace Lab03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly UnitOFWork context;
        private readonly IMapper map;
        public DepartmentController(UnitOFWork context, IMapper map)
        {
            this.context = context;
            this.map = map;
        }
        [EndpointSummary("select all Departments")]
        [EndpointDescription("read all departments from database  ex:/api/departments")]
        [Produces("text/xml")]
        [ProducesResponseType(200, Type = typeof(DepartmentDTO))]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]
        [ProducesErrorResponseType(typeof(void))]
        [HttpGet]
        public IActionResult Get()
        {
            var depts = context.DeptReps.IncludeStudentsWithDepartments();
            return Ok(map.Map<List<DepartmentDTO>>(depts));
        }
        [HttpGet("{id}")]
        [EndpointSummary("select a specific department")]
        [EndpointDescription("select a specific department from database  ex:/api/departments?id={}")]
        public IActionResult GetByID(int id)
        {
            var dept = context.DeptReps.GetDepartmentWithStudents(id);
            if (dept is null)
                return NotFound();

            var res = map.Map<DepartmentDTO>(dept);
            return Ok(res);

        }
        [HttpPost]
        public IActionResult Post(Department department)
        {
            if (!context.DeptReps.CanUseId(department.DeptId))
                return Conflict("Department with the same id already exists.");

            context.DeptReps.Add(department);
            context.save();

            var created = context.DeptReps.GetDepartmentWithStudents(department.DeptId);

            return CreatedAtAction(nameof(GetByID), new { id = department.DeptId }, map.Map<DepartmentDTO>(created));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Department department)
        {
            if (id != department.DeptId)
                return BadRequest();

            var existing = context.DeptReps.FindById(id);
            if (existing is null)
                return NotFound();

            context.DeptReps.Edit(department);
            context.save();

            var updated = context.DeptReps.GetDepartmentWithStudents(id);
            return Ok(map.Map<DepartmentDTO>(updated));
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var res = context.DeptReps.delete(id);
            if (res == true)
            {
                context.save();
                return NoContent();
            }
            return NotFound();
        }
    }
}
