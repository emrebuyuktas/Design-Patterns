using DecoratorPattern.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using DecoratorPattern.Repositories;
using DecoratorPattern.Repositories.Decorator;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMemoryCache();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRepository,ProductRepository>().Decorate<IRepository,ProductRepositoryCacheDecorator>().Decorate<IRepository,ProductRepositoryLoggingDecorator>();

builder.Services.AddScoped<IRepository>(sp=>{
    var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();

    var context = sp.GetRequiredService<AppIdenitytDbContext>();
    var memoryCache = sp.GetRequiredService<IMemoryCache>();
    var productRepository = new ProductRepository(context);
    var logService = sp.GetRequiredService<ILogger<ProductRepositoryLoggingDecorator>>();

    if (httpContextAccessor.HttpContext.User.Identity.Name == "user1")
    {
        var cacheDecorator = new ProductRepositoryCacheDecorator(productRepository, memoryCache);
        return cacheDecorator;
    }

    var logDecorator = new ProductRepositoryLoggingDecorator(productRepository, logService);

    return logDecorator;
});

builder.Services.AddDbContext<AppIdenitytDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AppIdenitytDbContext>();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{

    var identityDbContext = scope.ServiceProvider.GetRequiredService<AppIdenitytDbContext>();

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

    identityDbContext.Database.Migrate(); 

    if (!userManager.Users.Any())
    {
        userManager.CreateAsync(new AppUser() { UserName = "User1", Email = "user1@outlook.com" }, "Password12*").Wait();
        userManager.CreateAsync(new AppUser() { UserName = "User2", Email = "user2@outlook.com" }, "Password12*").Wait();
        userManager.CreateAsync(new AppUser() { UserName = "User3", Email = "user3@outlook.com" }, "Password12*").Wait();
        userManager.CreateAsync(new AppUser() { UserName = "User4", Email = "user4@outlook.com" }, "Password12*").Wait();
        userManager.CreateAsync(new AppUser() { UserName = "User5", Email = "user5@outlook.com" }, "Password12*").Wait();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
