var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy => 
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();
app.UseCors("AllowAll");

// Usamos una clase fija para evitar errores de "dynamic"
var servicios = new List<Servicio>
{
    new Servicio { id = 1, nombre = "Soporte Técnico", descripcion = "Mantenimiento de PC", icono = "💻" },
    new Servicio { id = 2, nombre = "Redes", descripcion = "Instalación de WiFi", icono = "📡" }
};

// GET: Obtener servicios
app.MapGet("/servicios", () => Results.Ok(servicios));

// POST: Crear servicio
app.MapPost("/servicios", (Servicio nuevo) => {
    nuevo.id = servicios.Any() ? servicios.Max(s => s.id) + 1 : 1;
    nuevo.icono = "✨";
    servicios.Add(nuevo);
    return Results.Created($"/servicios/{nuevo.id}", nuevo);
});

// PUT: Actualizar servicio
app.MapPut("/servicios/{id}", (int id, Servicio editado) => {
    var index = servicios.FindIndex(s => s.id == id);
    if (index == -1) return Results.NotFound();
    
    editado.id = id; // Aseguramos que mantenga el ID original
    servicios[index] = editado;
    return Results.Ok(servicios[index]);
});

app.MapControllers();
app.Run();

// Definición del modelo (Vital para que .NET entienda el JSON)
public class Servicio {
    public int id { get; set; }
    public string nombre { get; set; } = string.Empty;
    public string descripcion { get; set; } = string.Empty;
    public string icono { get; set; } = "🔧";
}