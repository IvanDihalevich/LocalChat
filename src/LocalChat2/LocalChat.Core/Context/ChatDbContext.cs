using Microsoft.EntityFrameworkCore;
using LocalChat.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;

namespace LocalChat.Core.Context
{
    public class ChatDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options)
            : base(options)
        {

        }

        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessedgeUsers> MessedgeUsers { get; set; }
        public DbSet<ChatRoomUsers> ChatRoomUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Seed();

            // Вказуємо зовнішній ключ для відносини один до одного між ChatRoom і Message
            builder.Entity<ChatRoom>()
                .HasOne(c => c.Messages)        // ChatRoom має одне повідомлення
                .WithOne(m => m.ChatRoom)       // Повідомлення належить тільки одній кімнаті чату
                .HasForeignKey<Message>(m => m.ChatRoomId); // Зовнішній ключ у таблиці повідомлень

            base.OnModelCreating(builder);
        }
    }
}
