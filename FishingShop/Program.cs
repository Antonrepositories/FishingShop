using FishingShop;
using FishingShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<ShopDatabaseContext>(option => option.UseSqlServer(
    builder.Configuration.GetConnectionString("IdentityConnection")));
        //"DefaultCOnnection")));

//Aunthefication and authorization
builder.Services.AddIdentity<ApplicationUser, Role>().AddEntityFrameworkStores<ShopDatabaseContext>().AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Reviews}/{action=Index}/{id?}");

app.Run();
