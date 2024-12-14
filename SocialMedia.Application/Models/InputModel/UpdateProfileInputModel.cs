using SocialMedia.Core.ValueObjects;

namespace SocialMedia.Application.Models.InputModel;

public class UpdateProfileInputModel
{
    public string ExhibitionName { get; set; }
    public string About { get; set; }
    public string Picture { get; set; }
    public Address Address { get; set; }
    public string Occupation { get; set; }
}
