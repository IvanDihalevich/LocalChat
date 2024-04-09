using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using LocalChat.Core.Entities;
using LocalChat.Repository;

namespace LocalChat.WebUI.Controllers
{
    public class MessageUserController : Controller
    {
        private readonly IMessageUserService _messageUserService;

        public MessageUserController(IMessageUserService messageUserService)
        {
            _messageUserService = messageUserService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddMessageToUser(Guid userId, Message message)
        {
            _messageUserService.AddMessageToUser(userId, message);
            return RedirectToAction("Index", "Messages"); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveMessageFromUser(Guid userId, Guid messageId)
        {
            _messageUserService.RemoveMessageFromUser(userId, messageId);
            return RedirectToAction("Index", "Messages"); 
        }
    }
}
