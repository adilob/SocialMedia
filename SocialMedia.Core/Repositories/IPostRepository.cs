using SocialMedia.Core.Entities;

namespace SocialMedia.Core.Repositories;

public interface IPostRepository : IRepository<Post>
{
	Task<IEnumerable<Post>> GetPostsByProfile(Guid profileId);
	Task<IEnumerable<Post>> GetFollowedProfilePosts(Guid profileId, int page, int pageSize);
}
