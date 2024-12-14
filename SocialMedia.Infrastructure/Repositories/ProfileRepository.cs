using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Repositories;
using SocialMedia.Infrastructure.Persistence.SqlServer;

namespace SocialMedia.Infrastructure.Repositories;

public class ProfileRepository : IProfileRepository
{
	private readonly SocialMediaDbContext _context;

	public ProfileRepository(SocialMediaDbContext context)
	{
		_context = context;
	}

	public async Task<Profile> AddAsync(Profile entity)
	{
		await _context.Profiles.AddAsync(entity);
		await _context.SaveChangesAsync();
		return entity;
	}

	public async Task DeleteAsync(Profile entity)
	{
		if (entity == null)
		{
			return;
		}

		_context.Profiles.Remove(entity);
		await _context.SaveChangesAsync();
	}

	public async Task<IEnumerable<Profile>> GetAllAsync()
	{
		return await _context.Profiles.ToListAsync();
	}

	public async Task<IEnumerable<Profile>> GetByAccountIdAsync(Guid accountId)
	{
		return await _context.Profiles.Where(x => x.AccountId == accountId).ToListAsync();
	}

	public async Task<Profile> GetByIdAsync(Guid id)
	{
		Profile? profile = null;

		if (id == Guid.Empty)
		{
			return await Task.FromResult(profile!);
		}

		return await _context.Profiles.FindAsync(id);
	}

	public async Task UpdateAsync(Profile entity)
	{
		_context.Profiles.Update(entity);
		await _context.SaveChangesAsync();
	}
}
