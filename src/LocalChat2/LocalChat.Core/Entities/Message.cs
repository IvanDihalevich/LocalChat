using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LocalChat.Core.Entities
{
    public class Message : IEntity<Guid>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid SenderId { get; set; }
        public User? Sender { get; set; }
        public string Text { get; set; }
        public DateTime SendTime { get; set; } = DateTime.Now;
        public Guid? ChatRoomId { get; set; }
        public Guid? ReciverId { get; set; }

        public ChatRoom? ChatRoom { get; set; }
    }
}