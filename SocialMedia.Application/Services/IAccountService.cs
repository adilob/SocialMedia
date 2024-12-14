using SocialMedia.Application.Models.InputModel;
using SocialMedia.Application.Models.ViewModel;

namespace SocialMedia.Application.Services;

public interface IAccountService
{
	Task<ResultViewModel<AccountViewModel>> GetAccountByIdAsync(Guid accountId);
	Task<ResultViewModel<AccountViewModel>> CreateAccountAsync(CreateAccountInputModel inputModel);
	Task<ResultViewModel<AccountViewModel>> UpdateAccountAsync(Guid accountId, UpdateAccountInputModel inputModel);
	Task DeleteAccountAsync(Guid accountId);
	Task<ResultViewModel<LoginAccountViewModel>> LoginAsync(string email, string password);
	Task<ResultViewModel> ChangePasswordAsync(ChangeAccountPasswordInputModel inputModel);
}
