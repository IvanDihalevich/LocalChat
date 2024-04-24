using LocalChat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Repository.Services
{
    public class ChatRoomUsersService : IChatRoomUsersService
    {
        private List<ChatRoomUsers> _chatRoomUsers;

        public ChatRoomUsersService()
        {
            _chatRoomUsers = new List<ChatRoomUsers>();
        }

        public void AddUserToChatRoom(Guid userId, Guid chatRoomId)
        {
            // Перевірка, чи користувач вже є в чат-кімнаті
            if (_chatRoomUsers.Exists(x => x.user.Id == userId && x.chatRoomId.Id == chatRoomId))
            {
                // Викидаємо виключення, якщо користувач вже є в чат-кімнаті
                throw new ArgumentException("User is already in the chat room.");
            }

            // Додаємо користувача до чат-кімнати
            _chatRoomUsers.Add(new ChatRoomUsers { Id = Guid.NewGuid(), user = new User { Id = userId }, chatRoomId = new ChatRoom { Id = chatRoomId } });
        }

        public void RemoveUserFromChatRoom(Guid userId, Guid chatRoomId)
        {
            // Видаляємо користувача з чат-кімнати
            _chatRoomUsers.RemoveAll(x => x.user.Id == userId && x.chatRoomId.Id == chatRoomId);
        }

        public List<User> GetUsersInChatRoom(Guid chatRoomId)
        {
            // Отримуємо всіх користувачів у заданій чат-кімнаті
            return _chatRoomUsers
                .Where(x => x.chatRoomId.Id == chatRoomId)
                .Select(x => x.user)
                .ToList();
        }

        public List<ChatRoom> GetChatRoomsForUser(Guid userId)
        {
            // Отримуємо всі чат-кімнати, у яких знаходиться заданий користувач
            return _chatRoomUsers
                .Where(x => x.user.Id == userId)
                .Select(x => x.chatRoomId)
                .Distinct()
                .ToList();
        }
    }
}
