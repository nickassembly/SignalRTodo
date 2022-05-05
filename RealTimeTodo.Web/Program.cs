var builder = WebApplication.CreateBuilder(args);

// TODO: how to configure loggined in .net 6?

// 1:32

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

