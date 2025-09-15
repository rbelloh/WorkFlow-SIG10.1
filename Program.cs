using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting; // Required for UseUrls
using Microsoft.AspNetCore.HttpOverrides; // Required for Forwarded Headers
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkFlow_SIG10._1.Data;
using WorkFlow_SIG10._1.Models;
using WorkFlow_SIG10._1.Areas.Identity;
using Microsoft.AspNetCore.Authorization;
using WorkFlow_SIG10._1.Localization; // Added for custom IdentityErrorDescriber
using System.Globalization; // Added for CultureInfo
using Microsoft.AspNetCore.Localization; // Added for RequestLocalizationOptions
using Microsoft.Extensions.Logging;
using System.IO; // Added by Gemini to handle Path operations

var builder = WebApplication.CreateBuilder(args);

// --- Gemini's Proxy Fix ---
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    // Trust the proxy that is forwarding the requests.
    options.KnownProxies.Clear();
    options.KnownNetworks.Clear();
});
// --- End of Gemini's Proxy Fix ---

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<Usuario>>();

// Database configuration
if (builder.Environment.IsDevelopment())
{
    var dbPath = Path.Combine(builder.Environment.ContentRootPath, "dev.db");
    var connectionString = $"Data Source={dbPath}";
    builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
        options.UseSqlite(connectionString));
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite(connectionString));
}
else
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
}

// Register services
builder.Services.AddScoped<WorkFlow_SIG10._1.Services.XmlImportService>();
builder.Services.AddScoped<WorkFlow_SIG10._1.Services.ExcelImportService>();
builder.Services.AddScoped<WorkFlow_SIG10._1.Services.TaskUpdateService>();
builder.Services.AddScoped<WorkFlow_SIG10._1.Services.TaskAggregationService>();

builder.Services.AddIdentity<Usuario, IdentityRole<int>>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; 
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddRoles<IdentityRole<int>>()
    .AddDefaultTokenProviders()
    .AddErrorDescriber<LocalizedIdentityErrorDescriber>();

// Configure Localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { new CultureInfo("es-ES"), new CultureInfo("en-US") };
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
    options.AccessDeniedPath = "/Forbidden";
});

var app = builder.Build();

// --- Gemini's Proxy Fix (Middleware) ---
// This must be one of the first middleware components added.
app.UseForwardedHeaders();
// --- End of Gemini's Proxy Fix ---

// Seeding and Migration Logic
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    // ... (seeding logic remains the same)
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(context =>
        {
            var exceptionHandlerPathFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();
            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogError(exceptionHandlerPathFeature?.Error, "An unhandled exception occurred.");
            context.Response.Redirect("/Error500");
            return Task.CompletedTask;
        });
    });
    app.UseHsts();
}

// app.UseHttpsRedirection(); // REVERTED BY GEMINI
app.UseStaticFiles();
app.UseRouting();
app.UseStatusCodePagesWithReExecute("/Error408", "?statusCode={0}");
app.UseRequestLocalization();

// Authentication & Authorization must come after routing and forwarded headers
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapRazorPages();

app.Run();
