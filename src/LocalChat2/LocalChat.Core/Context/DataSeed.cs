using LocalChat.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


namespace LocalChat.Core.Context
{
    public static class DataSeed
    {
        public static void Seed(this ModelBuilder builder)
        {
            var (adminRoleId, userRoleId) = SeedRoles(builder);

            var userIds = SeedUsers(builder, adminRoleId, userRoleId);

            var chatRoomId = SeedChatRooms(builder);

            //SeedMessages(builder, chatRoomId, userIds);

            //SeedChatRoomUsers(builder, chatRoomId, userIds);
        }

        private static (Guid, Guid) SeedRoles(ModelBuilder builder)
        {
            var ADMIN_ROLE_ID = Guid.NewGuid();
            var USER_ROLE_ID = Guid.NewGuid();

            builder.Entity<IdentityRole<Guid>>()
                .HasData(
                    new IdentityRole<Guid>
                    {
                        Id = ADMIN_ROLE_ID,
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                        ConcurrencyStamp = ADMIN_ROLE_ID.ToString()
                    },
                    new IdentityRole<Guid>
                    {
                        Id = USER_ROLE_ID,
                        Name = "User",
                        NormalizedName = "USER",
                        ConcurrencyStamp = USER_ROLE_ID.ToString()
                    }
                );

            return (ADMIN_ROLE_ID, USER_ROLE_ID);
        }

        private static List<Guid> SeedUsers(ModelBuilder builder, Guid adminRoleId, Guid userRoleId)
        {
            var adminUserId = Guid.NewGuid();
            var regularUserId = Guid.NewGuid();

            var adminUser = new User
            {
                Id = adminUserId,
                UserName = "admin@localchat.example",
                NormalizedUserName = "admin@localchat.example".ToUpper(),
                Email = "admin@localchat.example",
                NormalizedEmail = "admin@localchat.example".ToUpper(),
                //PhoneNumber = "0661430681",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                FullName = "Admin User"
            };

            var regularUser = new User
            {
                Id = regularUserId,
                UserName = "user@localchat.example",
                NormalizedUserName = "user@localchat.example".ToUpper(),
                Email = "user@localchat.example",
                NormalizedEmail = "user@localchat.example".ToUpper(),
                //PhoneNumber = "0661430681",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                FullName = "Regular User"
            };

            var passwordHasher = new PasswordHasher<User>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "AdminPass123!");
            regularUser.PasswordHash = passwordHasher.HashPassword(regularUser, "UserPass123!");

            builder.Entity<User>().HasData(adminUser, regularUser);

            builder.Entity<IdentityUserRole<Guid>>()
                .HasData(
                    new IdentityUserRole<Guid>
                    {
                        RoleId = adminRoleId,
                        UserId = adminUserId
                    },
                    new IdentityUserRole<Guid>
                    {
                        RoleId = userRoleId,
                        UserId = regularUserId
                    }
                );

            return new List<Guid> { adminUserId, regularUserId };
        }

        private static Guid SeedChatRooms(ModelBuilder builder)
        {
            var chatRoomId = Guid.NewGuid();

            builder.Entity<ChatRoom>()
                .HasData(
                    new ChatRoom
                    {
                        Id = chatRoomId,
                        Name = "General"
                    }
                );

            return chatRoomId;
        }
        //private static void SeedMessages(ModelBuilder builder, Guid chatRoomId, List<Guid> userIds)
        //{
        //    var adminUserId = userIds[0]; // Припускаючи, що перший елемент - це Admin
        //    var regularUserId = userIds[1]; // А другий - Regular User

        //    var message1 = new Message
        //    {
        //        Id = Guid.NewGuid(),
        //        SenderId = adminUserId,
        //        Text = "Welcome to the chat room!",
        //        SendTime = DateTime.Now
        //    };

        //    var message2 = new Message
        //    {
        //        Id = Guid.NewGuid(),
        //        SenderId = regularUserId,
        //        Text = "Thank you! Glad to be here.",
        //        SendTime = DateTime.Now.AddMinutes(5) // Час зрушений для різноманітності
        //    };

        //    builder.Entity<Message>().HasData(message1, message2);
        //}
    }
}
