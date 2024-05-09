using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalChat.Core.Context;
using LocalChat.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LocalChat.Repository.Services
{
    public class MessageService : IMessageService
    {
        private readonly ChatDbContext _dbContext;

        public MessageService(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

    
        public async Task AddMessageAsync(Message message)
        {
            var lastmassage = await _dbContext.Messages.OrderByDescending(p => p.Id).FirstOrDefaultAsync();

            if (lastmassage != null)
            {

                message.Id = Guid.NewGuid();
            }
            else
            {
                message.Id = Guid.NewGuid();
            }

            await _dbContext.Messages.AddAsync(message);
            await _dbContext.SaveChangesAsync();
        }
        public void SendMessage(Message message)
        {
            _dbContext.Messages.Add(message);
            _dbContext.SaveChanges();
        }

        public Message GetMessageById(Guid messageId)
        {
            return _dbContext.Messages.FirstOrDefault(m => m.Id == messageId);
        }

        public async Task<IEnumerable<Message>> GetAllByChatRoomId(Guid id)
        {
            return await _dbContext.Messages.Include(m => m.Sender)
               .Where(m => m.ChatRoomId == id)
               .ToListAsync();
        }

        public List<Message> GetMessagesBySenderId(Guid senderId)
        {
            return _dbContext.Messages.Where(m => m.SenderId == senderId).ToList();
        }

        public List<Message> GetMessagesBySendTime(DateTime sendTime)
        {
            return _dbContext.Messages.Where(m => m.SendTime == sendTime).ToList();
        }

        public async Task<IEnumerable<Message>> GetAll()
        {
            return await _dbContext.Messages.ToListAsync();
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
            return _dbContext.Messages.Any(m => m.Id == id);
        }

        public void DeleteMessage(Guid id)
        {
            var messageToDelete = _dbContext.Messages.FirstOrDefault(m => m.Id == id);
            if (messageToDelete != null)
            {
                _dbContext.Messages.Remove(messageToDelete);
                _dbContext.SaveChanges();
            }
        }

    }
}