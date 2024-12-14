namespace SocialMedia.Application.Models.ViewModel;

public class LoginAccountViewModel
{
	public LoginAccountViewModel(string token, string role)
	{
		Token = token;
		Role = role;
	}

	public string Token { get; set; }
	public string Role { get; set; }
}
