using System.ComponentModel.DataAnnotations;

namespace Laboratorio13.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        // Llave foránea hacia Grade
        public int GradeId { get; set; }
        public Grade Grade { get; set; } = null!;

        [MaxLength(45)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(45)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(45)]
        public string Phone { get; set; } = string.Empty;

        [MaxLength(45)]
        public string Email { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        // Un estudiante tiene muchas matrículas
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}