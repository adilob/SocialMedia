using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SocialMedia.Infrastructure.Persistence.SqlServer;

namespace SocialMedia.Infrastructure.Persistence;

public class SocialMediaDbContextFactory : IDesignTimeDbContextFactory<SocialMediaDbContext>
{
	public SocialMediaDbContext CreateDbContext(string[] args)
	{
		// add all needed configuration providers
		IConfigurationRoot configuration = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json")
			.Build();

		var optionsBuilder = new DbContextOptionsBuilder<SocialMediaDbContext>();
		optionsBuilder.UseSqlServer(configuration.GetConnectionString("SocialMediaDb"));

		return new SocialMediaDbContext(optionsBuilder.Options);
	}
}
