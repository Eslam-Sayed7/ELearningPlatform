namespace Infrastructure.Dtos;

public class GetCourseDto
{
    public Guid CourseId { get; set; }
    public string CourseName { get; set; } = null!;
    public string? Description { get; set; }
    public string? Instructor { get; set; }
    public string CategoryName { get; set; }
    public string Level { get; set; }
    public double Price { get; set; }
    public int Duration { get; set; }
    public string ThumbnailUrl { get; set; }
    public string Language { get; set; }
}