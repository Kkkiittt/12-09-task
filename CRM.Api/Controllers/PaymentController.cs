using CRM.Application.Interfaces.Services;
using CRM.Domain.Entities;

using Microsoft.AspNetCore.Mvc;

namespace CRM.Api.Controllers;
[ApiController]
[Route("/payments")]
public class PaymentController : ControllerBase
{
	private readonly IPaymentService _paymentService;

	public PaymentController(IPaymentService paymentService)
	{
		_paymentService = paymentService;
	}

	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var payments = await _paymentService.GetAllAsync();
		return payments != null ? Ok(payments) : NotFound();
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> Get(int id)
	{
		var payment = await _paymentService.GetAsync(id);
		return payment != null ? Ok(payment) : NotFound();
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] Payment entity)
	{
		var result = await _paymentService.CreateAsync(entity);
		return result ? Ok("Payment created successfully") : StatusCode(500, "Failed to create payment");
	}

	[HttpGet("detailed")]
	public async Task<IActionResult> GetDetailedPayments()
	{
		var payments = await _paymentService.GetDetailedPaymentsAsync();
		return payments != null ? Ok(payments) : NotFound();
	}
}