using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalChat.Repository.IEntity.Services;

namespace LocalChat.Repository
{
    public static class DependencyInjectionRepositories
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IEntityService<,>), typeof(EntityService<,>));
            services.AddScoped<IChatRoomService, ChatRoomService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
