using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_Final_ITI.Models
{
    public class Grade
    {
        public int GradeId { get; set; }

        [Required, Range(0, 100)]
        public double Value { get; set; }

        public ICollection<StdHasGrade> StudentGrades { get; set; }
    }
}
