using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkFlow_SIG10._1.Data;
using WorkFlow_SIG10._1.Models;
using WorkFlow_SIG10._1.Areas.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<Usuario>>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure DbContextOptions once
var dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
dbContextOptionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
var dbContextOptions = dbContextOptionsBuilder.Options;

// Register DbContextOptions as a Singleton
builder.Services.AddSingleton(dbContextOptions);

// Register DbContext as Scoped (for Identity)
builder.Services.AddScoped<ApplicationDbContext>(sp => new ApplicationDbContext(dbContextOptions));

// Register DbContextFactory as Singleton
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddIdentity<Usuario, IdentityRole<int>>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Set to false for development
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddRoles<IdentityRole<int>>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/AccessDenied"; // Optional: for unauthorized access
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Custom fallback for unauthenticated users
app.Use(async (context, next) =>
{
    if (!context.User.Identity?.IsAuthenticated ?? true)
    {
        // If not authenticated and not already on the login page, redirect to login
        if (!context.Request.Path.StartsWithSegments("/login", StringComparison.OrdinalIgnoreCase))
        {
            context.Response.Redirect("/login");
            return;
        }
    }
    await next();
});

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host"); // This will now only be reached by authenticated users or after login
app.MapRazorPages();

app.Run();
