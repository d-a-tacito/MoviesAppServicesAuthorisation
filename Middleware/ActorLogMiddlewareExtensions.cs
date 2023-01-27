using Microsoft.AspNetCore.Builder;
namespace MoviesApp.Middleware;
public static class ActorLogMiddlewareExtensions
{
    public static IApplicationBuilder UseActorLog(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ActorLogMiddleware>();
    }
}