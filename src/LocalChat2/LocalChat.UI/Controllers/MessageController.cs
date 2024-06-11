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
using Microsoft.AspNetCore.SignalR;


namespace LocalChat.UI.Controllers
{
    [Authorize]  
    public class MessageController : Controller
    {
        private readonly ChatDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly IMessageService _messageService;
        private readonly IHubContext<ChatHub> _hubContext;

        public MessageController(ChatDbContext dbContext, UserManager<User> userManager, IMessageService messageService, IHubContext<ChatHub> hubContext)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _messageService = messageService;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> Index(Guid id)
        {
            ViewData["ChatRoomId"] = id;
            var messages = await _messageService.GetAllByChatRoomId(id);
            messages = messages.OrderBy(m => m.SendTime); // Сортування за часом спаданням
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

                // Send the message to the SignalR hub
                await _hubContext.Clients.Group(model.ChatRoomId.ToString())
                    .SendAsync("ReceiveMessage", model.SenderId, model.Text, model.ChatRoomId);

                return RedirectToAction("Index", new { id = model.ChatRoomId });
            }

            return RedirectToAction("Index", new { id = model.ChatRoomId });
        }
    }
}