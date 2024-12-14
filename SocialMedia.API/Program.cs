
using Microsoft.OpenApi.Models;
using SocialMedia.API.Middlewares;
using SocialMedia.Application.Extensions;
using SocialMedia.Infrastructure.Extensions;

namespace SocialMedia.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder
				.Services.AddApplication()
				.AddInfrastructure(builder.Configuration);

			builder.Services.AddExceptionHandler<ApiExceptionHandler>();
			builder.Services.AddProblemDetails();

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "SocialMedia.API", Version = "v1" });

				// Define the BearerAuth scheme
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
					Scheme = "bearer" // Must be lowercase
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new string[] {}
					}
				});

				c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
			});

			var app = builder.Build();

			app.UseExceptionHandler();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
					// To enable authorization using Swagger (JWT)
					c.OAuthUsePkce();
				});
			}

			app.UseDeveloperExceptionPage();
			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
