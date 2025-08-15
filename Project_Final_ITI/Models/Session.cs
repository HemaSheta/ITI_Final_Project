using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_Final_ITI.Models
{
    public class Session : IValidatableObject
    {
        public int SessionId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int CourseId { get; set; }

        public Course Course { get; set; }
        public ICollection<Attends> Attendances { get; set; }
        public ICollection<StdHasGrade> Grades { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate.Date < DateTime.Now.Date)
            {
                yield return new ValidationResult(
                    "StartDate cannot be in the past.",
                    new[] { nameof(StartDate) }
                );
            }

            if (EndDate <= StartDate)
            {
                yield return new ValidationResult(
                    "EndDate must be after StartDate.",
                    new[] { nameof(EndDate) }
                );
            }
        }
    }
}
