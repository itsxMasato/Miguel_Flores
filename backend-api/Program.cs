var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();
app.UseCors("AllowAll");

// Base de datos temporal en memoria
var servicios = new List<dynamic>
{
    new { id = 1, nombre = "Soporte Técnico", descripcion = "Mantenimiento de PC", icono = "💻" },
    new { id = 2, nombre = "Redes", descripcion = "Instalación de WiFi", icono = "📡" }
};

// GET: Obtener servicios (Lo que usa getServicios())
app.MapGet("/servicios", () => servicios);

// POST: Crear servicio (Lo que usa crearServicio())
app.MapPost("/servicios", (dynamic nuevo) => {
    var id = servicios.Count + 1;
    var servicioFinal = new { id = id, nombre = (string)nuevo.nombre, descripcion = (string)nuevo.descripcion, icono = "✨" };
    servicios.Add(servicioFinal);
    return Results.Created($"/servicios/{id}", servicioFinal);
});

// PUT: Actualizar servicio (Lo que usa actualizarServicio())
app.MapPut("/servicios/{id}", (int id, dynamic editado) => {
    var index = servicios.FindIndex(s => s.id == id);
    if (index == -1) return Results.NotFound();
    servicios[index] = new { id = id, nombre = (string)editado.nombre, descripcion = (string)editado.descripcion, icono = "📝" };
    return Results.Ok(servicios[index]);
});

app.MapControllers();
app.Run();