using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Final_ITI.Data;
using Project_Final_ITI.Models;
using Training_Managment_System.Repositories.Implementations;
using Training_Managment_System.Repositories.Interfaces;
using Training_Managment_System.UnitOfWork;
using Training_Managment_System.ViewModels;

namespace Training_Managment_System.Controllers
{
    public class CourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CourseController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;



        // GET: Course
        public async Task<IActionResult> Index(string searchString)
        {
            IEnumerable<Course> courses;

            if (!string.IsNullOrEmpty(searchString))
            {
                courses = await _unitOfWork.course.FindWithInstructor(
                    c => c.CourseName.Contains(searchString) || c.Category.Contains(searchString) 
                );
            }
            else
            {
                courses = await _unitOfWork.course.GetAllWithInstructorAsync();
            }

            return View(courses);
        }
        // GET: Course/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var course = await _unitOfWork.course.GetById(id);
            if (course == null)
                return NotFound();

            return View(course);
        }

        // GET: Course/Create
        public async Task<IActionResult> Create()
        {
            var instructors = await _unitOfWork.user.GetAllInstructorsAsync();
            ViewBag.Instructors = new SelectList(instructors, "UserId", "UserName");
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

                await _unitOfWork.course.Add(course);
                return RedirectToAction(nameof(Index));
            }
            var instructors = await _unitOfWork.user.GetAllInstructorsAsync();
            ViewBag.Instructors = new SelectList(instructors, "UserId", "UserName", model.InstructorId);
            return View(model);
        }

        // GET: Course/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _unitOfWork.course.GetById(id);
            if (course == null)
                return NotFound();

            var model = new CourseViewModel
            {
                CourseId = course.CourseId,
                CourseName = course.CourseName,
                Category = course.Category,
                InstructorId = course.InstructorID
            };

            var instructors = await _unitOfWork.user.GetAllInstructorsAsync();
            ViewBag.Instructors = new SelectList(instructors, "UserId", "UserName", course.InstructorID);

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

                var course = await _unitOfWork.course.GetById(model.CourseId.Value);
                course.CourseName = model.CourseName;
                course.Category = model.Category;
                course.InstructorID = model.InstructorId;

                await _unitOfWork.course.Update(course);
                return RedirectToAction(nameof(Index));
            }

            var instructors = await _unitOfWork.user.GetAllInstructorsAsync();
            ViewBag.Instructors = new SelectList(instructors, "UserId", "UserName", model.InstructorId);


            return View(model);
        }
        // GET: Course/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _unitOfWork.course.GetCourseWithInstructor(id);
            if (course == null)
                return NotFound();



            var viewModel = new CourseViewModel
            {
                CourseId = course.CourseId,
                CourseName = course.CourseName,
                Category = course.Category,
                InstructorId = course.InstructorID,
                InstructorName = course.User != null ? course.User.UserName : "N/A"
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
            var course = await _unitOfWork.course.GetById(model.CourseId.Value);
            if (course != null)
            {
                await _unitOfWork.course.Delete(course);

            }
            return RedirectToAction(nameof(Index));
        }
    }

}
