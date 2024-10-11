using Core.Entities;

namespace Infrastructure.Dtos;

public class CourseCardDto
{
    public Guid CourseId { get; set; }
    public string CourseName { get; set; }
    public string CourseDescription { get; set; }
    public string? Instructor { get; set; }
    
    public string CategoryName { get; set; }    
    public string ThumbnailUrl { get; set; }
    public double Price { get; set; }
}