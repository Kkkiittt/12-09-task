namespace CRM.Domain.Entities;

public class Group
{
	public int Id { get; set; }
	public int CourseId { get; set; }
	public string Name { get; set; } = string.Empty;
	public DateTime StartedAt { get; set; }
}

