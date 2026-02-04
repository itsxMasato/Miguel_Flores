using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

public class ServicioDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
}

[ApiController]
[Route("api/[controller]")]
public class ServiciosController : ControllerBase
{
    private static readonly List<ServicioDto> _data = new()
    {
        new { Id = 1, Nombre = "Servicio A", Descripcion = "Descripción A" },
        new { Id = 2, Nombre = "Servicio B", Descripcion = "Descripción B" }
    };

    [HttpGet]
    public IActionResult Get() => Ok(_data.Select(s => new { id = s.Id, nombre = s.Nombre, descripcion = s.Descripcion }));

    [HttpPost]
    public IActionResult Post([FromBody] JsonElement payload)
    {
        try
        {
            var nombre = payload.GetProperty("nombre").GetString();
            var descripcion = payload.GetProperty("descripcion").GetString();

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(descripcion))
                return BadRequest("Nombre y descripción son requeridos");

            var newId = _data.Count > 0 ? _data.Max(x => x.Id) + 1 : 1;
            var newServicio = new ServicioDto { Id = newId, Nombre = nombre, Descripcion = descripcion };
            
            _data.Add(newServicio);
            return CreatedAtAction(nameof(Get), new { id = newId }, new { id = newId, nombre, descripcion });
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] JsonElement payload)
    {
        try
        {
            var servicio = _data.FirstOrDefault(x => x.Id == id);
            if (servicio == null)
                return NotFound("Servicio no encontrado");

            servicio.Nombre = payload.GetProperty("nombre").GetString();
            servicio.Descripcion = payload.GetProperty("descripcion").GetString();

            return Ok(new { id = servicio.Id, nombre = servicio.Nombre, descripcion = servicio.Descripcion });
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }
}
