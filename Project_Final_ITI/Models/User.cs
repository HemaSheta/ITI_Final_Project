using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Training_Managment_System.Entities;

namespace TrainingSystem.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression("Admin|Instructor|Trainee", ErrorMessage = "Role must be Admin, Instructor, or Trainee.")]
        public string Role { get; set; }

        // Navigation Properties
        public ICollection<Course> Courses { get; set; }
        public ICollection<StdEnrollsIn> Enrollments { get; set; }
        public ICollection<Attends> Attendances { get; set; }
        public ICollection<StdHasGrade> Grades { get; set; }
    }
}
