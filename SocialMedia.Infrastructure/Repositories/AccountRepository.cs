using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Repositories;
using SocialMedia.Infrastructure.Persistence.SqlServer;

namespace SocialMedia.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly SocialMediaDbContext _context;

    public AccountRepository(SocialMediaDbContext context)
    {
        _context = context;
    }

    public async Task<Account> AddAsync(Account entity)
    {
        await _context.Accounts.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(Account entity)
    {
        if (entity == null)
        {
            return;
        }

        _context.Accounts.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Account>> GetAllAsync()
    {
        return await _context.Accounts.ToListAsync();
    }

    public async Task<Account> GetByEmailAndPasswordAsync(string email, string password)
    {
        var account = await _context.Accounts.SingleOrDefaultAsync(x => x.Email == email && x.Password == password);
        return account;
    }

    public async Task<Account> GetByIdAsync(Guid id)
    {
        Account? account = null;

        if (id == Guid.Empty)
        {
            return await Task.FromResult(account!);
        }

        return await _context.Accounts.FindAsync(id);
    }

    public async Task UpdateAsync(Account entity)
    {
        _context.Accounts.Update(entity);
        await _context.SaveChangesAsync();
    }
}
