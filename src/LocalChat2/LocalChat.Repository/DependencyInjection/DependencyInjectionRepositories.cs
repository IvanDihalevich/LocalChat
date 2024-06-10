﻿using LocalChat.Core.Entities;
using LocalChat.Repository.IEntity.Services;
using LocalChat.Repository.Services;
using LocalChat.Repository.UserRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMessageService, MessageService>();
			services.AddScoped<IPostService, PostService>();
			services.AddScoped<ICommentService, CommentService>();
			services.AddScoped<ICommentReactionService, CommentReactionService>();
			services.AddScoped<IPostReactionService, PostReactionService>();

            services.AddScoped<RoleManager<IdentityRole<Guid>>>();
            

        }
    }
}
