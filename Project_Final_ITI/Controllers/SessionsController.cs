using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Final_ITI.Models;
using Training_Managment_System.UnitOfWork;
using System.Threading.Tasks;

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
                ? await iuow.SessionRepository.GetAll()
                : await iuow.SessionRepository.SearchByCourseNameAsync(search);

            ViewData["CurrentFilter"] = search;
            return View(sessions);
        }

        // Sessions details by ID
        public async Task<IActionResult> Details(int id)
        {
            var session = await iuow.SessionRepository.GetById(id);
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
        public async Task<IActionResult> Create(Session session)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCoursesDropDown(session.CourseId);
                return View(session);
            }

            await iuow.SessionRepository.Add(session);
            await iuow.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        // edit page Sessions
        public async Task<IActionResult> Edit(int id)
        {
            var session = await iuow.SessionRepository.GetById(id);
            if (session == null) return NotFound();

            await PopulateCoursesDropDown(session.CourseId);
            return View(session);
        }

        // confirm edits on Sessions
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Session session)
        {
            if (id != session.SessionId) return BadRequest();

            if (!ModelState.IsValid)
            {
                await PopulateCoursesDropDown(session.CourseId);
                return View(session);
            }

            iuow.SessionRepository.Update(session);
            await iuow.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        // ensure the delete page
        public async Task<IActionResult> Delete(int id)
        {
            var session = await iuow.SessionRepository.GetById(id);
            if (session == null) return NotFound();
            return View(session);
        }

        // confirm the delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var session = await iuow.SessionRepository.GetById(id);
            if (session == null) return NotFound();

            await Task.Run(() => iuow.SessionRepository.Delete(session));
            await iuow.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        // help to see courses
        private async Task PopulateCoursesDropDown(int? selectedId = null)
        {
            var courses = await iuow.CourseRepository.GetAll();
            ViewBag.CourseId = new SelectList(courses, "Id", "Name", selectedId);
        }
    }
}
