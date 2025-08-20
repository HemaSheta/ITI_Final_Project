using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Final_ITI.Data;
using Project_Final_ITI.Models;
using Training_Managment_System.Repositories.Implementations;
using Training_Managment_System.Repositories.Interfaces;
using Training_Managment_System.ViewModels;

namespace Training_Managment_System.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepo;
        
        public CourseController(ICourseRepository courseRepo)
        {
            _courseRepo = courseRepo;
            
        }


        // GET: Course
        public async Task<IActionResult> Index(string searchString)
        {
            IEnumerable<Course> courses;

            if (!string.IsNullOrEmpty(searchString))
            {
                courses = await _courseRepo.Find(
                    c => c.CourseName.Contains(searchString) || c.Category.Contains(searchString)
                );
            }
            else
            {
                courses = await _courseRepo.GetAll();
            }

            return View(courses);
        }
        // GET: Course/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var course = await _courseRepo.GetById(id);
            if (course == null)
                return NotFound();

            return View(course);
        }

        // GET: Course/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Course/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        [HttpPost]
        public async Task<IActionResult> Create(CourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                var course = new Course
                {
                    CourseName = model.CourseName,
                    Category = model.Category,
                    InstructorID = model.InstructorId
                };

                await _courseRepo.Add(course);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Course/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseRepo.GetById(id);
            if (course == null)
                return NotFound();

            var model = new CourseViewModel
            {
                CourseId = course.CourseId,
                CourseName = course.CourseName,
                Category = course.Category,
                InstructorId = course.InstructorID
            };

            return View(model);
        }

        // POST: Course/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.CourseId == null)
                    return BadRequest();

                var course = await _courseRepo.GetById(model.CourseId.Value);
                course.CourseName = model.CourseName;
                course.Category = model.Category;
                course.InstructorID = model.InstructorId;

                _courseRepo.Update(course);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
        // GET: Course/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseRepo.GetById(id);
            if (course == null)
                return NotFound();



            var viewModel = new CourseViewModel
            {
                CourseId = course.CourseId,
                CourseName = course.CourseName,
                Category = course.Category,
                InstructorId = course.InstructorID
            };

            return View(viewModel);
        }

        // POST: Course/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(CourseViewModel model)
        {
            if (model.CourseId == null)
                return BadRequest();
            var course = await _courseRepo.GetById(model.CourseId.Value);
            if (course != null)
            {
                await _courseRepo.Delete(course);

            }
            return RedirectToAction(nameof(Index));
        }
    }

}
