using LocalChat.Core.Entities;
using LocalChat.Repository.IEntity.Services;
using LocalChat.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Repository.UserRepositories
{
    public interface IUserRepository : IEntityService<User, Guid>
    {
        
    }
}
