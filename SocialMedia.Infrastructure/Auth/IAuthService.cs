namespace SocialMedia.Infrastructure.Auth;

public interface IAuthService
{
	Task<string> ComputeHashAsync(string password);
	Task<string> GenerateTokenAsync(string email, string role, Guid accountId);
}
