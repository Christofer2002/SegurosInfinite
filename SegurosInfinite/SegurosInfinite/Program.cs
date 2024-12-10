var builder = WebApplication.CreateBuilder(args);

// Agregar controladores
builder.Services.AddControllers();

var app = builder.Build();

app.UsePathBase("/secure-infinite");

// Configurar el middleware necesario
app.UseHttpsRedirection(); // Redireccionar a HTTPS
app.UseStaticFiles(); // Servir archivos est�ticos
app.UseRouting(); // Habilitar el enrutamiento

// Configurar los endpoints y fallback para el frontend
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Mapea los controladores
});

// Iniciar la aplicaci�n
app.Run();
