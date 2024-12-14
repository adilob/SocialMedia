using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Exceptions;

namespace SocialMedia.API.Middlewares
{
	public class ApiExceptionHandler : IExceptionHandler
	{
		public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
		{
			ProblemDetails? details;

			if (exception is NotFoundException)
			{
				details = new ProblemDetails
				{
					Status = StatusCodes.Status404NotFound,
					Title = exception.Message,
					Instance = httpContext.Request.Path
				};
			}
			else
			{
				details = new ProblemDetails
				{
					Status = StatusCodes.Status500InternalServerError,
					Title = "An error occurred while processing your request.",
					Instance = httpContext.Request.Path
				};
			}

			httpContext.Response.StatusCode = details?.Status ?? StatusCodes.Status500InternalServerError;

			await httpContext.Response.WriteAsJsonAsync(details, cancellationToken: cancellationToken);

			return true;
		}
	}
}
