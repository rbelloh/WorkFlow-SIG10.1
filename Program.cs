using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkFlow_SIG10._1.Data;
using WorkFlow_SIG10._1.Models;
using WorkFlow_SIG10._1.Areas.Identity;
using Microsoft.AspNetCore.Authorization;
using WorkFlow_SIG10._1.Localization; // Added for custom IdentityErrorDescriber
using System.Globalization; // Added for CultureInfo
using Microsoft.AspNetCore.Localization; // Added for RequestLocalizationOptions

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
    .AddDefaultTokenProviders()
    .AddErrorDescriber<LocalizedIdentityErrorDescriber>(); // Register custom error describer

// Configure Localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("es-ES"),
        new CultureInfo("en-US")
    };

    options.DefaultRequestCulture = new RequestCulture("es-ES");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

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

// Add Request Localization Middleware
app.UseRequestLocalization();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host"); // This will now only be reached by authenticated users or after login
app.MapRazorPages();

app.Run();

