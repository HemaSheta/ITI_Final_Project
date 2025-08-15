using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Training_Managment_System.Entities;

namespace Project_Final_ITI.Models
{
    public class Attends
    {
        
        public int UserId { get; set; }

       
        public int SessionId { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public Session Session { get; set; }
    }
}
