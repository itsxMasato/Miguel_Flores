using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ServiciosController : ControllerBase
{
    private static readonly List<dynamic> _data = new()
    {
        new { id = 1, nombre = "Servicio A", descripcion = "Descripción A" },
        new { id = 2, nombre = "Servicio B", descripcion = "Descripción B" }
    };

    [HttpGet]
    public IActionResult Get() => Ok(_data);

    [HttpPost]
    public IActionResult Post([FromBody] dynamic payload)
    {
        var newId = _data.Count > 0 ? ((int)_data.Max(x => (int)x.id)) + 1 : 1;
        dynamic newServicio = new System.Dynamic.ExpandoObject();
        newServicio.id = newId;
        newServicio.nombre = payload.nombre;
        newServicio.descripcion = payload.descripcion;
        
        _data.Add(newServicio);
        return CreatedAtAction(nameof(Get), new { id = newId }, newServicio);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] dynamic payload)
    {
        var servicio = _data.FirstOrDefault(x => (int)x.id == id);
        if (servicio == null)
            return NotFound("Servicio no encontrado");

        // Actualizar propiedades
        servicio.nombre = payload.nombre;
        servicio.descripcion = payload.descripcion;

        return Ok(servicio);
    }
}
