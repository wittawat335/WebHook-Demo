var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/webhook", async context =>
{
    if (!context.Request.Headers.ContainsKey("Authorzation"))
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return;
    }

    var apiKey = context.Request.Headers["Authorzation"];
    if (apiKey != "APIKEY")
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return;
    }

    var requestBody = await context.Request.ReadFromJsonAsync<WebHookPayload>();
    Console.WriteLine($"Header: {requestBody?.Header}, Body: {requestBody?.Body}");
    context.Response.StatusCode = 200;
    await context.Response.WriteAsync("Webhook ok!");
});

app.Run();

public record WebHookPayload (string Header, string Body);
