using SocialMedia.Application.Models.InputModel;
using SocialMedia.Application.Models.ViewModel;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Repositories;
using SocialMedia.Infrastructure.Auth;

namespace SocialMedia.Application.Services;

public class AccountService : IAccountService
{
	private readonly IAccountRepository _accountRepository;
	private readonly IAuthService _authService;

	public AccountService(IAccountRepository accountRepository, IAuthService authService)
	{
		_accountRepository = accountRepository;
		_authService = authService;
	}

	public async Task<ResultViewModel> ChangePasswordAsync(ChangeAccountPasswordInputModel inputModel)
	{
		var hash = await _authService.ComputeHashAsync(inputModel.CurrentPassword);
		var account = await _accountRepository.GetByEmailAndPasswordAsync(inputModel.Email, hash);

		if (account is null)
		{
			return ResultViewModel.Error("Account not found");
		}

		var currentPasswordHash = await _authService.ComputeHashAsync(inputModel.CurrentPassword);
		var newPasswordHash = await _authService.ComputeHashAsync(inputModel.NewPassword);

		if (currentPasswordHash == newPasswordHash)
		{
			return ResultViewModel.Error("New password must be a different one");
		}

		account.ChangePassword(newPasswordHash);
		await _accountRepository.UpdateAsync(account);

		return ResultViewModel.Success();
	}

	public async Task<ResultViewModel<AccountViewModel>> CreateAccountAsync(CreateAccountInputModel inputModel)
	{
		var hash = await _authService.ComputeHashAsync(inputModel.Password);
		var account = new Account
		(
			inputModel.Fullname,
			hash,
			inputModel.Email,
			inputModel.Birthdate,
			inputModel.Phone
		);
		
		await _accountRepository.AddAsync(account);

		return ResultViewModel<AccountViewModel>.Success(AccountViewModel.FromEntity(account));
	}

	public async Task DeleteAccountAsync(Guid accountId)
	{
		var account = await _accountRepository.GetByIdAsync(accountId);

		if (account is null)
		{
			await Task.CompletedTask;
		}

		await _accountRepository.DeleteAsync(account);
	}

	public async Task<ResultViewModel<AccountViewModel>> GetAccountByIdAsync(Guid accountId)
	{
		var account = await _accountRepository.GetByIdAsync(accountId);

		if (account is null)
		{
			return ResultViewModel<AccountViewModel>.Error("Account not found");
		}

		return ResultViewModel<AccountViewModel>.Success(AccountViewModel.FromEntity(account));
	}

	public async Task<ResultViewModel<LoginAccountViewModel>> LoginAsync(string email, string password)
	{
		var hash  = await _authService.ComputeHashAsync(password);
		var account = await _accountRepository.GetByEmailAndPasswordAsync(email, hash);

		if (account is null)
		{
			return ResultViewModel<LoginAccountViewModel>.Error("Invalid credentials");
		}

		var token = await _authService.GenerateTokenAsync(account.Email, "Admin", account.Id);
		return ResultViewModel<LoginAccountViewModel>.Success(new LoginAccountViewModel(token, "Admin"));
	}

	public async Task<ResultViewModel<AccountViewModel>> UpdateAccountAsync(Guid accountId, UpdateAccountInputModel inputModel)
	{
		var account = await _accountRepository.GetByIdAsync(accountId);

		if (account is null)
		{
			return ResultViewModel<AccountViewModel>.Error("Account not found");
		}

		account.Update(inputModel.Fullname, inputModel.Email, inputModel.Birthdate, inputModel.Phone);
		await _accountRepository.UpdateAsync(account);

		return ResultViewModel<AccountViewModel>.Success(AccountViewModel.FromEntity(account));
	}
}
