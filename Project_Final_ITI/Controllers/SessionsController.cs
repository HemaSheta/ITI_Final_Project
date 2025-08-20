using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Final_ITI.Models;
using Training_Managment_System.UnitOfWork;
using System.Threading.Tasks;
using Training_Managment_System.ViewModels;

namespace Training_Managment_System.Controllers
{
    public class SessionsController : Controller
    {
        private readonly IUnitOfWork iuow;

        public SessionsController(IUnitOfWork uow)
        {
            iuow = uow;
        }

        // Search for Sessions
        public async Task<IActionResult> Index(string? search)
        {
            var sessions = string.IsNullOrWhiteSpace(search)
                ? await iuow.session.GetAllWithCourseAsync()
                : await iuow.session.SearchByCourseNameAsync(search);

            ViewData["CurrentFilter"] = search;
            return View(sessions);
        }

        // Sessions details by ID
        public async Task<IActionResult> Details(int id)
        {
            var sessions = await iuow.session.GetAllWithCourseAsync();
            var session = sessions.FirstOrDefault(s => s.SessionId == id);

            if (session == null) return NotFound();
            return View(session);
        }

        // create a new Session
        public async Task<IActionResult> Create()
        {
            await PopulateCoursesDropDown();
            return View();
        }

        // create a new Session after compelete form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SessionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCoursesDropDown(model.CourseId);
                return View(model);
            }
            
            var session = new Session
            {
                CourseId = model.CourseId,
                StartDate = model.StartDate,
                EndDate = model.EndDate
            };
            await iuow.session.Add(session);
            await iuow.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        // edit page Sessions
        public async Task<IActionResult> Edit(int id)
        {
            var session = await iuow.session.GetById(id);
            if (session == null) return NotFound();

            await PopulateCoursesDropDown(session.CourseId);
            return View(session);
        }

        // confirm edits on Sessions
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SessionViewModel model)
        {
            if (id != model.SessionId) return BadRequest();

            if (!ModelState.IsValid)
            {
                await PopulateCoursesDropDown(model.CourseId);
                return View(model);
            }

            var session = await iuow.session.GetById(id);
            if (session == null) return NotFound();


            session.CourseId = model.CourseId;
            session.StartDate = model.StartDate;
            session.EndDate = model.EndDate;
            

            await iuow.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        // ensure the delete page
        public async Task<IActionResult> Delete(int id)
        {
            
            var sessions = await iuow.session.GetAllWithCourseAsync();
            var session = sessions.FirstOrDefault(s => s.SessionId == id);

            if (session == null) return NotFound();
            return View(session);
        }

        // confirm the delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var session = await iuow.session.GetById(id);
            if (session == null) return NotFound();

            await Task.Run(() => iuow.session.Delete(session));
            await iuow.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        // help to see courses
        private async Task PopulateCoursesDropDown(int? selectedId = null)
        {
            var courses = await iuow.session.GetAll();
            ViewBag.CourseId = new SelectList(courses, "CourseId", "CourseName", selectedId);
        }
    }
}
