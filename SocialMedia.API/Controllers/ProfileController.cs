using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Models.InputModel;
using SocialMedia.Application.Models.ViewModel;
using SocialMedia.Application.Services;

namespace SocialMedia.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfileController : ControllerBase
{
	private readonly IProfileService _profileService;
	private readonly IPostService _postService;

	public ProfileController(IProfileService profileService, IPostService postService)
	{
		_profileService = profileService;
		_postService = postService;
	}

	[HttpPost]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
	public async Task<IActionResult> Create([FromBody] CreateProfileInputModel inputModel)
	{
		var response = await _profileService.CreateProfileAsync(inputModel);

		if (response.IsSuccess)
		{
			return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response.Data);
		}

		return BadRequest(response.Message);
	}

	[HttpPut]
	[Authorize(Roles = "Admin")]
	[Route("{profileId}")]
	[ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
	public async Task<IActionResult> Update([FromRoute] Guid profileId, [FromBody] UpdateProfileInputModel inputModel)
	{
		var response = await _profileService.UpdateProfileAsync(profileId, inputModel);

		if (response.IsSuccess)
		{
			return NoContent();
		}

		return BadRequest(response.Message);
	}

	[HttpGet]
	[Authorize(Roles = "Admin")]
	[Route("{profileId}")]
	[ProducesResponseType(typeof(ProfileDetailsViewModel), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetById(Guid profileId)
	{
		var response = await _profileService.GetProfileByIdAsync(profileId);

		if (response.IsSuccess)
		{
			return Ok(response.Data);
		}

		return NotFound(response.Message);
	}

	[HttpGet]
	[Authorize(Roles = "Admin")]
	[Route("{profileId}/Posts")]
	[ProducesResponseType(typeof(IEnumerable<PostViewModel>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetPosts(Guid profileId)
	{
		var response = await _postService.GetPostsByProfileIdAsync(profileId);

		if (response.IsSuccess)
		{
			return Ok(response.Data);
		}

		return NotFound(response.Message);
	}

	[HttpDelete]
	[Authorize(Roles = "Admin")]
	[Route("{profileId}/deactivate")]
	[ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
	public async Task<IActionResult> Deactivate(Guid profileId)
	{
		var response = await _profileService.DeactivateProfileAsync(profileId);

		if (response.IsSuccess)
		{
			return NoContent();
		}

		return BadRequest(response.Message);
	}

	[HttpPut]
	[Authorize(Roles = "Admin")]
	[Route("{profileId}/reactivate")]
	[ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
	public async Task<IActionResult> Reactivate(Guid profileId)
	{
		var response = await _profileService.ReactivateProfileAsync(profileId);

		if (response.IsSuccess)
		{
			return NoContent();
		}

		return BadRequest(response.Message);
	}
}
