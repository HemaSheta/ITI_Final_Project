using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingSystem.Models
{
    public class StdHasGrade
    {
        [Key, Column(Order = 0)]
        public int UserId { get; set; }

        [Key, Column(Order = 1)]
        public int SessionId { get; set; }

        public int GradeId { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public Session Session { get; set; }
        public Grade Grade { get; set; }
    }
}
