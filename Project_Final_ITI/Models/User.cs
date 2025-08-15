using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Project_Final_ITI.Data; 

namespace Project_Final_ITI.Models
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
        public ICollection<Course> Courses { get; set; }          // instructor
        public ICollection<Enrollment> Enrollments { get; set; }
        
        public ICollection<Grade> Grades { get; set; }            // trainee
    }
}
