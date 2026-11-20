using System.ComponentModel.DataAnnotations;

namespace Laboratorio13.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        [MaxLength(45)]
        public string Name { get; set; } = string.Empty;

        public int Credit { get; set; }

        public bool IsActive { get; set; }

        // Un curso tiene muchas matrículas
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}