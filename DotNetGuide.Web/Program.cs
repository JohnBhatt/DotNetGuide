using DotNetGuide.Application.Interface;
using DotNetGuide.Application.Utility;
using DotNetGuide.Domain.Entities;
using DotNetGuide.Infrastructure.Data;
using DotNetGuide.Infrastructure.DbInitializer;
using DotNetGuide.Infrastructure.Middleware;
using DotNetGuide.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add RazorPage Support for Identity Pages
builder.Services.AddRazorPages();

// Connecting to SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity Setup (Ensure ApplicationUser is used)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


// Configure Application Cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.SlidingExpiration = true;
});

// Distributed Memory Cache and Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(180);
    option.Cookie.HttpOnly = true;
    option.Cookie.IsEssential = true;
});



// Custom Services

builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IUserStore<ApplicationUser>, UserStore<ApplicationUser, IdentityRole, AppDbContext>>();

builder.Services.AddScoped<IUserEmailStore<ApplicationUser>, UserStore<ApplicationUser, IdentityRole, AppDbContext>>();



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

//Custom middleware for getting name of user in claims. You can add custom claims as well. 
app.UseMiddleware<TenantIdMiddleware>();

//
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

AddPendingMigrations();

app.Run();


void AddPendingMigrations()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }

}