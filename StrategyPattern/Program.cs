using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StrategyPattern.Models;
using StrategyPattern.Repositories;

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
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IProductRepository>(sp =>
{
    var httpContext=sp.GetRequiredService<IHttpContextAccessor>();
    var claim= httpContext.HttpContext.User.Claims.Where(x=>x.Type==Settings.CalimDatabaseType).FirstOrDefault();
    var context = sp.GetRequiredService<AppIdenitytDbContext>();
    if (claim == null) return new ProductRepositoryFromSqlServer(context);
    var databaseType=(DatabaseType)int.Parse(claim.Type);
    return databaseType switch
    {
        DatabaseType.SqlServer => new ProductRepositoryFromSqlServer(context),
        DatabaseType.MongoDb => new ProductRepositoryFromMongoDb(builder.Configuration),
    };
});
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
