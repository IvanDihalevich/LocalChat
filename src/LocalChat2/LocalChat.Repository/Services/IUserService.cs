using LocalChat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Repository.Services
{
    public interface IUserService
    {
        void AddUser(User user);
        void UpdateUser(User updatedUser);
        void DeleteUser(Guid userId);
        User GetUserById(Guid userId);
        string GetUserNameById(Guid? userId);

        List<User> GetAllUsers();
        bool UserExists(Guid id);
    }
}
