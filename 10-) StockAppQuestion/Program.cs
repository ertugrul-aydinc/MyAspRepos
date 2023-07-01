﻿using _10___ServiceContracts;
using _10___Services;
using _10___StockAppQuestion.OptionsModels;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));
builder.Services.AddScoped<IFinnhubService, FinnhubService>();
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();
app.Run();

