namespace Core.Entities
{
    public class Progress
    {
        public int ProgressId { get; set; }
        public Guid EnrollmentId { get; set; }
        public Guid MaterialId { get; set; } 
        public bool IsCompleted { get; set; }  
        public virtual Enrollment Enrollment { get; set; }
        public virtual CourseMaterial Material { get; set; }  
    }
}