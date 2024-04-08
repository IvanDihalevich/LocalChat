using LocalChat.Core.Entities;
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

        public MessageService()
        {
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
    }
}
