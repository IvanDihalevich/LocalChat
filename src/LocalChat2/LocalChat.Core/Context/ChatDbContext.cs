using Microsoft.EntityFrameworkCore;
using LocalChat.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;


namespace LocalChat.Core.Context
{
    public class ChatDbContext :  IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<ChatRoom> ChatRooms => Set<ChatRoom>();
        public DbSet<Message> Messages => Set<Message>();
        public DbSet<MessedgeUsers> messedgeUsers => Set<MessedgeUsers>();
        public DbSet<ChatRoomUsers> ChatRoomUsers => Set<ChatRoomUsers>();

        public ChatDbContext(DbContextOptions<ChatDbContext> options)
         : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Seed();
            base.OnModelCreating(builder);



        }


    }
}
