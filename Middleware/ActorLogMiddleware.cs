using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MoviesApp.Middleware
{
    public class ActorLogMiddleware
    {
        private readonly RequestDelegate _next;

        public ActorLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ILogger<ActorLogMiddleware> logger)
        {
            if (httpContext.Request.Path.StartsWithSegments("/Actors"))
            {
                logger.LogInformation("OPENED CHAPTER WITH ACTORS\n"+$"Request: {httpContext.Request.Path}  Method: {httpContext.Request.Method}"+ 
                                      $"Request.Body: {httpContext.Request.Body}\n" + 
                                      $"\tConnection.Id: {httpContext.Connection.Id}\n" + 
                                      $"\tCookies: {httpContext.Request.Cookies}\n" + 
                                      $"\tProtocol: {httpContext.Request.Protocol}\n" + $"\tFeatures: {httpContext.Features.Revision}\n" +
                                      $"\tHost: {httpContext.Request.Host}\n" + 
                                      $"\tScheme: {httpContext.Request.Scheme}");
            }
            await _next(httpContext);
        }
    }
}