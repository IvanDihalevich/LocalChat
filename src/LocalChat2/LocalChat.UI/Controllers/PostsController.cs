using LocalChat.Core.Entities;
using LocalChat.Repository.Services;
using LocalChat.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LocalChat.UI.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly IPostReactionService _postReactionService;
        private readonly ICommentReactionService _commentReactionService;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<User> _userManager;

        public PostsController(
            IPostService postService,
            ICommentService commentService,
            IPostReactionService postReactionService,
            ICommentReactionService commentReactionService,
            IUserService userService,
            IWebHostEnvironment webHostEnvironment,
            UserManager<User> userManager)
        {
            _webHostEnvironment = webHostEnvironment;
            _commentService = commentService;
            _postService = postService;
            _postReactionService = postReactionService;
            _userService = userService;
            _commentReactionService = commentReactionService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAllPostsById();
            return View(posts);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Post newpost)
        {
            if(ModelState.IsValid)
            {
                if(newpost.Photo != null && newpost.Photo.Length > 0)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(newpost.Photo.FileName)}";
                    string filePath = Path.Combine(wwwRootPath, "img\\image\\", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await newpost.Photo.CopyToAsync(fileStream);
                    }
                    newpost.ImagePath = "/img/image/" + fileName;
                }
				await _postService.AddPostAsync(newpost);
			}
            return RedirectToAction("Index");
		}
		[HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

		public async Task<IActionResult> Discussion(Guid id)
		{
			var post = _postService.GetPostById(id);
			List<Comment> comments = (await _commentService.GetCommentsByPost(id)).ToList();
            if (comments is null)
            {
                comments = new List<Comment>();
            }
			var model = new DiscussionViewModel
			{
				Post = post,
				Comments = comments,
				NewComment = new Comment { PostId = id }
			};
            post.Likes = (await _postReactionService.GetReactionListAsync()).Where(r => r.PostId == id && r.Reaction == 1).Count();
            post.Dislikes = (await _postReactionService.GetReactionListAsync()).Where(r => r.PostId == id && r.Reaction == -1).Count();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> AddComment(DiscussionViewModel model)
		{
			if (model is not null)
			{
				model.NewComment.Id = Guid.NewGuid();
				model.NewComment.CreationTime = DateTime.Now;  // Assuming you have a CreateDateTime property
				model.NewComment.SenderId = _userService.GetUserByEmail(User.Identity.Name).Id;
				await _commentService.AddCommentAsync(model.NewComment);
				return RedirectToAction("Discussion", new { id = model.NewComment.PostId });
			}

			// If the model is invalid, reload the discussion view with the current model
			model.Post = _postService.GetPostById(model.NewComment.PostId.Value);
			model.Comments = await _commentService.GetCommentsByPost(model.NewComment.PostId.Value);
			return View("Discussion", model);
		}

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteComment(Guid commentId, Guid postId)
        {
            var comment = _commentService.GetCommentById(commentId);
            if (comment != null)
            {
                _commentService.DeleteComment(commentId);
            }
            return RedirectToAction("Discussion", new { id = postId });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> LikePost(Guid postId)
        {
            var user = _userService.GetUserByEmail(User.Identity.Name);
            var reaction = await _postReactionService.GetReactionByPostAndUser(postId, user.Id);

            if (reaction == null)
            {
                reaction = new PostReaction { PostId = postId, UserId = user.Id, Reaction = 1 };
                await _postReactionService.AddReactionAsync(reaction);
            }
            else
            {
                reaction.Reaction = reaction.Reaction == 1 ? 0 : 1;
                await _postReactionService.UpdateReactionAsync(reaction);
            }

            return RedirectToAction("Discussion", new { id = postId });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DislikePost(Guid postId)
        {
            var user = _userService.GetUserByEmail(User.Identity.Name);
            var reaction = await _postReactionService.GetReactionByPostAndUser(postId, user.Id);

            if (reaction == null)
            {
                reaction = new PostReaction { PostId = postId, UserId = user.Id, Reaction = -1 };
                await _postReactionService.AddReactionAsync(reaction);
            }
            else
            {
                reaction.Reaction = reaction.Reaction == -1 ? 0 : -1;
                await _postReactionService.UpdateReactionAsync(reaction);
            }

            return RedirectToAction("Discussion", new { id = postId });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> LikeComment(Guid commentId, Guid postId)
        {
            var user = _userService.GetUserByEmail(User.Identity.Name);
            var reaction = await _commentReactionService.GetReactionByCommentAndUser(commentId, user.Id);

            if (reaction == null)
            {
                reaction = new CommentReaction { CommentId = commentId, UserId = user.Id, Reaction = 1 };
                await _commentReactionService.AddReactionAsync(reaction);
            }
            else
            {
                reaction.Reaction = reaction.Reaction == 1 ? 0 : 1;
                await _commentReactionService.UpdateReactionAsync(reaction);
            }

            return RedirectToAction("Discussion", new { id = postId });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DislikeComment(Guid commentId, Guid postId)
        {
            var user = _userService.GetUserByEmail(User.Identity.Name);
            var reaction = await _commentReactionService.GetReactionByCommentAndUser(commentId, user.Id);

            if (reaction == null)
            {
                reaction = new CommentReaction { CommentId = commentId, UserId = user.Id, Reaction = -1 };
                await _commentReactionService.AddReactionAsync(reaction);
            }
            else
            {
                reaction.Reaction = reaction.Reaction == -1 ? 0 : -1;
                await _commentReactionService.UpdateReactionAsync(reaction);
            }

            return RedirectToAction("Discussion", new { id = postId });
        }
    }
}
