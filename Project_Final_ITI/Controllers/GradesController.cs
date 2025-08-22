using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Final_ITI.Models;
using System.Diagnostics;
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
            var grades = await _unitOfWork.grade.GetAllWithTraneeAndCourseAsync();
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
        public  async Task<ActionResult> CreateAsync()
        {
            await PopulateSessionsDropDown();
            await PopulateUsersDropDown();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(GradeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existsGrades = await _unitOfWork.grade.GetAll();
                var grade = new Grade
                {
                    SessionId = model.SessionId,
                    TraineeId = model.TraineeId,
                    Value = model.gardeValue

                };
                await _unitOfWork.grade.Add(grade);
                return RedirectToAction("Index");
            }
            await PopulateSessionsDropDown(model.SessionId);
            await PopulateUsersDropDown(model.TraineeId);
            return View(model);
        }






        private async Task PopulateSessionsDropDown(int? selectedId = null)
        {
            var sessions = await _unitOfWork.session.GetAll();
            ViewBag.SessionId = new SelectList(sessions, "SessionId", "SessionId", selectedId);
        }
        private async Task PopulateUsersDropDown(int? selectedId = null)
        {

            var Users = await _unitOfWork.user.GetAll();
            ViewBag.TraineeId = new SelectList(Users, "UserId", "UserName", selectedId);
        }


    }
}
