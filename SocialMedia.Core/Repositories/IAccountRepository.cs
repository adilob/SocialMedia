using SocialMedia.Core.Entities;

namespace SocialMedia.Core.Repositories;

public interface IAccountRepository : IRepository<Account>
{
	Task<Account> GetByEmailAndPasswordAsync(string email, string password);
}
