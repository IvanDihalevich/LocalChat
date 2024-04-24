﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Core.Entities
{
    public class Message : IEntity<Guid>
    {

        public Guid Id { get; set; }
        public User SenderId { get; set; }
        public string Text { get; set; }
        public DateTime SendTime { get; set; } = DateTime.Now;
    }
}
