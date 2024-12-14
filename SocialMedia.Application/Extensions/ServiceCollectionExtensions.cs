using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Application.Services;

namespace SocialMedia.Application.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services
			.AddServices();
		//    .AddValidators();

		return services;
	}

	private static IServiceCollection AddServices(this IServiceCollection services)
	{
		services.AddScoped<IAccountService, AccountService>();
		services.AddScoped<IProfileService, ProfileService>();
		services.AddScoped<IPostService, PostService>();

		return services;
	}
}
