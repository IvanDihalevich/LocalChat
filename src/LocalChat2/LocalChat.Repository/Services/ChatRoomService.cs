using System;
using System.Collections.Generic;
using LocalChat.Core.Context;
using LocalChat.Core.Entities;
using LocalChat.Repository;
using Microsoft.EntityFrameworkCore;

namespace LocalChat.Repository.Services
{
    public class ChatRoomService : IChatRoomService
    {
        private readonly ChatDbContext _dbContext;

        public ChatRoomService(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateChatRoom(ChatRoom chatRoom)
        {
            _dbContext.ChatRooms.Add(chatRoom);
            _dbContext.SaveChanges();
        }

        public ChatRoom GetChatRoomById(Guid chatRoomId)
        {
            return _dbContext.ChatRooms.FirstOrDefault(p => p.Id == chatRoomId);
        }

        public IEnumerable<ChatRoom> GetAllChatRooms()
        {
            return _dbContext.ChatRooms.ToList();
        }

        public void UpdateChatRoom(ChatRoom updatedChatRoom)
        {
            var existingChatRoom = _dbContext.ChatRooms.Find(updatedChatRoom.Id);

            if (existingChatRoom != null)
            {
                existingChatRoom.Name = updatedChatRoom.Name;

                _dbContext.SaveChanges();
            }
        }

        public void DeleteChatRoom(Guid chatRoomId)
        {
            var chatRoomToDelete = _dbContext.ChatRooms.FirstOrDefault(p => p.Id == chatRoomId);

            if (chatRoomToDelete != null)
            {
                _dbContext.ChatRooms.Remove(chatRoomToDelete);
                _dbContext.SaveChanges();
            }
        }
        public bool ChatRoomExists(Guid id)
        {
            return _dbContext.ChatRooms.Any(c => c.Id == id);
        }
    }
}
