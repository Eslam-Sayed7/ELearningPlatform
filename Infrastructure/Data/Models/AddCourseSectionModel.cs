namespace Infrastructure.Data.Models;

public class AddCourseSectionModel
{
    public string? Title { get; set; }
    public int SectionSequence { get; set; } = 0;
    public List<AddCourseMaterialModel> Materials { get; set; } = new List<AddCourseMaterialModel>();
}