using CRM.Application.Interfaces.Services;
using CRM.Domain.Entities;

using Microsoft.AspNetCore.Mvc;

namespace CRM.Api.Controllers;
[ApiController]
[Route("/groups")]
public class GroupController : ControllerBase
{
	private readonly IGroupService _groupService;

	public GroupController(IGroupService groupService)
	{
		_groupService = groupService;
	}

	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var groups = await _groupService.GetAllAsync();
		return groups != null ? Ok(groups) : NotFound();
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> Get(int id)
	{
		var group = await _groupService.GetAsync(id);
		return group != null ? Ok(group) : NotFound();
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] Group entity)
	{
		var result = await _groupService.CreateAsync(entity);
		return result ? Ok("Group created successfully") : StatusCode(500, "Failed to create group");
	}
}