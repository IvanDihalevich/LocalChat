using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using LocalChat.Core.Entities;
using LocalChat.Repository;
using LocalChat.Repository.Services;

namespace LocalChat.WebUI.Controllers
{
    public class ChatRoomController : Controller
    {
        private readonly IChatRoomService _chatRoomService;
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;

        public ChatRoomController(IChatRoomService chatRoomService, IMessageService messageService, IUserService userService)
        {
            _chatRoomService = chatRoomService;
            _messageService = messageService;
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var chatRooms = _chatRoomService.GetAllChatRooms();
            return View(chatRooms);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ChatRoom chatRoom)
        {
            if (ModelState.IsValid)
            {
                _chatRoomService.CreateChatRoom(chatRoom);
                return RedirectToAction("Index");
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

    }
}
