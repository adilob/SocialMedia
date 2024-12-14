using SocialMedia.Application.Models.InputModel;
using SocialMedia.Application.Models.ViewModel;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Repositories;

namespace SocialMedia.Application.Services;

public class PostService : IPostService
{
	private readonly IPostRepository _postRepository;

	public PostService(IPostRepository postRepository)
	{
		_postRepository = postRepository;
	}

	public async Task<ResultViewModel<PostViewModel>> CreatePostAsync(CreatePostInputModel inputModel)
	{
		var post = new Post(
			inputModel.Content,
			inputModel.ProfileId);
		
		await _postRepository.AddAsync(post);

		return ResultViewModel<PostViewModel>.Success(PostViewModel.FromEntity(post));
	}

	public async Task<ResultViewModel> DeletePostAsync(Guid postId)
	{
		var post = await _postRepository.GetByIdAsync(postId);

		if (post is null)
		{
			return ResultViewModel.Error("Post not found");
		}

		await _postRepository.DeleteAsync(post);

		return ResultViewModel.Success();
	}

	public async Task<ResultViewModel<IEnumerable<PostViewModel>>> GetFollowedProfilePosts(Guid profileId, int page, int pageSize)
	{
		var posts = await _postRepository.GetFollowedProfilePosts(profileId, page, pageSize);

		if (posts is null || posts?.Any() == false)
		{
			return ResultViewModel<IEnumerable<PostViewModel>>.Error("Posts not found");
		}

		var postViewModelsWithProfile = posts.Select(post =>
		{
			var vm = new PostViewModel(post.Id, post.Content, post.ProfileId);
			vm.Profile = ProfileViewModel.FromEntity(post.Profile);
			return vm;
		});

		return ResultViewModel<IEnumerable<PostViewModel>>.Success(postViewModelsWithProfile);
	}

	public async Task<ResultViewModel<PostViewModel>> GetPostByIdAsync(Guid postId)
	{
		var post = await _postRepository.GetByIdAsync(postId);
		
		if (post is null)
		{
			return ResultViewModel<PostViewModel>.Error("Post not found");
		}

		var vm = PostViewModel.FromEntity(post);
		vm.Profile = ProfileViewModel.FromEntity(post.Profile);

		return ResultViewModel<PostViewModel>.Success(vm);
	}

	public async Task<ResultViewModel<IEnumerable<PostViewModel>>> GetPostsByProfileIdAsync(Guid profileId)
	{
		if (profileId == Guid.Empty)
		{
			return ResultViewModel<IEnumerable<PostViewModel>>.Error("ProfileId is empty");
		}

		var posts = await _postRepository.GetPostsByProfile(profileId);

		if (posts is null || posts?.Any() == false)
		{
			return ResultViewModel<IEnumerable<PostViewModel>>.Error("Posts not found");
		}

		var postViewModelsWithProfile = posts.Select(post =>
		{
			var vm = new PostViewModel(post.Id, post.Content, post.ProfileId);
			vm.Profile = ProfileViewModel.FromEntity(post.Profile);
			return vm;
		});

		return ResultViewModel<IEnumerable<PostViewModel>>.Success(postViewModelsWithProfile);
	}

	public async Task<ResultViewModel<PostViewModel>> UpdatePostAsync(Guid postId, UpdatePostInputModel inputModel)
	{
		var post = await _postRepository.GetByIdAsync(postId);

		if (post is null)
		{
			return ResultViewModel<PostViewModel>.Error("Post not found");
		}

		post.Update(inputModel.Content);

		await _postRepository.UpdateAsync(post);

		return ResultViewModel<PostViewModel>.Success(PostViewModel.FromEntity(post));
	}
}
