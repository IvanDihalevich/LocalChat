using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Core.Entities
{
	public class Comment : IEntity<Guid>
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public DateTime CreationTime { get; set; }
		public string Content { get; set; }
		public User? Sender {  get; set; }
		[ForeignKey(nameof(Sender))]
		public Guid? SenderId { get; set; }
        public Post? Post { get; set; }
		[ForeignKey(nameof(Post))]
		public Guid? PostId { get; set; }
		public virtual ICollection<CommentReaction> CommentReactions{ get; set; } = new List<CommentReaction>();
		[NotMapped]
		public int? Likes { get; set; }
		[NotMapped]
		public int? Dislikes { get; set; }
	}
}
