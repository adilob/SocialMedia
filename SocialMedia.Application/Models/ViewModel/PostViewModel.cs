using SocialMedia.Core.Entities;

namespace SocialMedia.Application.Models.ViewModel;

public class PostViewModel
{
	public PostViewModel(Guid id, string content, Guid profileId)
	{
		Id = id;
		Content = content;
		ProfileId = profileId;
	}

    public Guid Id { get; set; }
    public string Content { get; set; }
    public Guid ProfileId { get; set; }
    public ProfileViewModel Profile { get; set; }

	public static PostViewModel FromEntity(Post post) => new PostViewModel(post.Id, post.Content, post.ProfileId);
}
