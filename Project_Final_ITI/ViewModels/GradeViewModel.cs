using System.ComponentModel.DataAnnotations;
namespace Training_Managment_System.ViewModels
{
    public class GradeViewModel
    {
        [Required]
        public double Value { get; set; }
        [Required]
        public int TraineeId { get; set; }
        public string TraineeName { get; set; }
        [Required]
        public int SessionId { get; set; }
        public string courseName { get; set; }




                                
                                
    }
}
