using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Final_ITI.Models;
using System.Linq;
using System.Threading.Tasks;
using Training_Managment_System.UnitOfWork;
using Training_Managment_System.ViewModels;

namespace Training_Managment_System.Controllers
{
    public class GradesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public GradesController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        // ========================
        // INDEX
        // ========================
        public async Task<ActionResult> Index()
        {
            var grades = await _unitOfWork.grade.GetAllWithTraineeAndCourseAsync();
            return View(grades);
        }

        // ========================
        // DETAILS
        // ========================
        public async Task<ActionResult> Details(int id)
        {
            var grade = await _unitOfWork.grade.GetByIdWithIncludesAsync(id);
            if (grade == null) return NotFound();
            return View(grade);
        }

        // ========================
        // CREATE
        // ========================
        public async Task<ActionResult> Create()
        {
            await PopulateSessionsDropDown();
            await PopulateUsersDropDown();
            return View(new GradeViewModel());
        }

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

        // ========================
        // EDIT
        // ========================
        public async Task<ActionResult> Edit(int id)
        {
            var grade = await _unitOfWork.grade.GetByIdWithIncludesAsync(id);
            if (grade == null) return NotFound();

            var vm = new GradeViewModel
            {
                Value = grade.Value,
                TraineeId = grade.TraineeId,
                SessionId = grade.SessionId
            };

            await PopulateUsersDropDown(grade.TraineeId);
            await PopulateSessionsDropDown(grade.SessionId);

            ViewBag.GradeId = grade.GradeId;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GradeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateUsersDropDown(model.TraineeId);
                await PopulateSessionsDropDown(model.SessionId);
                ViewBag.GradeId = id;
                return View(model);
            }

            var grade = await _unitOfWork.grade.GetByIdWithIncludesAsync(id);
            if (grade == null) return NotFound();

            grade.Value = model.Value;
            grade.TraineeId = model.TraineeId;
            grade.SessionId = model.SessionId;

            await _unitOfWork.grade.Update(grade);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(Index));
        }

        // ========================
        // DELETE
        // ========================
        public async Task<ActionResult> Delete(int id)
        {
            var grade = await _unitOfWork.grade.GetByIdWithIncludesAsync(id);
            if (grade == null) return NotFound();

            return View(grade);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grade = await _unitOfWork.grade.GetByIdWithIncludesAsync(id);
            if (grade == null) return NotFound();

            await _unitOfWork.grade.Delete(grade);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(Index));
        }

        // ========================
        // HELPERS
        // ========================
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
