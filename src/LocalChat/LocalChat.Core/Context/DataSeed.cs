using LocalChat.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public static class DataSeed
{
    public static void Seed(this ModelBuilder builder)
    {
        var users = _seedUsers(builder);
        var chatRooms = _seedChatRooms(builder);
        //var messages = _seedMessages(builder, users, chatRooms);
        //var messageUsers = _seedMessageUsers(builder, messages, users);
        //var chatRoomUsers = _seedChatRoomUsers(builder, users, chatRooms);
    }

    private static List<User> _seedUsers(ModelBuilder builder)
    {
        var users = new List<User>
        {
            new User
            {
                Id = Guid.NewGuid(),
                Name = "user1@example.com",
                Password = "USER1@EXAMPLE.COM",
                Email = "user1@example.com",
            },
            new User
            {
                Id = Guid.NewGuid(),
                Name = "user2@example.com",
                Password = "USER2@EXAMPLE.COM",
                Email = "user2@example.com",
            }
            // Add more users as needed
        };

        builder.Entity<User>().HasData(users);
        return users;
    }

    private static List<ChatRoom> _seedChatRooms(ModelBuilder builder)
    {
        var chatRooms = new List<ChatRoom>
        {
            new ChatRoom
            {
                Id = Guid.NewGuid(),
                Name = "General"
            },
            new ChatRoom
            {
                Id = Guid.NewGuid(),
                Name = "Random"
            }
            // Add more chat rooms as needed
        };

        builder.Entity<ChatRoom>().HasData(chatRooms);
        return chatRooms;
    }

    //private static List<Message> _seedMessages(ModelBuilder builder, List<User> users, List<ChatRoom> chatRooms)
    //{
    //    var messages = new List<Message>
    //    {
    //        new Message
    //        {
    //            Id = Guid.NewGuid(),
    //            SenderId = users.First().Id,
    //            Text = "Hello, this is the first message!",
    //            SendTime = DateTime.Now,
    //            ChatRoomId = chatRooms.First().Id
    //        },
    //        new Message
    //        {
    //            Id = Guid.NewGuid(),
    //            SenderId = users.Last().Id,
    //            Text = "Hi, this is the second message!",
    //            SendTime = DateTime.Now,
    //            ChatRoomId = chatRooms.Last().Id
    //        }
    //        // Add more messages as needed
    //    };

    //    builder.Entity<Message>().HasData(messages);
    //    return messages;
    //}

    //private static List<MessedgeUsers> _seedMessageUsers(ModelBuilder builder, List<Message> messages, List<User> users)
    //{
    //    var messageUsers = new List<MessedgeUsers>();

    //    foreach (var message in messages)
    //    {
    //        var receiver = users.FirstOrDefault(); // Assuming first user for demonstration
    //        if (receiver != null)
    //        {
    //            var messageUser = new MessedgeUsers
    //            {
    //                Id = Guid.NewGuid(),
    //                MessageId = message.Id,
    //                ReceiverId = receiver.Id
    //            };

    //            messageUsers.Add(messageUser);
    //        }
    //    }

    //    builder.Entity<MessedgeUsers>().HasData(messageUsers);
    //    return messageUsers;
    //}

    //private static List<ChatRoomUsers> _seedChatRoomUsers(ModelBuilder builder, List<User> users, List<ChatRoom> chatRooms)
    //{
    //    var chatRoomUsers = new List<ChatRoomUsers>();

    //    foreach (var chatRoom in chatRooms)
    //    {
    //        foreach (var user in users)
    //        {
    //            var chatRoomUser = new ChatRoomUsers
    //            {
    //                Id = Guid.NewGuid(),
    //                ChatRoomId = chatRoom.Id,
    //                UserId = user.Id
    //            };

    //            chatRoomUsers.Add(chatRoomUser);
    //        }
    //    }

    //    builder.Entity<ChatRoomUsers>().HasData(chatRoomUsers);
    //    return chatRoomUsers;
    //}
}
