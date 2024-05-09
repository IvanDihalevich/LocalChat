using Microsoft.AspNetCore.Mvc;
using LocalChat.Core.Context;
using LocalChat.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LocalChat.Repository.Services;
using System.Security.Claims;

namespace LocalChat.UI.Controllers
{
    [Authorize]  // Require authentication
    public class MessageController : Controller
    {
        private readonly ChatDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly IMessageService _messageService;

        public MessageController(ChatDbContext dbContext, UserManager<User> userManager, IMessageService messageService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid id)
        {
            ViewData["ChatRoomId"] = id;
            var messages = await _messageService.GetAllByChatRoomId(id);
            return View(messages);
        }

        [HttpGet]
        public IActionResult Create(Guid id)
        {
            ViewData["ChatRoomId"] = id;
            return View(new Message() { ChatRoomId = id });
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Message model)
        {
            if (ModelState.IsValid)
            {
                await _messageService.AddMessageAsync(model); // Виклик методу AddMessageAsync вашого сервісу
                return RedirectToAction("Index", new { id = model.ChatRoomId });
            }

            return RedirectToAction("Index", new { id = model.ChatRoomId });
        }



    }
}