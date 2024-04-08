using LocalChat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Repository
{
    public interface IChatRoomUsersService
    {
        void AddUserToChatRoom(Guid userId, Guid chatRoomId);
        void RemoveUserFromChatRoom(Guid userId, Guid chatRoomId);
        List<User> GetUsersInChatRoom(Guid chatRoomId);
        List<ChatRoom> GetChatRoomsForUser(Guid userId);
    }
}
