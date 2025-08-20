using Microsoft.AspNetCore.Mvc;
using Project_Final_ITI.Models;
using Training_Managment_System.Repositories.Interfaces;
using Training_Managment_System.UnitOfWork;
using Training_Managment_System.ViewModels;

namespace Training_Managment_System.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        ///////////////////////////////////////////////////Home/////////////////////////////////////////
        public async Task<IActionResult> Index(string name, string role)
        {
            var users = await _unitOfWork.Users.GetAll();

            if (!string.IsNullOrEmpty(name))
            {
                users = users.Where(u => u.UserName.Contains(name));
            }

            if (!string.IsNullOrEmpty(role) && role != "All")
            {
                users = users.Where(u => u.Role == role);
            }

            ViewBag.CurrentName = name;
            ViewBag.CurrentRole = role;

            return View(users.ToList());
        }

        ////////////////////////////////////////////////////Add//////////////////////////////////////////
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(UserViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = new User
            {
                UserName = vm.UserName,
                Email = vm.Email,
                Role = vm.Role
            };

            await _unitOfWork.Users.Add(user);
            await _unitOfWork.CompleteAsync(); // ✅ commit changes

            TempData["Message"] = "User added successfully!";
            return RedirectToAction("Index");
        }

        //////////////////////////////////////////////Edit///////////////////////////////////////////////////
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            var user = await _unitOfWork.Users.GetById(id.Value);
            if (user == null) return NotFound();

            var vm = new UserViewModel
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = await _unitOfWork.Users.GetById(id);
            if (user == null) return NotFound();

            user.UserName = vm.UserName;
            user.Email = vm.Email;
            user.Role = vm.Role;

            await _unitOfWork.Users.Update(user);
            await _unitOfWork.CompleteAsync(); // ✅ commit changes

            TempData["Message"] = "User updated successfully!";
            return RedirectToAction("Index");
        }

        //////////////////////////////////////////////Delete///////////////////////////////////////////////////
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var user = await _unitOfWork.Users.GetById(id.Value);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _unitOfWork.Users.GetById(id);
            if (user != null)
            {
                await _unitOfWork.Users.Delete(user);
                await _unitOfWork.CompleteAsync(); // ✅ commit changes
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
