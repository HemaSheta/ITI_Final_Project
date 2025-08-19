
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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

        [ValidateNever]
        public User User { get; set; }

        [ValidateNever]
        public ICollection<Session> Sessions { get; set; }

        [ValidateNever]
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
