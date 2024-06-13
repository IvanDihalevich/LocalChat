using LocalChat.Core.Entities;
using LocalChat.Repository.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace LocalChat.UI.Controllers
{
    public class ChatRoomAdminController : Controller
    {
        private readonly IChatRoomService _chatRoomService;
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;

        public ChatRoomAdminController(
            IChatRoomService chatRoomService,
            IMessageService messageService,
            IUserService userService)
        {
            _chatRoomService = chatRoomService ?? throw new ArgumentNullException(nameof(chatRoomService));
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        public IActionResult Index()
        {
            var chatRooms = _chatRoomService.GetAllChatRooms();
            return View(chatRooms);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ChatRoom chatRoom)
        {
            if (chatRoom == null)
            {
                return BadRequest("Chat room is required.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _chatRoomService.CreateChatRoom(chatRoom);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"An error occurred while creating the chat room: {ex.Message}");
                }
            }

            return View(chatRoom);
        }

        [HttpPost]
        public IActionResult Delete(Guid chatId)
        {
                _chatRoomService.DeleteChatRoom(chatId);
                return RedirectToAction("Index");
        }
    }
}
