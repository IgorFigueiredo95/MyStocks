using MyStocks.Api.Common;
using MyStocks.Domain.Common;

namespace MyStocks.Api.Middlewares;

public class GlobalHandleException
{
    private readonly RequestDelegate _next;

    public GlobalHandleException(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {

        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            ProblemDetailException(context, ex);
        } 

    }

    private HttpContext ProblemDetailException(HttpContext context, Exception exception)
    {
            context.Response.StatusCode = 500;
            context.Response.WriteAsJsonAsync(
            Responses.ErrorResponse(context, Error.Create("UNHANDLED_ERROR", exception.Message)).Value);

        return context;
    }
}
