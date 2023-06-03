using App.Data;
using App.Services;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp.Web.Caching;
using SixLabors.ImageSharp.Web.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddImageSharp(
    options =>
    {
        options.BrowserMaxAge = TimeSpan.FromDays(7);
        options.CacheMaxAge = TimeSpan.FromDays(365);
        options.CacheHashLength = 8;
    }).Configure<PhysicalFileSystemCacheOptions>(options =>
    {
        options.CacheFolder = "img/cache";
    });

builder.Services.AddSingleton<IProcessadorImagem, ProcessadorImagemService>();
builder.Services.AddDbContext<DataContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();
app.UseDeveloperExceptionPage();
app.UseImageSharp();
app.UseStaticFiles();
app.UseRouting();
app.MapDefaultControllerRoute();
app.Run();
