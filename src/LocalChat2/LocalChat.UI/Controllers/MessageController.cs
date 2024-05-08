using LocalChat.Core.Entities;
using LocalChat.Repository.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

public class MessageController : Controller
{
    private readonly IMessageService _messageService;

    public MessageController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    // Метод для відображення списку повідомлень
    public async Task<IActionResult> Index()
    {
        var messages = await _messageService.GetAll();
        return View(messages);
    }

    // Метод для відображення форми створення нового повідомлення
    public IActionResult Create()
    {
        var newMessage = new Message
        {
            SenderId = Guid.NewGuid(),  // Тут можна встановити реальний ідентифікатор відправника
            SendTime = DateTime.Now  // Час надсилання встановлюємо відразу
        };
        return View(newMessage);
    }

    // Метод для обробки даних після відправки форми
    [HttpPost]
    [ValidateAntiForgeryToken]  // Додано для захисту від CSRF
    public async Task<IActionResult> Create(Message message)
    {
        if (!ModelState.IsValid)
        {
            return View(message);  // Повертаємо форму, якщо є помилки валідації
        }

        message.Id = Guid.NewGuid();  // Генеруємо новий ідентифікатор
        message.SendTime = DateTime.Now;  // Встановлюємо час відправки

        await _messageService.AddMessageAsync(message);  // Зберігаємо повідомлення асинхронно

        return RedirectToAction("Index");  // Повертаємося до списку повідомлень після успішного створення
    }
}
