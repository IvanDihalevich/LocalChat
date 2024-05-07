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
        public IActionResult Delete(Guid id)
        {
            return View(_chatRoomService.GetChatRoomById(id));
        }

        // POST: ProjectsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id, IFormCollection form)
        {
            try
            {
                _chatRoomService.DeleteChatRoom(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Delete", new { id = id });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid chat room ID.");
            }

            try
            {
                _chatRoomService.DeleteChatRoom(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while deleting the chat room: {ex.Message}");
                var chatRoom = _chatRoomService.GetChatRoomById(id);
                return View("Delete", chatRoom);
            }
        }
    }
}
