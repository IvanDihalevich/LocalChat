using LocalChat.Core.Context;
using LocalChat.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Repository
{
    public class MessageService : IMessageService
    {
        private List<Message> _messages;
        private readonly ChatDbContext _dbContext;

        //public MessageService()
        //{
        //    _messages = new List<Message>();
        //}

        public MessageService(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
            _messages = new List<Message>();
        }


        public void SendMessage(Message message)
        {
            // Додаємо повідомлення до колекції
            _messages.Add(message);
        }

        public Message GetMessageById(Guid messageId)
        {
            // Пошук повідомлення за його ідентифікатором
            return _messages.Find(m => m.Id == messageId);
        }

        public List<Message> GetMessagesBySenderId(Guid senderId)
        {
            // Пошук повідомлень від заданого відправника
            return _messages.FindAll(m => m.SenderId.Id == senderId);
        }


        public List<Message> GetMessagesBySendTime(DateTime sendTime)
        {
            // Пошук повідомлень за часом надсилання
            return _messages.FindAll(m => m.SendTime == sendTime);
        }

        public IQueryable<Message> GetAllMessages()
        {
            return _dbContext.Messages;
        }

        public void UpdateMessage(Message message)
        {

            var existingMessage = _dbContext.Messages.FirstOrDefault(m => m.Id == message.Id);
            if (existingMessage != null)
            {
                existingMessage.Text = message.Text; 
                existingMessage.SendTime = message.SendTime;
                                                             

                _dbContext.SaveChanges();
            }
        }


        public bool MessageExists(Guid id)
        {
            // Реалізація перевірки існування повідомлення за ідентифікатором
            return _dbContext.Messages.Any(m => m.Id == id);
        }

        public void DeleteMessage(Guid id)
        {
            // Реалізація видалення повідомлення з бази даних
            var messageToDelete = _dbContext.Messages.FirstOrDefault(m => m.Id == id);
            if (messageToDelete != null)
            {
                _dbContext.Messages.Remove(messageToDelete);
                _dbContext.SaveChanges();
            }
        }
    }
}
