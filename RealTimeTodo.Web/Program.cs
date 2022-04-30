var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSpaStaticFiles(configure => {
    configure.RootPath = "wwwroot";
});

var app = builder.Build();

app.UseSpaStaticFiles();
app.UseSpa(config => 
{
  config.UseProxyToSpaDevelopmentServer("http://localhost:8080");
});

app.Run();
