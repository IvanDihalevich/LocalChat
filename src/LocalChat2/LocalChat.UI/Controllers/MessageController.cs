﻿using Microsoft.AspNetCore.Mvc;
using LocalChat.Core.Context;
using LocalChat.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LocalChat.Repository.Services;
using System.Security.Claims;

namespace LocalChat.UI.Controllers
{
    [Authorize]  // Require authentication
    public class MessageController : Controller
    {
        private readonly ChatDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly IMessageService _messageService;

        public MessageController(ChatDbContext dbContext, UserManager<User> userManager, IMessageService messageService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var messages = await _messageService.GetAll();
            return View(messages);
        }

        [HttpGet]
        public IActionResult Create(Guid id)
        {
            return View(new Message() { ChatRoomId = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Message model)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Messages.Add(model);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }



    }
}