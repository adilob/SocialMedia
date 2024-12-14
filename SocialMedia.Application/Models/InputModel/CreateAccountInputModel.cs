using SocialMedia.Core.ValueObjects;

namespace SocialMedia.Application.Models.InputModel;

public class CreateAccountInputModel
{
    public string Fullname { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public DateTime Birthdate { get; set; }
    public Phone Phone { get; set; }
}
