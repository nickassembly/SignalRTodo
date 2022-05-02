var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSpaStaticFiles(configure => {
    configure.RootPath = "wwwroot";
});

builder.Services.AddSingleton<InMemoryToDoRepository, InMemoryToDoRepository>();

var app = builder.Build();

app.UseSpaStaticFiles();
app.UseSpa(config => 
{
  config.UseProxyToSpaDevelopmentServer("http://localhost:8080");
});

app.Run();
