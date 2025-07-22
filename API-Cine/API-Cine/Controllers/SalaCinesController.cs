using API_Cine.Models.DTOs;
using API_Cine.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_Cine.Controllers
{
    [Route("api/salas-cine")]
    [ApiController]
    public class SalasCineController : ControllerBase
    {
        private readonly ISalaCineService _salaCineService;

        public SalasCineController(ISalaCineService salaCineService)
        {
            _salaCineService = salaCineService;
        }

        // POST: api/salas-cine/verificar-disponibilidad
        [HttpPost("verificar-disponibilidad")]
        public async Task<ActionResult<VerificarDisponibilidadResponseDTO>> VerificarDisponibilidad(VerificarDisponibilidadRequestDTO request)
        {
            if (string.IsNullOrWhiteSpace(request?.NombreSala))
            {
                return BadRequest("El campo 'nombreSala' es requerido.");
            }

            var resultado = await _salaCineService.VerificarDisponibilidadAsync(request);
            return Ok(resultado);
        }

        // GET: api/salas-cine
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalaCineDTO>>> GetSalasCine()
        {
            var salas = await _salaCineService.GetAllSalasCineAsync();
            return Ok(salas);
        }

        // GET: api/salas-cine/3
        [HttpGet("{id}")]
        public async Task<ActionResult<SalaCineDTO>> GetSalaCine(int id)
        {
            var sala = await _salaCineService.GetSalaCineByIdAsync(id);
            if (sala == null)
            {
                return NotFound($"No se encontró la sala con ID {id}.");
            }
            return Ok(sala);
        }

        // POST: api/salas-cine
        [HttpPost]
        public async Task<ActionResult<SalaCineDTO>> PostSalaCine(SalaCineCreateDTO salaCineCreateDto)
        {
            var nuevaSala = await _salaCineService.CreateSalaCineAsync(salaCineCreateDto);
            return CreatedAtAction(nameof(GetSalaCine), new { id = nuevaSala.Id_Sala }, nuevaSala);
        }

        // PUT: api/salas-cine/3
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalaCine(int id, SalaCineUpdateDTO salaCineUpdateDto)
        {
            if (id != salaCineUpdateDto.Id_Sala)
            {
                return BadRequest("El ID de la URL no coincide con el ID del cuerpo de la solicitud.");
            }
            var success = await _salaCineService.UpdateSalaCineAsync(salaCineUpdateDto);
            if (!success)
            {
                return NotFound($"No se encontró la sala con ID {id} para actualizar.");
            }
            return NoContent();
        }

        // DELETE: api/salas-cine/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalaCine(int id)
        {
            var success = await _salaCineService.DeleteSalaCineAsync(id);
            if (!success)
            {
                return NotFound($"No se encontró la sala con ID {id} para eliminar.");
            }
            return NoContent();
        }


        [HttpGet("dashboard")]
        public async Task<ActionResult<DashBoardDTO>> GetDash() { 
        
            var datos = await _salaCineService.GetDash();

            return Ok(datos);

        }
    }
}
