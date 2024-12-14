using SocialMedia.Application.Models.InputModel;
using SocialMedia.Application.Models.ViewModel;

namespace SocialMedia.Application.Services;

public interface IProfileService
{
	Task<ResultViewModel<ProfileViewModel>> GetProfileByIdAsync(Guid profileId);
	Task<ResultViewModel<IEnumerable<ProfileViewModel>>> GetProfileByAccountIdAsync(Guid accountId);
	Task<ResultViewModel<ProfileViewModel>> CreateProfileAsync(CreateProfileInputModel inputModel);
	Task<ResultViewModel<ProfileViewModel>> UpdateProfileAsync(Guid profileId, UpdateProfileInputModel inputModel);
	Task<ResultViewModel> DeactivateProfileAsync(Guid profileId);
	Task<ResultViewModel> ReactivateProfileAsync(Guid profileId);
}
