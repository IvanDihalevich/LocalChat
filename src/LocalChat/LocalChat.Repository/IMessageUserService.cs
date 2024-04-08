using LocalChat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Repository
{
    public interface IMessageUserService
    {
        void AddMessageToUser(Guid userId, Message message);
        void RemoveMessageFromUser(Guid userId, Guid messageId);
        List<Message> GetMessagesForUser(Guid userId);
    }
}
