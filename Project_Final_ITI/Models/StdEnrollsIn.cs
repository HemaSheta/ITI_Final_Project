using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingSystem.Models
{
    public class StdEnrollsIn
    {
        [Key, Column(Order = 0)]
        public int UserId { get; set; }

        [Key, Column(Order = 1)]
        public int CourseId { get; set; }

        public User User { get; set; }
        public Course Course { get; set; }
    }
}
