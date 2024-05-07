using Microsoft.AspNetCore.Mvc;
using LocalChat.Core.Context;
using LocalChat.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LocalChat.UI.Controllers
{
    [Authorize]  // Вимога аутентифікації
    public class MessageController : Controller
    {
        private readonly ChatDbContext _dbContext;
        private readonly UserManager<User> _userManager;



        public MessageController(ChatDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        // Метод для відображення списку повідомлень
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Отримання всіх повідомлень із завантаженням пов'язаних даних
            //var messages = await _dbContext.Messages
            //    .Include(m => m.SenderId)  // Завантаження пов'язаного відправника
            //    .Include(m => m.MessedgeUsersId)  // Завантаження пов'язаного одержувача
            //    .OrderByDescending(m => m.SendTime)  // Сортування за часом відправлення
            //    .ToListAsync();  // Асинхронне отримання списку

            //return View(messages);  // Передача списку повідомлень у вигляд
            return View();
        }

        // Метод для відображення сторінки створення нового повідомлення
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();  // Передача ідентифікатора відправника у вигляд
        }
        [HttpPost]
        public async Task<IActionResult> Create(Message message)
        {
            return View(new Message { Text = message.Text });  // Передача ідентифікатора відправника у вигляд
        }

        //[HttpPost, ActionName("Create")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([FromBody] Message message)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            message.SendTime = DateTime.UtcNow;
        //            _messages.Add(message);
        //            _dbContext.Messages.Add(message);
        //            await _dbContext.SaveChangesAsync();
        //            return Ok("Повідомлення успішно відправлено");
        //        }
        //        else
        //        {
        //            return BadRequest("Недійсні дані повідомлення");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "Помилка під час відправлення повідомлення: " + ex.Message);
        //    }
        //    return View(nameof(Index));
        //}

    }
}
