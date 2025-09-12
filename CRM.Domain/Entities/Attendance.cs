namespace CRM.Domain.Entities;

public class Attendance
{
	public int Id { get; set; }
	public int StudentGroupId { get; set; }
	public DateTime LessonAt { get; set; }
	public bool Present { get; set; }
}

