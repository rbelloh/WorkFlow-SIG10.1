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
using Microsoft.Extensions.Logging;
using System.IO; // Added by Gemini to handle Path operations

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<Usuario>>();

// --- Gemini's Database Provider Switch ---
if (builder.Environment.IsDevelopment())
{
    // We are in the cloud dev environment, use a local SQLite database file.
    // This makes the app runnable without needing a remote MySQL server.
    // --- Start of Gemini's Path Fix ---
    var dbPath = Path.Combine(builder.Environment.ContentRootPath, "dev.db");
    var connectionString = $"Data Source={dbPath}";
    // --- End of Gemini's Path Fix ---
    builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
        options.UseSqlite(connectionString));
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite(connectionString));
}
else
{
    // We are in a production or local environment, use the configured MySQL database.
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
}
// --- End of Gemini's Switch ---

// Register the XML Import Service
builder.Services.AddScoped<WorkFlow_SIG10._1.Services.XmlImportService>();

// Register the Excel Import Service
builder.Services.AddScoped<WorkFlow_SIG10._1.Services.ExcelImportService>();

// Register the Task Update Service
builder.Services.AddScoped<WorkFlow_SIG10._1.Services.TaskUpdateService>();

// Register the Task Aggregation Service
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
    options.AccessDeniedPath = "/Forbidden";
});

var app = builder.Build();

// --- Gemini's Seeding and Migration Logic ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var logger = services.GetRequiredService<ILogger<Program>>();

        // Ensure database is migrated before doing anything else
        var dbContext = services.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();
        logger.LogInformation("Database migrated successfully.");

        var userManager = services.GetRequiredService<UserManager<Usuario>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();

        // Ensure SuperAdmin role exists
        var roleName = "SuperAdmin";
        var roleExists = await roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            await roleManager.CreateAsync(new IdentityRole<int>(roleName));
            logger.LogInformation("Role SuperAdmin created.");
        }

        // Ensure superadmin user exists
        var superAdminUser = await userManager.FindByNameAsync("superadmin");
        if (superAdminUser == null)
        {
            superAdminUser = new Usuario
            {
                UserName = "superadmin",
                Email = "superadmin@example.com",
                Nombre = "Super",
                Apellido = "Admin",
                Direccion = "System",
                NumeroIdentificacion = "00000000-0",
                Dependencia = "IT",
                EmailConfirmed = true
            };
            var createUserResult = await userManager.CreateAsync(superAdminUser, "Admin123!");
            if (createUserResult.Succeeded)
            {
                logger.LogInformation("User superadmin created.");
                // Assign SuperAdmin role to the new user
                await userManager.AddToRoleAsync(superAdminUser, roleName);
                logger.LogInformation("User superadmin assigned to SuperAdmin role.");
            }
            else
            {
                logger.LogError($"Error creating superadmin user: {string.Join(", ", createUserResult.Errors.Select(e => e.Description))}");
            }
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding or migrating the database.");
    }
}
// --- End of Gemini's Logic ---

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(context =>
        {
            var exceptionHandlerPathFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature?.Error;

            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
            var errorId = Guid.NewGuid().ToString();

            logger.LogError(exception, "ErrorId: {ErrorId} - An unhandled exception occurred.", errorId);

            context.Response.Redirect($"/Error500?errorId={errorId}");
            return Task.CompletedTask;
        });
    });
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseStatusCodePagesWithReExecute("/Error408", "?statusCode={0}");

app.UseRequestLocalization();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapRazorPages();

app.Run();