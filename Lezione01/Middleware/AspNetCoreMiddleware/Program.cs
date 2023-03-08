var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (context, next) =>
{
    await context.Response.WriteAsync($"Hello from MW1 going {Environment.NewLine}");
    await next();
    await context.Response.WriteAsync($"Hello from MW1 returning {Environment.NewLine}");
});

app.Use(async (context, next) =>
{
    await context.Response.WriteAsync($"Hello from MW2 going {Environment.NewLine}");
    await next();
    await context.Response.WriteAsync($"Hello from MW2 returning {Environment.NewLine}");
});

app.Run(async (context) =>
{
    await context.Response.WriteAsync($"Hello from terminal middleware {Environment.NewLine}");
});

app.Run();