using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Final_ITI.Models
{
    public class StdEnrollsIn
    {
        
        public int UserId { get; set; }

       
        public int CourseId { get; set; }

        public User User { get; set; }
        public Course Course { get; set; }
    }
}

