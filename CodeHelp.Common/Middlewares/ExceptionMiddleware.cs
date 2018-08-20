using Microsoft.AspNetCore.Builder;

namespace CodeHelp.Common.Middlewares
{
    public static class ExceptionMiddleware
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}