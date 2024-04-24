using LocalChat.Core.Entities;
using LocalChat.Repository;
using LocalChat.Repository.Services;
using Microsoft.AspNetCore.Mvc;

namespace LocalChat.UI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUserService _userService;
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _userService.AddUser(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
    }
}
