using LocalChat.Core.Entities;

namespace LocalChat.UI.Models
{
	public class DiscussionViewModel
	{
		public Post Post { get; set; }
		public IEnumerable<Comment> Comments { get; set; }
		public Comment NewComment { get; set; }
	}
}
