namespace SocialMedia.Application.Models.InputModel;

public class CreatePostInputModel
{
    public Guid ProfileId { get; set; }
    public string Content { get; set; }
}
