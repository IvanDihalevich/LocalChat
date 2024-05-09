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
        public async Task<IActionResult> Index()
        {
            var messages = await _messageService.GetAll();
            return View(messages);
        }

        [HttpGet]
        public IActionResult Create(Guid chatRoomId)
        {
            // Pass the chatRoomId to the Create view
            ViewData["ChatRoomId"] = chatRoomId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid chatRoomId, [Bind("Name")] ChatRoom chatRoom, Message message)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the currently authenticated user
                var user = await _userManager.GetUserAsync(User);

                // Set the SenderId using the currently authenticated user
                message.SenderId = Guid.Parse(await _userManager.GetUserIdAsync(user));

                // Set the ChatRoomId from the provided parameter
                message.ChatRoomId = chatRoomId;
                // Set the ChatRoom object from the provided parameter
                message.ChatRoom = chatRoom;

                // Call the service method to add the message
                await _messageService.AddMessageAsync(message);

                // Redirect to the Index action after successful creation
                return RedirectToAction("Index");
            }

            // If the model state is not valid, populate the ViewData dictionary with the chatRoomId parameter
            ViewData["ChatRoomId"] = chatRoomId;
            // Populate the ViewData dictionary with the chatRoom parameter
            ViewData["ChatRoom"] = chatRoom;

            // Return the view with the posted data
            return View(message);
        }



    }
}