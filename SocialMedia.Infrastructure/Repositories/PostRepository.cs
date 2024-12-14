using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Repositories;
using SocialMedia.Infrastructure.Persistence.SqlServer;
using System.Linq;

namespace SocialMedia.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
	private readonly SocialMediaDbContext _context;

	public PostRepository(SocialMediaDbContext context)
	{
		_context = context;
	}

	public async Task<Post> AddAsync(Post entity)
	{
		await _context.Posts.AddAsync(entity);
		await _context.SaveChangesAsync();
		return entity;
	}

	public async Task DeleteAsync(Post entity)
	{
		if (entity == null)
		{
			return;
		}

		_context.Posts.Remove(entity);
		await _context.SaveChangesAsync();
	}

	public async Task<IEnumerable<Post>> GetAllAsync()
	{
		return await _context.Posts.ToListAsync();
	}

	public async Task<Post> GetByIdAsync(Guid id)
	{
		Post? post = null;

		if (id == Guid.Empty)
		{
			return await Task.FromResult(post!);
		}

		return await _context.Posts.Where(x => x.Id == id).Include(x => x.Profile).SingleOrDefaultAsync();
	}

	public async Task<IEnumerable<Post>> GetFollowedProfilePosts(Guid profileId, int page, int pageSize)
	{
		if (profileId == Guid.Empty)
		{
			return new List<Post>();
		}

		var result = await _context.GetFollowedProfilePosts(
			profileId, page, pageSize);

		if (result is null)
		{
			return new List<Post>();
		}

		var postIds = result.Select(p => p.Id).ToList();

		return await _context.Posts
			.Where(p => postIds.Contains(p.Id))
			.Include(p => p.Profile)
			.ToListAsync();
	}

	public async Task<IEnumerable<Post>> GetPostsByProfile(Guid profileId)
	{
		if (profileId == Guid.Empty)
		{
			return new List<Post>();
		}

		var posts = await _context.Posts
			.Where(p => p.ProfileId == profileId)
			.Include(p => p.Profile)
			.ToListAsync();

		return posts;
	}

	public async Task UpdateAsync(Post entity)
	{
		_context.Posts.Update(entity);
		await _context.SaveChangesAsync();
	}
}
