using SocialMedia.Core.ValueObjects;

namespace SocialMedia.Core.Entities;

public class Profile : Entity
{
	public Profile(string exhibitionName, string about, string picture, Address address, string occupation, Guid accountId, bool isEnabled = true)
		: this(exhibitionName, about, picture, occupation, accountId, isEnabled)
	{
		Address = address;
	}

	public Profile(string exhibitionName, string about, string picture, string occupation, Guid accountId, bool isEnabled = true)
	{
		ExhibitionName = exhibitionName;
		About = about;
		Picture = picture;
		Occupation = occupation;
		AccountId = accountId;
		IsEnabled = isEnabled;
	}

	public string ExhibitionName { get; private set; }
    public string About { get; private set; }
    public string Picture { get; private set; }
	public Address Address { get; private set; }
	public string Occupation { get; private set; }
	public Guid AccountId { get; private set; }
	public Account Account { get; private set; }
    public List<Post> Posts { get; set; }
    public bool IsEnabled { get; set; } = true;
    public List<Profile> Followers { get; set; }
	public List<Profile> Followeds { get; set; }

    public void Update(string exhibitionName, string about, string picture, Address address, string occupation)
	{
		ExhibitionName = exhibitionName;
		About = about;
		Picture = picture;
		Address = address;
		Occupation = occupation;
		UpdatedAt = DateTime.Now;
	}

	public void Deactivate()
	{
		IsEnabled = false;
		UpdatedAt = DateTime.Now;
	}

	public void Reactivate()
	{
		IsEnabled = true;
		UpdatedAt = DateTime.Now;
	}
}
