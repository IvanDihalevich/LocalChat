using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Core.Entities
{
    public class User : IdentityUser<Guid>, IEntity<Guid>
    {
        public string? FullName { get; set; } 
    }
}
