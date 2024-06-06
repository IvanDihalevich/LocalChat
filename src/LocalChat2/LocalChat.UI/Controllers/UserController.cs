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
            public UserController(IUserRepository userRepository)
            {
                this.userRepository = userRepository;
            }

            [Authorize(Roles = "Admin")]
            public async Task<IActionResult> Index()
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
        }
    }
