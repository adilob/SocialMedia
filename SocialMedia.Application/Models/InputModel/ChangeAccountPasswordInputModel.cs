namespace SocialMedia.Application.Models.InputModel;

public class ChangeAccountPasswordInputModel
{
    public string Email { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}
