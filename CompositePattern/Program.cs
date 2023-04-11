using CompositePattern.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
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
        var newUser = new AppUser() { UserName = "user1", Email = "user1@outlook.com" };
        userManager.CreateAsync(newUser, "Password12*").Wait();

        userManager.CreateAsync(new AppUser() { UserName = "user2", Email = "user2@outlook.com" }, "Password12*").Wait();
        userManager.CreateAsync(new AppUser() { UserName = "user3", Email = "user3@outlook.com" }, "Password12*").Wait();
        userManager.CreateAsync(new AppUser() { UserName = "user4", Email = "user4@outlook.com" }, "Password12*").Wait();
        userManager.CreateAsync(new AppUser() { UserName = "user5", Email = "user5@outlook.com" }, "Password12*").Wait();

        var newCategory1 = new Category { Name = "Suç romanlarý", ReferenceId = 0, UserId = newUser.Id };
        var newCategory2 = new Category { Name = "Cinayet romanlarý", ReferenceId = 0, UserId = newUser.Id };
        var newCategory3 = new Category { Name = "Polisiye romanlarý", ReferenceId = 0, UserId = newUser.Id };

        identityDbContext.Categories.AddRange(newCategory1, newCategory2, newCategory3);

        identityDbContext.SaveChanges();

        var subCategory1 = new Category { Name = "Suç romanlarý 1", ReferenceId = newCategory1.Id, UserId = newUser.Id };
        var subCategory2 = new Category { Name = "Cinayet romanlarý 1", ReferenceId = newCategory2.Id, UserId = newUser.Id };
        var subCategory3 = new Category { Name = "Polisiye romanlarý 1", ReferenceId = newCategory3.Id, UserId = newUser.Id };

        identityDbContext.Categories.AddRange(subCategory1, subCategory2, subCategory3);
        identityDbContext.SaveChanges();

        var subCategory4 = new Category { Name = "Cinayet romanlarý 1.1", ReferenceId = subCategory2.Id, UserId = newUser.Id };

        identityDbContext.Categories.Add(subCategory4);
        identityDbContext.SaveChanges();

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
