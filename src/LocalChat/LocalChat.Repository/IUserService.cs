using LocalChat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Repository
{
    public interface IUserService
    {
        void AddUser(User user);
        void UpdateUser(User updatedUser);
        void DeleteUser(Guid userId);
        User GetUserById(Guid userId);
        List<User> GetAllUsers();
    }
}
