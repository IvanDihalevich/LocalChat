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
        //[HttpGet]
        //public IActionResult Users()
        //{
        //    var users = _userService.GetAllUsers();
        //    return View(users);
        //}
        //public IActionResult Details(Guid id)
        //{
        //    var chatRoom = _chatRoomService.GetChatRoomById(id);
        //    return View(chatRoom);
        //}
        
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult SendMessage(Message message)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        message.SendTime = DateTime.Now; // Set current time
        //        _messageService.SendMessage(message);
        //    }
        //    return RedirectToAction(nameof(Index));
        //}
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

        //public IActionResult Edit(Guid id)
        //{
        //    var chatRoom = _chatRoomService.GetChatRoomById(id);
        //    if (chatRoom == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(chatRoom);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(Guid id, ChatRoom chatRoom)
        //{
        //    if (id != chatRoom.Id)
        //    {
        //        return BadRequest();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _chatRoomService.UpdateChatRoom(chatRoom);
        //        }
        //        catch (Exception)
        //        {
        //            if (!_chatRoomService.ChatRoomExists(id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(chatRoom);
        //}

        //public IActionResult Delete(Guid id)
        //{
        //    var chatRoom = _chatRoomService.GetChatRoomById(id);
        //    if (chatRoom == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(chatRoom);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeleteConfirmed(Guid id)
        //{
        //    _chatRoomService.DeleteChatRoom(id);
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
