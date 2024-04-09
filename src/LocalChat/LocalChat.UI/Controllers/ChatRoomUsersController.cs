using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using LocalChat.Core.Entities;
using LocalChat.Repository;

namespace LocalChat.WebUI.Controllers
{
    public class ChatRoomUsersController : Controller
    {
        private readonly IChatRoomUsersService _chatRoomUsersService;

        public ChatRoomUsersController(IChatRoomUsersService chatRoomUsersService)
        {
            _chatRoomUsersService = chatRoomUsersService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddUserToChatRoom(Guid userId, Guid chatRoomId)
        {
            try
            {
                _chatRoomUsersService.AddUserToChatRoom(userId, chatRoomId);
                return RedirectToAction("Index", "ChatRooms"); // Redirect to ChatRooms/Index
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Index", "ChatRooms"); // Redirect to ChatRooms/Index
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveUserFromChatRoom(Guid userId, Guid chatRoomId)
        {
            _chatRoomUsersService.RemoveUserFromChatRoom(userId, chatRoomId);
            return RedirectToAction("Index", "ChatRooms"); 
        }
    }
}
