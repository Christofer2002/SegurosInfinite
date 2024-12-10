var builder = WebApplication.CreateBuilder(args);

// Agregar controladores
builder.Services.AddControllers();

var app = builder.Build();

// Configurar la base de la ruta
app.UsePathBase("/secure-infinite");

// Middleware para redireccionar a HTTPS
app.UseHttpsRedirection();

// Habilitar middleware para buscar archivos predeterminados como `index.html`
app.UseDefaultFiles();

// Configurar para servir archivos est�ticos
app.UseStaticFiles();

// Configurar enrutamiento
app.UseRouting();

// Configurar los endpoints
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// Iniciar la aplicaci�n
app.Run();
