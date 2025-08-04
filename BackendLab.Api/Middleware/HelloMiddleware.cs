namespace BackendLab.Api.Middleware;

public class HelloMiddleware
{
    private readonly RequestDelegate _next;

    public HelloMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task Invoke(HttpContext context)
    {
        Console.WriteLine($"Hello incoming requesting");
        return _next.Invoke(context);
    }
}
