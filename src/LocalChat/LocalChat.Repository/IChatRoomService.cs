﻿using System;
using System.Collections.Generic;
using LocalChat.Core.Entities;

namespace LocalChat.Repository
{
    public interface IChatRoomService
    {
        void CreateChatRoom(ChatRoom chatRoom);
        ChatRoom GetChatRoomById(Guid chatRoomId);
        List<ChatRoom> GetAllChatRooms();
        void UpdateChatRoom(ChatRoom updatedChatRoom);
        void DeleteChatRoom(Guid chatRoomId);
    }
}
