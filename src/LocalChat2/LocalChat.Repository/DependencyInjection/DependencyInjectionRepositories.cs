﻿using LocalChat.Repository.IEntity.Services;
using LocalChat.Repository.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Repository.DependencyInjection
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