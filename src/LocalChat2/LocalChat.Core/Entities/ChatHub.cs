using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Core.Entities
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Guid senderId, string message, Guid chatRoomId)
        {
            await Clients.Group(chatRoomId.ToString()).SendAsync("ReceiveMessage", senderId, message);
        }

        public async Task JoinGroup(string chatRoomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatRoomId);
        }

        public async Task LeaveGroup(string chatRoomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatRoomId);
        }
    }
}
