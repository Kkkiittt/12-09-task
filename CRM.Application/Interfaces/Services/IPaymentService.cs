using CRM.Application.Dtos;
using CRM.Domain.Entities;

namespace CRM.Application.Interfaces.Services;

public interface IPaymentService : IService<Payment>
{
	public Task<IEnumerable<PaymentCourseGetDto>> GetDetailedPaymentsAsync();
}
