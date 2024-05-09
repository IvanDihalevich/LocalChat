using LocalChat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Repository.Services
{
    public interface IMessageService
    {
        void SendMessage(Message message);
        Message GetMessageById(Guid messageId);
        List<Message> GetMessagesBySenderId(Guid senderId);
        List<Message> GetMessagesBySendTime(DateTime sendTime);
        void UpdateMessage(Message message);
        bool MessageExists(Guid id);
        Task AddMessageAsync(Message message);
        void DeleteMessage(Guid id);
        Task<IEnumerable<Message>> GetAll();
        Task<IEnumerable<Message>> GetAllByChatRoomId(Guid id);
    }
}
