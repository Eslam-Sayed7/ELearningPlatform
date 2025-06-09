namespace Core.Entities
{
    public class Progress
    {
        public Guid EnrollmentId { get; set; }
        public Guid SectionId { get; set; } 
        public bool IsCompleted { get; set; }  
        public virtual Enrollment Enrollment { get; set; }
        public virtual CourseSection Section { get; set; }
        
    }
}