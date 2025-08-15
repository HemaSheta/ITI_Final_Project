using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_Final_ITI.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        public string CourseName { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public int InstructorID { get; set; } // InstructorId

        // Navigation Properties


        public User User { get; set; }
        public ICollection<Session> Sessions { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
