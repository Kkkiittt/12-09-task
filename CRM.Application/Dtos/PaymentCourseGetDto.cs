namespace CRM.Application.Dtos;

public class PaymentCourseGetDto
{
	public string StudentName { get; set; } = string.Empty;
	public string GroupName { get; set; } = string.Empty;
	public string CourseTitle { get; set; } = string.Empty;
	public double Amount { get; set; }
	public DateTime PaidAt { get; set; }
}

