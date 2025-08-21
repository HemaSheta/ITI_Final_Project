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
        public IActionResult Index()
        {
            var grades = _unitOfWork.grade.GetAll();
            return View(grades);
        }
        // GET: /Grade/Details/{id}
        public IActionResult Details(int id)
        {
            var grade = _unitOfWork.grade.GetById(id);
            if (grade == null) return NotFound();
            return View(grade);
        }
        // GET: /Grade/Create
        public ActionResult Create()
        {
            ViewBag.SessionId = new SelectList((System.Collections.IEnumerable)_unitOfWork.session.GetAll(), "Id", "Id");
            ViewBag.TraineeId = new SelectList((System.Collections.IEnumerable)_unitOfWork.user.GetAll(), "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Grade grade)
        {
            if (ModelState.IsValid)
            {
                
                var exists = await _unitOfWork.grade.GetAll();
                var existsBool = exists.Any(g =>
                    g.SessionId == grade.SessionId &&
                    g.TraineeId == grade.TraineeId);

                if (existsBool)
                {
                    ModelState.AddModelError("", "this trainee already has a grade in this session!!");
                }
                else
                {
                    _ = _unitOfWork.grade.Add(grade);////////////////////////////////
                    //_unitOfWork.Complete(); // already existing
                    return RedirectToAction("Index");
                }
            }
            ViewBag.SessionId = new SelectList((System.Collections.IEnumerable)_unitOfWork.session.GetAll(), "Id", "Id", grade.SessionId);
            ViewBag.TraineeId = new SelectList((System.Collections.IEnumerable)_unitOfWork.user.GetAll(),"Id", "Name", grade.TraineeId);
            return View(grade);
        }





        



    }
}
