using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Task01.DTOs.Department;
using Task01.Models;
using Task01.Repositories.Interfaces;

namespace Task01.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;
    public DepartmentsController(IDepartmentRepository departmentRepository, IMapper mapper)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DepartmentReadDto>>> GetAll()
    {
        var departments = await _departmentRepository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<DepartmentReadDto>>(departments));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DepartmentReadDto>> GetById(int id)
    {
        var department = await _departmentRepository.GetByIdAsync(id);

        if (department is null)
            return NotFound();

        return Ok(_mapper.Map<DepartmentReadDto>(department));
    }

    [HttpPost]
    public async Task<ActionResult<DepartmentReadDto>> Create(DepartmentUpsertDto dto)
    {
        var department = _mapper.Map<Department>(dto);

        await _departmentRepository.AddAsync(department);

        var result = _mapper.Map<DepartmentReadDto>(department);
        return CreatedAtAction(nameof(GetById), new { id = department.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, DepartmentUpsertDto dto)
    {
        var department = await _departmentRepository.GetByIdAsync(id);
        if (department is null)
            return NotFound();

        _mapper.Map(dto, department);
        await _departmentRepository.UpdateAsync(department);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var department = await _departmentRepository.GetByIdAsync(id);

        if (department is null)
            return NotFound();

        if (await _departmentRepository.HasStudentsAsync(id))
            return BadRequest("Cannot delete department that has students.");

        await _departmentRepository.DeleteAsync(department);

        return NoContent();
    }
}

