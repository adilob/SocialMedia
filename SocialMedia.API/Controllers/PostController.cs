using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Models.InputModel;
using SocialMedia.Application.Models.ViewModel;
using SocialMedia.Application.Services;

namespace SocialMedia.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class PostController : ControllerBase
	{
		private readonly IPostService _postService;

		public PostController(IPostService postService)
		{
			_postService = postService;
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
		public async Task<IActionResult> Create([FromBody] CreatePostInputModel inputModel)
		{
			var response = await _postService.CreatePostAsync(inputModel);

			if (response.IsSuccess)
			{
				return CreatedAtAction(nameof(GetById), new { postId = response.Data.Id }, response.Data);
			}

			return BadRequest(response.Message);
		}

		[HttpPut]
		[Authorize(Roles = "Admin")]
		[Route("{postId}")]
		[ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
		public async Task<IActionResult> Update([FromRoute] Guid postId, [FromBody] UpdatePostInputModel inputModel)
		{
			var response = await _postService.UpdatePostAsync(postId, inputModel);

			if (response.IsSuccess)
			{
				return NoContent();
			}

			return BadRequest(response.Message);
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		[Route("{postId}")]
		[ProducesResponseType(typeof(PostViewModel), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetById([FromRoute] Guid postId)
		{
			var response = await _postService.GetPostByIdAsync(postId);

			if (response.IsSuccess)
			{
				return Ok(response.Data);
			}

			return NotFound(response.Message);
		}

		[HttpDelete]
		[Authorize(Roles = "Admin")]
		[Route("{postId}")]
		[ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
		public async Task<IActionResult> Delete([FromRoute] Guid postId)
		{
			var response = await _postService.DeletePostAsync(postId);

			if (response.IsSuccess)
			{
				return NoContent();
			}

			return BadRequest(response.Message);
		}
	}
}
