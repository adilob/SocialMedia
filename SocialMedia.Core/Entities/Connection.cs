namespace SocialMedia.Core.Entities;

public class Connection : Entity
{
	public Connection(Guid idFollower, Guid idFollowed, DateTime date)
	{
		IdFollower = idFollower;
		IdFollowed = idFollowed;
		Date = date;
	}

	public Guid IdFollower { get; private set; }
    public Guid IdFollowed { get; private set; }
    public DateTime Date { get; private set; }
    public Profile Follower { get; private set; }
	public Profile Followed { get; private set; }
}
