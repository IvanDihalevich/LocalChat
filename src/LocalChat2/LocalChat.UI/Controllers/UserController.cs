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
using LocalChat.Repository.Model;


namespace LocalChat.WebUI.Controllers
{
        public class UserController : Controller
        {
            private readonly IUserRepository userRepository;
        private readonly IUserService _userService;
        private readonly IMessageService messageRepository;

        public UserController(IUserRepository userRepository, IUserService userService, IMessageService _messageRepository)
            {
                this.userRepository = userRepository;
                _userService = userService;
                this.messageRepository = _messageRepository;
            }

            [Authorize(Roles = "Admin")]
            public async Task<IActionResult> Index()
            {
                return View(await userRepository.GetAllWithRolesAsync());
            }
        [HttpGet]
        public async Task<IActionResult> Friends()
        {
            return View(await userRepository.GetAllWithRolesAsync());
        }

        [Authorize(Roles = "Admin")]
            [HttpGet]
            public IActionResult Create()
            {
                return View(new UserCreateModel());
            }

            [Authorize(Roles = "Admin")]
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(UserCreateModel model)
            {
                if (ModelState.IsValid)
                {
                    var user = await userRepository.CreateWithPasswordAsync(model);

                    if (user != null)
                    {
                        return RedirectToAction(nameof(Edit), new { id = user.Id });
                    }
                }

                return View(new UserCreateModel());
            }

            [HttpGet]
            public async Task<IActionResult> Edit(Guid id)
            {
                ViewBag.Roles = await userRepository.GetRolesAsync();
                return View(await userRepository.GetOneWithRolesAsync(id));
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(UserListItemModel model, string[] roles)
            {
                if (ModelState.IsValid)
                {
                    await userRepository.UpdateUserAsync(model, roles);
                    return RedirectToAction("Index");
                }
                ViewBag.Roles = await userRepository.GetRolesAsync();
                return View(model);
            }
        [HttpGet]
        public async Task<IActionResult> Text(Guid id)
        {
            ViewData["ReciverId"] = id;
            ViewBag.Messages = messageRepository.GetAllMessagesByReciverId(id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Text(Message message, Guid id)
        {
            ViewData["SenderId"] = id;
            if (ModelState.IsValid)
            {
                await messageRepository.AddMessageAsync(message);
                return RedirectToAction("Text");
            }
            ViewBag.Messages = messageRepository.GetAllMessagesByReciverId(id);
            return View();
        }

        [HttpPost]
            public async Task<int> CheckDelete(Guid id)
            {
                var check = await userRepository.CheckUser(id);
                return check ? 1 : 0;
            }

            [HttpPost]
            public async Task<IActionResult> Delete(Guid userId)
            {
                _userService.DeleteUser(userId);
                return RedirectToAction("Index");
            }
        }
    }
