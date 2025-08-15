using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Training_Managment_System.Entities;

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
        public int UserId { get; set; } // InstructorId


        public User Instructor { get; set; }
        public ICollection<Session> Sessions { get; set; }
        public ICollection<StdEnrollsIn> Enrollments { get; set; }
    }
}
