using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using LocalChat.Core.Entities;
using LocalChat.Repository;
using LocalChat.Repository.Services;
using Microsoft.AspNetCore.Authorization;
using LocalChat.Repository.UserRepositories;
using Microsoft.AspNetCore.Identity;
using LocalChat.UI.Areas.Identity.Pages.Account;


namespace LocalChat.WebUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserRepository userRepository;
        private readonly SignInManager<User> _signInManager;
        public UserController(IUserService userService, IUserRepository userRepository, SignInManager<User> signInManager)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }


        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var usersWithRoles = await userRepository.GetAllWithRolesAsync();  // Отримуємо потрібний формат
            return View(usersWithRoles);  // Передаємо дані у правильному форматі
        }
        [HttpGet]
        public IActionResult Message()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Details(Guid id)
        {
            var user = _userService.GetUserById(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _userService.AddUser(user);
                Console.WriteLine("Created user");
                return RedirectToAction(nameof(Index));
            }
            else Console.WriteLine("Not");
            return View(user);
        }

        public IActionResult Edit(Guid id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _userService.UpdateUser(user);
                }
                catch (Exception)
                {
                    if (!_userService.UserExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        [HttpPost]
        public async Task<int> CheckDelete(Guid id)
        {
            var check = await userRepository.CheckUser(id);
            return check ? 1 : 0;
        }

        [HttpDelete]
        public async Task Delete(Guid id)
        {
            await userRepository.DeleteUser(id);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel.InputModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    // Successful login, redirect to the desired page
                    return RedirectToAction("Index", "Home"); // Example
                }
                else
                {
                    // Invalid login attempt, display error message
                    ModelState.AddModelError(string.Empty, "Invalid login attempt");
                    return View(model);
                }
            }
            // Invalid input data, redisplay the login form with errors
            return View(model);
        }
    }
}
