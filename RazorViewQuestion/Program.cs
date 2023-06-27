using _5___ServiceContracts;
using _5___Services;
using Autofac.Extensions.DependencyInjection;
using Autofac;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddControllersWithViews();

//builder.Services.Add(new ServiceDescriptor(
//    typeof(ICityWeathersService),
//    typeof(CityWeathersService),
//    ServiceLifetime.Transient
//    ));

//builder.Services.AddScoped<ICityWeathersService, CityWeathersService>();

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterType<CityWeathersService>().As<ICityWeathersService>().InstancePerLifetimeScope();
});

var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();

app.Run();

