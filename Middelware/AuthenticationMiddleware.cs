public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token != "your_valid_token")
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("{\"error\":\"Unauthorized\"}");
            return;
        }
        await _next(context);
    }
}