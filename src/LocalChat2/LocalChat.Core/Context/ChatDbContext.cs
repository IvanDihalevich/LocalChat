using Microsoft.EntityFrameworkCore;
using LocalChat.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


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

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        IConfigurationRoot configuration = new ConfigurationBuilder()
        //            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        //            .AddJsonFile("C:\\Users\\Lenovo\\Documents\\GitHub\\LocalChat\\LocalChat\\src\\LocalChat\\src\\LocalChat\\LocalChat.UI\\appsettings.json")
        //            .Build();

        //        string connectionString = configuration.GetConnectionString("DefaultConnection");
        //        optionsBuilder.UseSqlServer(connectionString);
        //    }
        //}


    }
}
