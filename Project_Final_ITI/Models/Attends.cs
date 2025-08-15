using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Training_Managment_System.Entities;

namespace TrainingSystem.Models
{
    public class Attends
    {
        [Key, Column(Order = 0)]
        public int UserId { get; set; }

        [Key, Column(Order = 1)]
        public int SessionId { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public Session Session { get; set; }
    }
}
