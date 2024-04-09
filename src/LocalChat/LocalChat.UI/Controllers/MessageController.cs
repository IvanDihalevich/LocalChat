using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using LocalChat.Core.Entities;
using LocalChat.Repository;

namespace LocalChat.WebUI.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public IActionResult Index()
        {
            var messages = _messageService.GetAllMessages();
            return View(messages);
        }

        public IActionResult Details(Guid id)
        {
            var message = _messageService.GetMessageById(id);
            return View(message);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Message message)
        {
            if (ModelState.IsValid)
            {
                _messageService.SendMessage(message);
                return RedirectToAction(nameof(Index));
            }
            return View(message);
        }

        public IActionResult Edit(Guid id)
        {
            var message = _messageService.GetMessageById(id);
            if (message == null)
            {
                return NotFound();
            }
            return View(message);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, Message message)
        {
            if (id != message.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _messageService.UpdateMessage(message);
                }
                catch (Exception)
                {
                    if (!_messageService.MessageExists(id))
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
            return View(message);
        }

        public IActionResult Delete(Guid id)
        {
            var message = _messageService.GetMessageById(id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _messageService.DeleteMessage(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
