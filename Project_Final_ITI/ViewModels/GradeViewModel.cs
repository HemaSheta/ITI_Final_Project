using System.ComponentModel.DataAnnotations;
namespace Training_Managment_System.ViewModels
{
    public class GradeViewModel
    {
        [Required]
        public double Value { get; set; }
        [Required]
        public int TraineeId { get; set; }
        [Required]
        public int SessionId { get; set; }
    }
}
