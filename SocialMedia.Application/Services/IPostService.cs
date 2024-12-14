using SocialMedia.Application.Models.InputModel;
using SocialMedia.Application.Models.ViewModel;

namespace SocialMedia.Application.Services;

public interface IPostService
{
	Task<ResultViewModel<PostViewModel>> GetPostByIdAsync(Guid postId);
	Task<ResultViewModel<IEnumerable<PostViewModel>>> GetPostsByProfileIdAsync(Guid profileId);
	Task<ResultViewModel<PostViewModel>> CreatePostAsync(CreatePostInputModel inputModel);
	Task<ResultViewModel<PostViewModel>> UpdatePostAsync(Guid postId, UpdatePostInputModel inputModel);
	Task<ResultViewModel> DeletePostAsync(Guid postId);
	Task<ResultViewModel<IEnumerable<PostViewModel>>> GetFollowedProfilePosts(Guid profileId, int page, int pageSize);
}
