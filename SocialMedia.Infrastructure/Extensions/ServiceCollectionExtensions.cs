using SocialMedia.Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Infrastructure.Persistence.SqlServer;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Repositories;
using SocialMedia.Infrastructure.Repositories;

namespace SocialMedia.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddInfrastructure(
			this IServiceCollection services
			, IConfiguration configuration)
	{
		services
			.AddDbContext(configuration)
			.AddRepositories()
			.AddAuth(configuration);
			//.ExecuteMigrations();

		return services;
	}

	private static IServiceCollection AddRepositories(this IServiceCollection services)
	{
		services.AddScoped<IAuthService, AuthService>();
		services.AddScoped<IAccountRepository, AccountRepository>();
		services.AddScoped<IProfileRepository, ProfileRepository>();
		services.AddScoped<IPostRepository, PostRepository>();

		return services;
	}

	private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<SocialMediaDbContext>(options =>
		{
			options.UseInMemoryDatabase("SocialMediaDb");
			//options.UseSqlServer(configuration.GetConnectionString("SocialMediaDb"));
		});

		return services;
	}

	private static IServiceCollection ExecuteMigrations(this IServiceCollection services)
	{
		using var serviceProvider = services.BuildServiceProvider();
		using var scope = serviceProvider.CreateScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<SocialMediaDbContext>();
		dbContext.Database.Migrate();

		return services;
	}

	private static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
	{
		services
			.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,

					ValidIssuer = configuration["Jwt:Issuer"],
					ValidAudience = configuration["Jwt:Audience"],
					IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
				};
			});

		services.AddScoped<IAuthService, AuthService>();

		return services;
	}
}
