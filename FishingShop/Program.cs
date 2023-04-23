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
//Services
builder.Services.AddRazorPages();
builder.Services.ConfigureApplicationCookie(options =>
{
	// Cookie settings
	options.Cookie.HttpOnly = true;
	options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

	options.LoginPath = "/Views/Account/Login";
	options.AccessDeniedPath = "/Views/Account/Register";
	options.SlidingExpiration = true;
});
//
//Aunthefication and authorization
builder.Services.AddIdentity<ApplicationUser, Role>().AddEntityFrameworkStores<ShopDatabaseContext>().AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	try
	{
		var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
		var rolesManager = services.GetRequiredService<RoleManager<Role>>();
		await RoleInitializer.InitializyAsync(userManager, rolesManager);
	}
	catch(Exception ex)
	{
		var logger = services.GetService<ILogger<Program>>();
		logger.LogError(ex, "Error occured");
	}
}

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
    pattern: "{controller=Roles}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
