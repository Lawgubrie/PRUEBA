using API_Cine.Models.DTOs;
using API_Cine.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_Cine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly IPeliculaService _peliculaService;

        public PeliculasController(IPeliculaService peliculaService)
        {
            _peliculaService = peliculaService;
        }

        // GET: api/peliculas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PeliculaDTO>>> GetPeliculas()
        {
            var peliculas = await _peliculaService.GetAllPeliculasAsync();
            return Ok(peliculas);
        }

        // GET: api/peliculas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PeliculaDTO>> GetPelicula(int id)
        {
            var pelicula = await _peliculaService.GetPeliculaByIdAsync(id);
            if (pelicula == null)
            {
                return NotFound($"No se encontró la película con ID {id}.");
            }
            return Ok(pelicula);
        }

        [HttpGet("buscar/{nombre}")]
        public async Task<ActionResult<IEnumerable<PeliculaDTO>>> BuscarPeliculaPorNombre(string nombre)
        {

            var peliculas = await _peliculaService.buscarNombre(nombre);

            if (peliculas == null || !peliculas.Any())
            {
                return NotFound($"No se encontraron películas que coincidan con el nombre '{nombre}'.");
            }

            return Ok(peliculas);
        }

        // POST: api/peliculas
        [HttpPost]
        public async Task<ActionResult<PeliculaDTO>> PostPelicula(PeliculaCreateDTO peliculaCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nuevaPelicula = await _peliculaService.CreatePeliculaAsync(peliculaCreateDto);

            return CreatedAtAction(nameof(GetPelicula), new { id = nuevaPelicula.Id_Pelicula }, nuevaPelicula);
        }

        // PUT: api/peliculas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPelicula(int id, PeliculaUpdateDTO peliculaUpdateDto)
        {
            if (id != peliculaUpdateDto.Id_Pelicula)
            {
                return BadRequest("El ID de la URL no coincide con el ID del cuerpo de la solicitud.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _peliculaService.UpdatePeliculaAsync(peliculaUpdateDto);
            if (!success)
            {
                return NotFound($"No se encontró la película con ID {id} para actualizar.");
            }

            return NoContent(); 
        }

        // DELETE: api/peliculas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePelicula(int id)
        {
            var success = await _peliculaService.DeletePeliculaAsync(id);
            if (!success)
            {
                return NotFound($"No se encontró la película con ID {id} para eliminar.");
            }

            return NoContent(); 
        }
    }
}
