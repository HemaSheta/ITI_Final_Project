using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Final_ITI.Models;
using Training_Managment_System.Repositories.Interfaces;

namespace Training_Managment_System.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        ///////////////////////////////////////////////////Home/////////////////////////////////////////
        public async Task<IActionResult> Index(string name, string role)
        {
            // Start with all users
            var users = await _userRepository.GetAll();

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

        // POST: Users/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(User ur)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = ur.UserName,
                    Email = ur.Email,
                    Role = ur.Role
                };

                await _userRepository.Add(user);

                TempData["Message"] = "User added successfully!";
                return RedirectToAction("Index");
            }

            return View(ur);
        }

        //////////////////////////////////////////////Edit///////////////////////////////////////////////////
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            var user = await _userRepository.GetById(id.Value);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User ur)
        {
            if (!ModelState.IsValid) return View(ur);

            var user = await _userRepository.GetById(id);
            if (user == null) return NotFound();

            user.UserName = ur.UserName;
            user.Email = ur.Email;
            user.Role = ur.Role;

            _userRepository.Update(user);

            TempData["Message"] = "User updated successfully!";
            return RedirectToAction("Index");
        }

        //////////////////////////////////////////////Delete///////////////////////////////////////////////////
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var user = await _userRepository.GetById(id.Value);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user != null)
            {
                await _userRepository.Delete(user);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
