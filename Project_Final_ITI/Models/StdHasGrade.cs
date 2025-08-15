using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Final_ITI.Models
{
    public class StdHasGrade
    {
        public int UserId { get; set; }

        public int SessionId { get; set; }

        public int GradeId { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public Session Session { get; set; }
        public Grade Grade { get; set; }
    }
}
