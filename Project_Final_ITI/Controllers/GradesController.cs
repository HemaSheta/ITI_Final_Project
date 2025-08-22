using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Final_ITI.Models;
using System.Threading.Tasks;
using Training_Managment_System.UnitOfWork;
using Training_Managment_System.ViewModels;

namespace Training_Managment_System.Controllers
{
    public class GradesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public GradesController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        // GET: /Grade/Index
        public async Task<ActionResult> Index()
        {
            var grades = await _unitOfWork.grade.GetAllWithTraineeAndCourseAsync();
            return View(grades);
        }

        // GET: /Grade/Details/id
        public async Task<ActionResult> Details(int id)
        {
            var grade = await _unitOfWork.grade.GetById(id);
            if (grade == null) return NotFound();
            return View(grade);
        }

        // GET: /Grade/Create
        public async Task<ActionResult> Create()
        {
            await PopulateSessionsDropDown();
            await PopulateUsersDropDown();
            return View(new GradeViewModel());
        }

        // POST: /Grade/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GradeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateUsersDropDown(model.TraineeId);
                await PopulateSessionsDropDown(model.SessionId);
                return View(model);
            }

            var grade = new Grade
            {
                Value = model.Value,
                TraineeId = model.TraineeId,
                SessionId = model.SessionId
            };

            await _unitOfWork.grade.Add(grade);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(Index));
        }

        // ---------- Helpers ----------

        private async Task PopulateSessionsDropDown(int? selectedId = null)
        {
            var sessions = await _unitOfWork.session.GetAll();
            ViewBag.SessionId = new SelectList(sessions, "SessionId", "SessionId", selectedId);
        }

        private async Task PopulateUsersDropDown(int? selectedId = null)
        {
            var trainees = (await _unitOfWork.user.GetAll())
                           .Where(u => u.Role == "Trainee");

            ViewBag.TraineeId = new SelectList(trainees, "UserId", "UserName", selectedId);
        }
    }
}
