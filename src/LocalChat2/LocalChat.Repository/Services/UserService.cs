using LocalChat.Core.Context;
using LocalChat.Core.Entities;
using LocalChat.Repository.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Repository.Services
{
    public class UserService : IUserService
    {
        private readonly ChatDbContext _dbContext;

        public UserService(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void AddUser(User user)
        {
            // Додати користувача до списку користувачів
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void UpdateUser(User updatedUser)
        {
            // Знайти і оновити користувача в списку
            _dbContext.Users.Update(updatedUser);
        }

        public void DeleteUser(Guid userId)
        {
            // Видалити користувача за його ідентифікатором
            _dbContext.Users.Find(userId);
        }

        public User GetUserById(Guid userId)
        {
            // Знайти користувача за його ідентифікатором
            return _dbContext.Users.Find(userId);
        }
        public string GetUserNameById(Guid? userId)
        {
            var usr = _dbContext.Users.Find(userId);
            // Знайти користувача за його ідентифікатором
            return usr.FullName;
        }

        public List<User> GetAllUsers()
        {
            // Отримати всіх користувачів
            return _dbContext.Users.ToList();
        }
        public bool UserExists(Guid id)
        {
            // Реалізація перевірки існування користувача за ідентифікатором
            return _dbContext.Users.Any(u => u.Id == id);
        }

		public User GetUserByEmail(string userEmail)
		{
            return _dbContext.Users.FirstOrDefault(u => u.Email == userEmail);
		}
	}
}
