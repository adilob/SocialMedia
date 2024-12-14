using SocialMedia.Core.Entities;
using SocialMedia.Core.ValueObjects;

namespace SocialMedia.Application.Models.ViewModel;

public class ProfileDetailsViewModel
{
	public ProfileDetailsViewModel(string exhibitionName, string about, string picture, Address address, string occupation, Account account)
	{
		ExhibitionName = exhibitionName;
		About = about;
		Picture = picture;
		Address = address?.FullAddress;
		Occupation = occupation;
		Account = AccountViewModel.FromEntity(account);
	}

	public string ExhibitionName { get; set; }
    public string About { get; set; }
    public string Picture { get; set; }
    public string Address { get; set; }
    public string Occupation { get; set; }
    public AccountViewModel Account { get; set; }

	public static ProfileDetailsViewModel FromEntity(Profile profile)
	{
		return new ProfileDetailsViewModel(
			profile.ExhibitionName, 
			profile.About, 
			profile.Picture,
			profile.Address,
			profile.Occupation, 
			profile.Account);
	}
}
