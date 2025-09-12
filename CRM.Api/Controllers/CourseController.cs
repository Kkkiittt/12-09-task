using CRM.Application.Interfaces.Services;
using CRM.Domain.Entities;

using Microsoft.AspNetCore.Mvc;

namespace CRM.Api.Controllers;
[ApiController]
[Route("/courses")]
public class CourseController : ControllerBase
{
	private readonly ICourseService _courseService;

	public CourseController(ICourseService courseService)
	{
		_courseService = courseService;
	}

	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var courses = await _courseService.GetAllAsync();
		return courses != null ? Ok(courses) : NotFound();
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> Get(int id)
	{
		var course = await _courseService.GetAsync(id);
		return course != null ? Ok(course) : NotFound();
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] Course entity)
	{
		var result = await _courseService.CreateAsync(entity);
		return result ? Ok("Course created successfully") : StatusCode(500, "Failed to create course");
	}
}