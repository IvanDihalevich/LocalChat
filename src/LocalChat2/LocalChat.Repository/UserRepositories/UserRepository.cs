using LocalChat.Core.Context;
using LocalChat.Core.Entities;
using LocalChat.Repository.IEntity.Services;
using LocalChat.Repository.Model;
using LocalChat.Repository.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Repository.UserRepositories
{
    public class UserRepository : EntityService<User, Guid>, IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        public UserRepository(ChatDbContext ctx,
            UserManager<User> userManager,
            RoleManager<IdentityRole<Guid>> roleManager) : base(ctx)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<User> CreateWithPasswordAsync(UserCreateModel model)
        {
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                UserName = model.Email,
                Name = model.Name,
                EmailConfirmed = false,
                NormalizedUserName = model.Email.ToUpper(),
                NormalizedEmail = model.Email.ToUpper(),
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            return await _ctx.Users.FirstAsync(x => x.Email == model.Email);
        }
        public async Task<IEnumerable<UserListItemModel>> GetAllWithRolesAsync()
        {
            var list = new List<UserListItemModel>();

            foreach (var user in await _ctx.Users.ToListAsync())
            {
                var userModel = new UserListItemModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.Name,
                    Roles = new List<IdentityRole<Guid>>()
                };

                foreach (var role in await _userManager.GetRolesAsync(user))
                {
                    userModel.Roles.Add(await _ctx.Roles.FirstAsync(x => x.Name.ToLower() == role.ToLower()));
                }

                list.Add(userModel);
            }

            return list;
        }

        public async Task UpdateUserAsync(UserListItemModel model, string[] roles)
        {
            var user = _ctx.Users.Find(model.Id);

            if (user.Email != model.Email)
            {
                user.Email = model.Email;
                user.UserName = model.Email;
                user.NormalizedUserName = model.Email.ToUpper();
                user.NormalizedEmail = model.Email.ToUpper();
            }

            if (user.Name != model.FullName)
                user.Name = model.FullName;

            //var admRole = await _roleManager.FindByNameAsync("Admin");

            if ((await _userManager.GetRolesAsync(user)).Any())
            {
                await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
            }

            if (roles.Any())
            {
                await _userManager.AddToRolesAsync(user, roles.ToList());
            }
        }


        public async Task<UserListItemModel> GetOneWithRolesAsync(Guid id)
        {
            var user = await _ctx.Users.FirstAsync(x => x.Id == id);

            var userModel = new UserListItemModel
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.Name,
                Roles = new List<IdentityRole<Guid>>()
            };

            foreach (var role in await _userManager.GetRolesAsync(user))
            {
                userModel.Roles.Add(await _ctx.Roles.FirstAsync(x => x.Name.ToLower() == role.ToLower()));
            }

            return userModel;
        }

        public async Task<IEnumerable<IdentityRole<Guid>>> GetRolesAsync()
        {
            return await _ctx.Roles.ToListAsync();
        }

        public async Task<bool> CheckUser(Guid id)
        {
            var user = _ctx.Users.Find(id);
            var roles = await _userManager.GetRolesAsync(user);
            return roles.All(x => x != "Admin");
        }

        public async Task DeleteUser(Guid id)
        {
            var user = _ctx.Users.Find(id);

            if (await CheckUser(id))
            {
                if ((await _userManager.GetRolesAsync(user)).Any())
                {
                    await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
                }

                await _userManager.DeleteAsync(user);
            }
        }
    }
}
