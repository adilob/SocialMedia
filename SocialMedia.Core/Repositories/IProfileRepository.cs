using SocialMedia.Core.Entities;

namespace SocialMedia.Core.Repositories;

public interface IProfileRepository : IRepository<Profile>
{
	Task<IEnumerable<Profile>> GetByAccountIdAsync(Guid accountId);
}
