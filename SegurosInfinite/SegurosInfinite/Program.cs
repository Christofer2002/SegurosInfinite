var builder = WebApplication.CreateBuilder(args);

// Agregar controladores
builder.Services.AddControllers();

var app = builder.Build();

// Configurar el middleware necesario
app.UseHttpsRedirection(); // Redireccionar a HTTPS
app.UseStaticFiles(); // Servir archivos estáticos
app.UseRouting(); // Habilitar el enrutamiento

// Configurar los endpoints y fallback para el frontend
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Mapea los controladores
    endpoints.MapFallbackToFile("index.html"); // Redirige rutas no encontradas al frontend
});

// Iniciar la aplicación
app.Run();
