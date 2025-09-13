using CRM.Application.Interfaces.Services;
using CRM.Domain.Entities;

using Microsoft.AspNetCore.Mvc;

namespace CRM.Api.Controllers;
[ApiController]
[Route("/students")]
public class StudentController : ControllerBase
{
	private readonly IStudentService _studentService;

	public StudentController(IStudentService studentService)
	{
		_studentService = studentService;
	}

	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var students = await _studentService.GetAllAsync();
		return students != null ? Ok(students) : NotFound();
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> Get(int id)
	{
		var student = await _studentService.GetAsync(id);
		return student != null ? Ok(student) : NotFound();
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] Student entity)
	{
		var result = await _studentService.CreateAsync(entity);
		return result ? Ok("Student created successfully") : StatusCode(500, "Failed to create student");
	}

	[HttpGet("detailed")]
	public async Task<IActionResult> GetStudentsDetailed()
	{
		var students = await _studentService.GetStudentsDetailedAsync();
		return students != null ? Ok(students) : NotFound();
	}

	[HttpGet("search")]
	public async Task<IActionResult> Search(string? name, int courseId, double paymentSum)
	{
		var students = await _studentService.SearchAsync(name ?? "", courseId, paymentSum);
		return students != null ? Ok(students) : NotFound();
	}

	[HttpGet("qr/{id}")]
	public IActionResult GenerateQrCode(int id)
	{
		return File(_studentService.GenerateQrCode(id), "image/png");
	}

	[HttpGet("detailed/{id}")]
	public async Task<IActionResult> GetDetailedAsync(int id)
	{
		var studName = (await _studentService.GetAsync(id)).Name;
		var student = (await _studentService.GetStudentsDetailedAsync()).Where(x => x.StudentName == studName).FirstOrDefault();
		return student != null ? Ok(student) : NotFound();
	}
}