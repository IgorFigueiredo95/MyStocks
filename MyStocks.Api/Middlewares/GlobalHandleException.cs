namespace MyStocks.Api.Middlewares;

public class GlobalHandleException : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await context.Response.WriteAsync("I cought you on my middleware!");
        }
    }
}
