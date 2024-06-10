using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Core.Entities
{
    public class Post : IEntity<Guid>
    {
        public Guid Id { get; set; }   = Guid.NewGuid();
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
		public User? Sender { get; set; }
        
        [ForeignKey(nameof(Sender))]
		public Guid? SenderId { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public int Likes { get; set; }
        public string? ImagePath { get; set; } = "img/image/no_photo.jpg";
		[NotMapped]
		public IFormFile? Photo { get; set; }
        public virtual ICollection<PostReaction> PostReactions { get; set; }
        public virtual ICollection<Comment> Comments{ get; set; }
        [NotMapped]
        public int Dislikes { get; set; }
	}
}
