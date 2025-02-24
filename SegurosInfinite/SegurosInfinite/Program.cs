using Microsoft.EntityFrameworkCore;
using SegurosInfinite.Data;
using SegurosInfinite.Services.Interfaces;
using SegurosInfinite.Services;
using Microsoft.AspNetCore.Authentication;
using SegurosInfinite.Services.Authentication;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

/*builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});*/

builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

// CORS enabled
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Register services
builder.Services.AddScoped<IUserService, UserService>();

// Setup connection string for postgresql database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add APIS
builder.Services.AddControllers();

builder.Services.AddAuthorization();

var app = builder.Build();

// Configurar enrutamiento de solicitudes
app.UseRouting();

// Apply the authentication and authorization middleware globally
app.UseAuthentication();
app.UseAuthorization();

// Middleware to redirect to HTTPS (if necessary)
app.UseHttpsRedirection();

// Middleware to look for default files like `index.html` in static routes
app.UseDefaultFiles();

// Configure static files to serve content like images, CSS, etc.
app.UseStaticFiles();

// Setup the base of the route
app.UsePathBase("/secure-infinite");

// Apply CORS
app.UseCors("AllowAll");

app.UseDeveloperExceptionPage();  // For detailed debugging

// Map the controllers to the endpoints globally (for all routes)
app.MapControllers();

// Run app
app.Run();
