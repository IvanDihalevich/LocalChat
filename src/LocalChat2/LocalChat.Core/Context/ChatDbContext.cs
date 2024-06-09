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
        public DbSet<ChatRoomUsers> ChatRoomUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Seed();

            // Вказуємо відношення один до багатьох між ChatRoom і Message
            builder.Entity<Message>()
                .HasOne(m => m.ChatRoom)       // Повідомлення належить одній кімнаті чату
                .WithMany(c => c.Messages)      // Кожна кімната чату може мати багато повідомлень
                .HasForeignKey(m => m.ChatRoomId); // Зовнішній ключ у таблиці повідомлень

            base.OnModelCreating(builder);
        }
    }
}
