using LocalChat.Core.Entities;
using LocalChat.Repository.Services;
using Microsoft.AspNetCore.Mvc;

public class MessageController : Controller
{
    private readonly IMessageService _messageService;

    public MessageController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    public async Task<IActionResult> Index()
    {
        var messages = await _messageService.GetAll();
        return View(messages);
    }

    public IActionResult Create()
    {
        var newMessage = new Message
        {
            SenderId = Guid.NewGuid(),  // Тут можна встановити реальний ідентифікатор відправника
            SendTime = DateTime.Now  // Час надсилання встановлюємо відразу
        };
        return View(newMessage);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]  // Захист від CSRF
    public async Task<IActionResult> Create(Message message)
    {
        if (!ModelState.IsValid)  // Перевірка валідації даних
        {
            return View(message);
        }

        try
        {
            message.Id = Guid.NewGuid();  // Генерація нового ідентифікатора
            message.SendTime = DateTime.Now;  // Час надсилання

            await _messageService.AddMessageAsync(message);  // Додавання повідомлення асинхронно

            TempData["SuccessMessage"] = "Повідомлення успішно створено.";  // Повідомлення про успіх
            return RedirectToAction("Index");  // Повернення до списку повідомлень
        }
        catch (Exception ex)  // Обробка винятків
        {
            ModelState.AddModelError("", "Сталася помилка при збереженні повідомлення. Спробуйте ще раз.");
            return View(message);  // Повернення до форми з помилками
        }
    }
}
