using LocalChat.Core.Context;
using LocalChat.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Repository
{
    public class UserService : IUserService
    {
        private List<User> _users;
        private readonly ChatDbContext _dbContext;

        public UserService(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
            _users = new List<User>();
        }


        public void AddUser(User user)
        {
            // Додати користувача до списку користувачів
            _users.Add(user);
        }

        public void UpdateUser(User updatedUser)
        {
            // Знайти і оновити користувача в списку
            int index = _users.FindIndex(u => u.Id == updatedUser.Id);
            if (index != -1)
            {
                _users[index] = updatedUser;
            }
        }

        public void DeleteUser(Guid userId)
        {
            // Видалити користувача за його ідентифікатором
            _users.RemoveAll(u => u.Id == userId);
        }

        public User GetUserById(Guid userId)
        {
            // Знайти користувача за його ідентифікатором
            return _users.Find(u => u.Id == userId);
        }

        public List<User> GetAllUsers()
        {
            // Отримати всіх користувачів
            return _users;
        }
        public bool UserExists(Guid id)
        {
            // Реалізація перевірки існування користувача за ідентифікатором
            return _dbContext.Users.Any(u => u.Id == id);
        }
    }
}
