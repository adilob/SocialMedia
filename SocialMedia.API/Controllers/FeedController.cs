using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Models.ViewModel;
using SocialMedia.Application.Services;

namespace SocialMedia.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FeedController : ControllerBase
	{
		private readonly IPostService _postService;

		public FeedController(IPostService postService)
		{
			_postService = postService;
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		[Route("{profileId}")]
		[ProducesResponseType(typeof(IEnumerable<PostViewModel>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetFollowedProfilePosts(Guid profileId, int page = 1, int pageSize = 10)
		{
			var response = await _postService.GetFollowedProfilePosts(profileId, page, pageSize);

			if (response.IsSuccess)
			{
				return Ok(response.Data);
			}

			return NotFound(response.Message);
		}
	}
}
