using SocialMedia.Core.Entities;
using SocialMedia.Core.ValueObjects;

namespace SocialMedia.Application.Models.ViewModel;

public class ProfileViewModel
{
	public ProfileViewModel(Guid id, string exhibitionName, string about, string picture, Address address, string occupation, bool isEnabled)
	{
		Id = id;
		ExhibitionName = exhibitionName;
		About = about;
		Picture = picture;
		Address = address?.FullAddress;
		Occupation = occupation;
		IsEnabled = isEnabled;
	}

	public Guid Id { get; set; }
	public string ExhibitionName { get; set; }
	public string About { get; set; }
	public string Picture { get; set; }
	public string Address { get; set; }
	public string Occupation { get; set; }
    public bool IsEnabled { get; set; }

    public static ProfileViewModel FromEntity(Profile profile) =>
		new ProfileViewModel(
			profile.Id,
			profile.ExhibitionName,
			profile.About,
			profile.Picture,
			profile.Address,
			profile.Occupation,
			profile.IsEnabled);
}
