using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Final_ITI.Data;
using Microsoft.EntityFrameworkCore;
using Project_Final_ITI.Models;
using System.Threading.Tasks;
using System.Linq;

namespace Training_Managment_System.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        ///////////////////////////////////////////////////Home/////////////////////////////////////////

        //public IActionResult Index()
        //{
        //    return View(_context.Users.ToList());
        //}
        public IActionResult Index(string name, string role)
        {
            // Start with all users
            var users = _context.Users.AsQueryable();

            // Filter by name
            if (!string.IsNullOrEmpty(name))
            {
                users = users.Where(u => u.UserName.Contains(name));
            }

            // Filter by role (only if not "All")
            if (!string.IsNullOrEmpty(role) && role != "All")
            {
                users = users.Where(u => u.Role == role);
            }

            // Pass current search values back to the View (for persistence)
            ViewBag.CurrentName = name;
            ViewBag.CurrentRole = role;

            return View(users.ToList());
        }
        ////////////////////////////////////////////////////Add//////////////////////////////////////////

        // GET: Users/Add
        public IActionResult Add()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(User ur)
        {

            var user = new User
            {
                UserName = ur.UserName,
                Email = ur.Email,
                Role = ur.Role
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            TempData["Message"] = "User added successfully!";
            return RedirectToAction("Index");
        }

        //////////////////////////////////////////////Edit///////////////////////////////////////////////////
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null) return BadRequest();

            var user = _context.Users.FirstOrDefault(p => p.UserId == id);
            if (user == null) return NotFound();

            var ur = new User
            {
               UserName = user.UserName,
                Email = user.Email,
                Role = user.Role
            };

            return View(ur);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, User ur)
        {

            var user = _context.Users.FirstOrDefault(p => p.UserId == id);
            if (user == null) return NotFound();

            user.UserName = ur.UserName;
            user.Email = ur.Email;
            user.Role = ur.Role;

            _context.SaveChanges();
            TempData["Message"] = "Product updated successfully!";
            return RedirectToAction("Index");

        }

        //////////////////////////////////////////////Delete///////////////////////////////////////////////////
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var user = await _context.Users.FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
