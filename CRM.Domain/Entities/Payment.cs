namespace CRM.Domain.Entities;

public class Payment
{
	public int Id { get; set; }
	public int StudentGroupId { get; set; }
	public double Amount { get; set; }
	public DateTime PaidAt { get; set; }
}

