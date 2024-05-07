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
            var messages = await _dbContext.Messages
                .Include(m => m.SenderId)  // Завантаження пов'язаного відправника
                .Include(m => m.MessedgeUsersId)  // Завантаження пов'язаного одержувача
                .OrderByDescending(m => m.SendTime)  // Сортування за часом відправлення
                .ToListAsync();  // Асинхронне отримання списку

            return View(messages);  // Передача списку повідомлень у вигляд
        }

        // Метод для відображення сторінки створення нового повідомлення
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var currentUser = await _userManager.GetUserAsync(User);  // Отримання поточного користувача
            var users = _dbContext.Users
                .Where(u => u.Id != currentUser.Id)  // Відфільтрувати, щоб не включати поточного користувача
                .Select(u => new SelectListItem
                {
                    Text = u.FullName ?? u.UserName,  // Показувати повне ім'я або ім'я користувача
                    Value = u.Id.ToString()
                })
                .ToList();

            ViewBag.Users = users;  // Додавання списку користувачів у ViewBag

            return View(new Message { SenderId = currentUser.Id });  // Передача ідентифікатора відправника у вигляд
        }

        // Метод для обробки відправки форми
        [HttpPost]
        [ValidateAntiForgeryToken]  // Захист від CSRF
        public async Task<IActionResult> Create(Message message)
        {
            if (!ModelState.IsValid)  // Перевірка валідності даних
            {
                var currentUser = await _userManager.GetUserAsync(User);
                ViewBag.Users = _dbContext.Users
                    .Where(u => u.Id != currentUser.Id)
                    .Select(u => new SelectListItem
                    {
                        Text = u.FullName ?? u.UserName,
                        Value = u.Id.ToString()
                    })
                    .ToList();

                return View(message);  // Повернути форму з помилками валідації
            }

            message.Id = Guid.NewGuid();  // Генерація унікального ідентифікатора
            message.SendTime = DateTime.UtcNow;  // Встановлення часу відправлення

            _dbContext.Messages.Add(message);  // Додавання нового повідомлення до контексту
            await _dbContext.SaveChangesAsync();  // Збереження змін у базі даних

            return RedirectToAction("Index");  // Перенаправлення на список повідомлень
        }
    }
}
