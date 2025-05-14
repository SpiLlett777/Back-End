namespace Laba_15.Middlewares
{
    public class RoleRedirectMiddleware
    {
        private readonly RequestDelegate _next;

        public RoleRedirectMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower() ?? "";

            if (path!.StartsWith("/api/admin"))
            {
                if (!context.User.Identity!.IsAuthenticated)
                {
                    context.Response.Redirect("/api/forbidden");
                    return;
                }

                if (!context.User.IsInRole("Admin"))
                {
                    context.Response.Redirect("/api/forbidden");
                    return;
                }
            }

            await _next(context);
        }
    }
}
