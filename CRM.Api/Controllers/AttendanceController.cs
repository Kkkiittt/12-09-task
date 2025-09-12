using CRM.Application.Interfaces.Services;
using CRM.Domain.Entities;

using Microsoft.AspNetCore.Mvc;

namespace CRM.Api.Controllers;

[ApiController]
[Route("attendances")]
public class AttendanceController : ControllerBase
{
	private readonly IAttendanceService _attendanceService;

	public AttendanceController(IAttendanceService attendanceService)
	{
		_attendanceService = attendanceService;
	}

	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var attendances = await _attendanceService.GetAllAsync();
		return attendances != null ? Ok(attendances) : NotFound();
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> Get(int id)
	{
		var attendance = await _attendanceService.GetAsync(id);
		return attendance != null ? Ok(attendance) : NotFound();
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] Attendance entity)
	{
		var result = await _attendanceService.CreateAsync(entity);
		return result ? Ok("Attendance created successfully") : StatusCode(500, "Failed to create attendance");
	}

	[HttpGet("missing/{date}")]
	public async Task<IActionResult> GetMissing(DateOnly date)
	{
		var missingStudents = await _attendanceService.GetMissingAsync(date);
		return missingStudents != null ? Ok(missingStudents) : NotFound();
	}
}