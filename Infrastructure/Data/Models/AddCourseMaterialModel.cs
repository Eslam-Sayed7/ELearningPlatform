using Core.Enums;

namespace Infrastructure.Data.Models;

public class AddCourseMaterialModel
{
    public Guid SectionId { get; set; }
    public MaterialType MaterialType { get; set; }
    public string TextContent { get; set; }
    public string Url { get; set; }
    public int MaterialSequence { get; set; }
}