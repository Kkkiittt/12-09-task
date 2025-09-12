using CRM.Application.Interfaces.Services;

using Microsoft.AspNetCore.Mvc;

namespace CRM.Api.Controllers;

[ApiController]
[Route("student-groups")]
public class StudentGroupController : ControllerBase
{
	private readonly IStudentGroupService _serv;

	public StudentGroupController(IStudentGroupService serv)
	{
		_serv = serv;
	}

	[HttpPost("{groupId}/{studId}")]
	public async Task<IActionResult> Create(int groupId, int studId)
	{
		var result = await _serv.AddAsync(groupId, studId);
		return result ? Ok("StudentGroup created successfully") : StatusCode(500, "Failed to create StudentGroup");
	}

	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var result = await _serv.GetAllAsync();
		return result != null ? Ok(result) : NotFound();
	}
}
