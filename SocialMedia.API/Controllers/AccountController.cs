using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Models.InputModel;
using SocialMedia.Application.Models.ViewModel;
using SocialMedia.Application.Services;
using SocialMedia.Infrastructure.Extensions;

namespace SocialMedia.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
	private readonly IAccountService _accountService;
	private readonly IProfileService _profileService;

    public AccountController(IAccountService accountService, IProfileService profileService)
    {
		_accountService = accountService;
		_profileService = profileService;
    }

    [HttpPost]
	[AllowAnonymous]
	[ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
	public async Task<IActionResult> Create([FromBody] CreateAccountInputModel inputModel)
	{
		var response = await _accountService.CreateAccountAsync(inputModel);

		if (response.IsSuccess)
		{
			return CreatedAtAction(nameof(GetById), new { accountId = response.Data.Id }, response.Data);
		}

		return BadRequest(response.Message);
	}

	[HttpGet]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(AccountViewModel), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetById()
	{
		var accountId = HttpContext.User.GetAccountId();
		var response = await _accountService.GetAccountByIdAsync(accountId);

		if (response.IsSuccess)
		{
			return Ok(response.Data);
		}

		return NotFound(response.Message);
	}

	[HttpPut]
	[Authorize(Roles = "Admin")]
	[Route("ChangePassword")]
	[ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
	public async Task<IActionResult> ChangePassword([FromBody] ChangeAccountPasswordInputModel inputModel)
	{
		var response = await _accountService.ChangePasswordAsync(inputModel);

		if (response.IsSuccess)
		{
			return NoContent();
		}

		return BadRequest(response.Message);
	}

	[HttpPut]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
	public async Task<IActionResult> Update([FromBody] UpdateAccountInputModel inputModel)
	{
		var accountId = HttpContext.User.GetAccountId();
		var response = await _accountService.UpdateAccountAsync(accountId, inputModel);

		if (response.IsSuccess)
		{
			return NoContent();
		}

		return BadRequest(response.Message);
	}

	[HttpPost]
	[AllowAnonymous]
	[Route("Login")]
	[ProducesResponseType(typeof(LoginAccountViewModel), StatusCodes.Status200OK)]
	public async Task<IActionResult> Login([FromBody] LoginAccountInputModel inputModel)
	{
		var response = await _accountService.LoginAsync(inputModel.Email, inputModel.Password);

		if (response.IsSuccess)
		{
			return Ok(response.Data);
		}

		return Unauthorized(response.Message);
	}

	[HttpGet]
	[Authorize(Roles = "Admin")]
	[Route("Profiles")]
	[ProducesResponseType(typeof(IEnumerable<ProfileViewModel>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetProfiles()
	{
		var accountId = HttpContext.User.GetAccountId();
		var response = await _profileService.GetProfileByAccountIdAsync(accountId);

		if (response.IsSuccess)
		{
			return Ok(response.Data);
		}

		return NotFound(response.Message);
	}
}
