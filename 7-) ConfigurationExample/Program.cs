using _7___ConfigurationExample.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.Configure<WeatherApiOptions>(builder.Configuration.GetSection("weatherapi"));
var app = builder.Build();


app.UseStaticFiles();
app.UseRouting();



#pragma warning disable
app.UseEndpoints(endpoint =>
{
    endpoint.Map("/someurl", async context =>
    {
        await context.Response.WriteAsync(builder.Configuration["weatherapi:ClientID"] + "\n");
        await context.Response.WriteAsync(builder.Configuration["weatherapi:ClientSecret"] + "\n");
    });
});
app.MapControllers();
app.Run();

