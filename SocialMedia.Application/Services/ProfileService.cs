using SocialMedia.Application.Models.InputModel;
using SocialMedia.Application.Models.ViewModel;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Repositories;

namespace SocialMedia.Application.Services;

public class ProfileService : IProfileService
{
	private readonly IProfileRepository _profileRepository;
	private readonly IAccountRepository _accountRepository;

    public ProfileService(IProfileRepository profileRepository, IAccountRepository accountRepository)
    {
        _profileRepository = profileRepository;
		_accountRepository = accountRepository;
    }

    public async Task<ResultViewModel<ProfileViewModel>> CreateProfileAsync(CreateProfileInputModel inputModel)
	{
		var account = await _accountRepository.GetByIdAsync(inputModel.AccountId);

		if (account is null)
		{
			return ResultViewModel<ProfileViewModel>.Error("Account not found");
		}

		var profile = new Profile(
			inputModel.ExhibitionName,
			inputModel.About,
			inputModel.Picture,
			inputModel.Address,
			inputModel.Occupation,
			inputModel.AccountId);

		await _profileRepository.AddAsync(profile);

		return ResultViewModel<ProfileViewModel>.Success(ProfileViewModel.FromEntity(profile));
	}

	public async Task<ResultViewModel> DeactivateProfileAsync(Guid profileId)
	{
		var profile = await _profileRepository.GetByIdAsync(profileId);	

		if (profile is null)
		{
			return ResultViewModel.Error("Profile not found");
		}

		profile.Deactivate();
		await _profileRepository.UpdateAsync(profile);

		return ResultViewModel.Success();
	}

	public async Task<ResultViewModel> ReactivateProfileAsync(Guid profileId)
	{
		var profile = await _profileRepository.GetByIdAsync(profileId);

		if (profile is null)
		{
			return ResultViewModel.Error("Profile not found");
		}

		profile.Reactivate();
		await _profileRepository.UpdateAsync(profile);

		return ResultViewModel.Success();
	}

	public async Task<ResultViewModel<IEnumerable<ProfileViewModel>>> GetProfileByAccountIdAsync(Guid accountId)
	{
		var account = await _accountRepository.GetByIdAsync(accountId);

		if (account is null)
		{
			return ResultViewModel<IEnumerable<ProfileViewModel>>.Error("Account not found");
		}

		var profiles = await _profileRepository.GetByAccountIdAsync(accountId);

		if (profiles is null)
		{
			return ResultViewModel<IEnumerable<ProfileViewModel>>.Error("Profiles not found");
		}

		var profilesViewModel = profiles.Select(ProfileViewModel.FromEntity);

		return ResultViewModel<IEnumerable<ProfileViewModel>>.Success(profilesViewModel);
	}

	public async Task<ResultViewModel<ProfileViewModel>> GetProfileByIdAsync(Guid profileId)
	{
		var profile = await _profileRepository.GetByIdAsync(profileId);

		if (profile is null)
		{
			return ResultViewModel<ProfileViewModel>.Error("Profile not found");
		}

		return ResultViewModel<ProfileViewModel>.Success(ProfileViewModel.FromEntity(profile));
	}

	public async Task<ResultViewModel<ProfileViewModel>> UpdateProfileAsync(Guid profileId, UpdateProfileInputModel inputModel)
	{
		var profile = await _profileRepository.GetByIdAsync(profileId);

		if (profile is null)
		{
			return ResultViewModel<ProfileViewModel>.Error("Profile not found");
		}

		profile.Update(
			inputModel.ExhibitionName,
			inputModel.About,
			inputModel.Picture,
			inputModel.Address,
			inputModel.Occupation);

		await _profileRepository.UpdateAsync(profile);

		return ResultViewModel<ProfileViewModel>.Success(ProfileViewModel.FromEntity(profile));
	}
}
