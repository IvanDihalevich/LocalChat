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

        public ChatRoomController(IChatRoomService chatRoomService, IMessageService messageService)
        {
            _chatRoomService = chatRoomService;
            _messageService = messageService;
        }

        public IActionResult Index()
        {
            var chatRooms = _chatRoomService.GetAllChatRooms();
            if (chatRooms == null)
            {
                // Обробка випадку, коли chatRooms є нульовим
                return View(new List<Message>());
            }

            var messages = chatRooms.SelectMany(chatRoom => chatRoom.Messages);
            return View(messages);
        }





        public IActionResult Details(Guid id)
        {
            var chatRoom = _chatRoomService.GetChatRoomById(id);
            return View(chatRoom);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendMessage(Message message)
        {
            if (ModelState.IsValid)
            {
                message.SendTime = DateTime.Now; // Set current time
                _messageService.SendMessage(message);
            }
            return RedirectToAction(nameof(Index));
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
                return RedirectToAction(nameof(Index));
            }
            return View(chatRoom);
        }

        public IActionResult Edit(Guid id)
        {
            var chatRoom = _chatRoomService.GetChatRoomById(id);
            if (chatRoom == null)
            {
                return NotFound();
            }
            return View(chatRoom);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, ChatRoom chatRoom)
        {
            if (id != chatRoom.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _chatRoomService.UpdateChatRoom(chatRoom);
                }
                catch (Exception)
                {
                    if (!_chatRoomService.ChatRoomExists(id))
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
            return View(chatRoom);
        }

        public IActionResult Delete(Guid id)
        {
            var chatRoom = _chatRoomService.GetChatRoomById(id);
            if (chatRoom == null)
            {
                return NotFound();
            }

            return View(chatRoom);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _chatRoomService.DeleteChatRoom(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
