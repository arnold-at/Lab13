namespace Laboratorio13.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }

        // Llave foránea hacia Student
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;

        // Llave foránea hacia Course
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public DateTime Date { get; set; }

        public bool IsActive { get; set; }
    }
}