namespace Infrastructure.Dtos;

public class CourseCardDto
{
    public Guid CourseId { get; set; }
    public string CourseName { get; set; }
    public virtual string Category { get; set; }    
    public string ThumbnailUrl { get; set; }
    public double Price { get; set; }
}