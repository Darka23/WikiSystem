using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WikiSystem.Contracts;
using WikiSystem.Data;
using WikiSystem.Data.Identity;
using WikiSystem.Data.Repositories;
using WikiSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

builder.Services.AddScoped<IApplicationDbRepository, ApplicationDbRepository>();

builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<IArticleServices, ArticleServices>();
builder.Services.AddScoped<IAdminServices, AdminServices>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/home/error");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Area",
    pattern: "{area:exists}/{controller=Admin}/{action=AdminPanel}/{id?}");

app.MapControllerRoute(
    name: "Area",
    pattern: "{area:exists}/{controller=Category}/{action=CategoryList}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Article}/{action=ListArticle}/{id?}");

app.MapRazorPages();

app.Run();
