namespace Infrastructure.Data.Models;

public class AddCourseModel
{
    public string CourseName { get; set; }
    public string? Description { get; set; }
    public Guid? CategoryId { get; set; }
    public string Level { get; set; }
    public double Price { get; set; }
    public int Duration { get; set; }
    public string ThumbnailUrl { get; set; }
    public string Language { get; set; }
    public List<AddCourseSectionModel> Sections { get; set; } = new List<AddCourseSectionModel>();
}