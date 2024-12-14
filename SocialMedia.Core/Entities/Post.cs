namespace SocialMedia.Core.Entities;

public class Post : Entity
{
	public Post(string content, Guid profileId)
	{
		Content = content;
		PostDate = DateTime.Now;
		Likes = 0;
		ProfileId = profileId;
	}

	public string Content { get; private set; }
    public DateTime PostDate { get; private set; }
    public int Likes { get; private set; }
    public Guid ProfileId { get; set; }
    public Profile Profile { get; set; }

	public void Update(string content)
	{
		Content = content;
		UpdatedAt = DateTime.Now;
	}

	public void Like()
	{
		Likes++;
	}

	public void Unlike()
	{
		Likes--;
	}
}
