using LocalChat.Core.Entities;
using LocalChat.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Repository.Services
{
    public class MessageUserService : IMessageUserService
    {
        private Dictionary<Guid, List<Message>> _userMessages;

        public MessageUserService()
        {
            _userMessages = new Dictionary<Guid, List<Message>>();
        }

        public void AddMessageToUser(Guid userId, Message message)
        {
            if (!_userMessages.ContainsKey(userId))
            {
                _userMessages[userId] = new List<Message>();
            }

            _userMessages[userId].Add(message);
        }

        public void RemoveMessageFromUser(Guid userId, Guid messageId)
        {
            if (_userMessages.ContainsKey(userId))
            {
                _userMessages[userId].RemoveAll(m => m.Id == messageId);
            }
        }

        public List<Message> GetMessagesForUser(Guid userId)
        {
            if (_userMessages.ContainsKey(userId))
            {
                return _userMessages[userId];
            }
            else
            {
                return new List<Message>();
            }
        }
    }
}
