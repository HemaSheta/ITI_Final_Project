using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Final_ITI.Models
{
    public class Enrollment
    {
        
        public int StudentId { get; set; }

       
        public int CourseId { get; set; }

        // Navigation Properties

        public User User { get; set; }
        public Course Course { get; set; }
    }
}

