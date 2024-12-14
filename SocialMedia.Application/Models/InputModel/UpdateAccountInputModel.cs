using SocialMedia.Core.ValueObjects;

namespace SocialMedia.Application.Models.InputModel;

public class UpdateAccountInputModel
{
    public string Fullname { get; set; }
    public string Email { get; set; }
    public DateTime Birthdate { get; set; }
    public Phone Phone { get; set; }
}
