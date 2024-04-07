using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Core.Entities
{
    public class Message: IEntity<Guid>
    {
        public Guid Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Text { get; set; }
        public DateTime SendTime { get; set; }
    }
}
