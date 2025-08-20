using System.ComponentModel.DataAnnotations;

namespace Training_Managment_System.ViewModels
{
    public class SessionViewModel
    {

        public int? SessionId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}
