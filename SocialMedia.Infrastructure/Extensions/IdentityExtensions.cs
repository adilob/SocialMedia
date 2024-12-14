using System.Security.Claims;

namespace SocialMedia.Infrastructure.Extensions;

public static class IdentityExtensions
{
	public static Guid GetAccountId(this ClaimsPrincipal claimsPrincipal)
	{
		if (claimsPrincipal is null)
		{
			throw new ArgumentNullException(nameof(claimsPrincipal));
		}

		var accountId = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "accountId")?.Value;

		if (accountId is null)
		{
			throw new ArgumentNullException(nameof(accountId));
		}

		return Guid.Parse(accountId);
	}
}
