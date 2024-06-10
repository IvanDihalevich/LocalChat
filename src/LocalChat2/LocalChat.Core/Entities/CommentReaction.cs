using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Core.Entities
{
	public class CommentReaction
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public Comment? Comment { get; set; }
		[ForeignKey(nameof(Comment))]
		public Guid? CommentId { get; set; }
		public User? User { get; set; }
		[ForeignKey(nameof(User))]
		public Guid? UserId { get; set; }
		//0 - none
		//1 - like
		//-1 - dislike
		public int Reaction { get; set; }
	}
}
