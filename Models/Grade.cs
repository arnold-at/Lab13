using System.ComponentModel.DataAnnotations;

namespace Laboratorio13.Models
{
    public class Grade
    {
        public int GradeId { get; set; }

        [MaxLength(45)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(45)]
        public string Description { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        // Un grado tiene muchos estudiantes
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}