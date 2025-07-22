using API_Cine.Models.DTOs;
using API_Cine.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_Cine.Controllers
{
    [Route("api/asignaciones")]
    [ApiController]
    public class PeliculaSalaCinesController : ControllerBase
    {
        private readonly IPeliculaSalaCineService _asignacionService;

        public PeliculaSalaCinesController(IPeliculaSalaCineService asignacionService)
        {
            _asignacionService = asignacionService;
        }

        // GET: api/peliculas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PeliculaSalaCineDTO>>> GetPeliculaSalaCine()
        {
            var peliculaSalaCine = await _asignacionService.GetAllAsignacion();
            return Ok(peliculaSalaCine);
        }

        // POST: api/asignaciones/pelicula/{idPelicula}/sala/{idSala}
        [HttpPost("pelicula/{idPelicula}/sala/{idSala}")]
        public async Task<ActionResult<PeliculaSalaCineDTO>> AsignarPeliculaASala(int idPelicula, int idSala, [FromBody] PeliculaSalaCineCreateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nuevaAsignacion = await _asignacionService.AsignarPeliculaASalaAsync(idPelicula, idSala, dto);

            if (nuevaAsignacion == null)
            {
                return BadRequest("La película o la sala especificadas no existen o están inactivas.");
            }

            return Ok(nuevaAsignacion);
        }

        // PUT: api/asignaciones/5
        [HttpPut("{idAsignacion}")]
        public async Task<IActionResult> UpdateAsignacion(int idAsignacion, [FromBody] PeliculaSalaCineDTO dto)
        {

            if (idAsignacion != dto.IdPeliculaSala)
            {
                return BadRequest("El ID de la ruta no coincide con el ID del cuerpo de la solicitud.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _asignacionService.UpdateAsignacionAsync(dto);

            if (!success)
            {
                // Si el servicio devuelve false, significa que no encontró el registro.
                return NotFound($"No se encontró la asignación con ID {idAsignacion} para actualizar.");
            }
            return NoContent();
        }

        // DELETE: api/asignaciones/10
        [HttpDelete("{idAsignacion}")]
        public async Task<IActionResult> RemoverAsignacion(int idAsignacion)
        {
            var success = await _asignacionService.RemoverAsignacionAsync(idAsignacion);

            if (!success)
            {
                return NotFound($"No se encontró la asignación con ID {idAsignacion}.");
            }

            return NoContent(); 
        }

        [HttpGet("por-fecha/{fecha}")]
        public async Task<ActionResult<IEnumerable<PeliculaSalaDetalleDTO>>> GetPeliculasPorFechaPublicacion(DateOnly fecha)
        {
            try
            {
                var resultados = await _asignacionService.GetByFechaPublicacion(fecha);
                if (resultados == null || !resultados.Any())
                {
                    return NotFound($"No se encontraron películas para la fecha {fecha}");
                }

                return Ok(resultados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

    }
}
