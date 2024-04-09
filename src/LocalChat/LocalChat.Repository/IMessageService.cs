using LocalChat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Repository
{
    public interface IMessageService
    {
        void SendMessage(Message message);
        Message GetMessageById(Guid messageId);
        List<Message> GetMessagesBySenderId(Guid senderId);
        List<Message> GetMessagesBySendTime(DateTime sendTime);
        void UpdateMessage(Message message);
        bool MessageExists(Guid id);
        void DeleteMessage(Guid id);
        IQueryable<Message> GetAllMessages();
    }
}
