using Microsoft.EntityFrameworkCore;
using LocalChat.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;


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

            // Встановлення відношення між `Message` і `User`
            builder.Entity<Message>()
                .HasOne(m => m.Sender)  // Відношення один до одного
                .WithMany()  // Користувач може мати багато повідомлень
                .HasForeignKey(m => m.SenderId)  // Вказуємо, що `SenderId` - це зовнішній ключ
                .OnDelete(DeleteBehavior.Restrict);  // Опція на випадок видалення користувача


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
