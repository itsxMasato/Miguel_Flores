var builder = WebApplication.CreateBuilder(args);

// 1. Configuración de Servicios y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// 2. Configuración de CORS (Permitir que el Front se conecte)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// 3. Middleware
app.UseCors("AllowAll");

// --- RUTAS DE LA API ---

// Ruta de prueba del clima
var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
app.MapGet("/weatherforecast", () =>
{
    return Enumerable.Range(1, 5).Select(index =>
        new {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = summaries[Random.Shared.Next(summaries.Length)]
        })
        .ToArray();
});

// NUEVA RUTA: Datos de Servicios TI (Esto es lo que tu Front necesita)
// Nota: Si tu front pide "/api/servicios", cámbialo a "/api/servicios" aquí abajo
app.MapGet("/servicios", () =>
{
    return new[] {
        new { id = 1, nombre = "Soporte Técnico", descripcion = "Mantenimiento preventivo y correctivo de hardware.", icono = "💻" },
        new { id = 2, nombre = "Desarrollo Web", descripcion = "Creación de aplicaciones y sitios web modernos.", icono = "🌐" },
        new { id = 3, nombre = "Redes y Servidores", descripcion = "Instalación y configuración de infraestructura local.", icono = "📡" },
        new { id = 4, nombre = "Seguridad Informática", descripcion = "Protección de datos y auditorías de seguridad.", icono = "🔒" }
    };
});

app.MapControllers();

app.Run();