// TODO: Problem setting up logging in .Net 6 need to fix
// SignalR issue, when adding item to list, does not display until screen is refreshed

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSpaStaticFiles(configure =>
{
    configure.RootPath = "wwwroot";
});

builder.Services.AddSignalR();

builder.Services.AddSingleton<IToDoRepository, InMemoryToDoRepository>();

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ToDoHub>("/hubs/todo");
});

app.UseSpaStaticFiles();

app.UseSpa(config =>
{
    config.UseProxyToSpaDevelopmentServer("http://localhost:8080");
});

app.Run();

