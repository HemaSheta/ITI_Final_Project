using Microsoft.AspNetCore.Mvc;

namespace Training_Managment_System.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
