using registration.Services;

namespace registration.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            ErrorLogService errorLogService)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                errorLogService.LogError(ex, context);

                // TEMPORARY
                throw;
            }
        }
    }
}