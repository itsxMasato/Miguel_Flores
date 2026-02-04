using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")] // si el controller se llama ServiciosController => ruta /api/servicios
public class ServiciosController : ControllerBase
{
    private static readonly List<object> _data = new()
    {
        new { Id = 1, Nombre = "Servicio A" },
        new { Id = 2, Nombre = "Servicio B" }
    };

    [HttpGet]
    public IActionResult Get() => Ok(_data);

    [HttpPost]
    public IActionResult Post([FromBody] object payload)
    {
        // ejemplo simple, en producción mapear a modelo y validar
        return CreatedAtAction(nameof(Get), new { }, payload);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] object payload)
    {
        // actualizar y devolver el recurso actualizado
        return Ok(payload);
    }
}
