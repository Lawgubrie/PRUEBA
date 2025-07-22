using API_Cine.Models.DTOs;
using API_Cine.Models.Entities;
using API_Cine.Repository;

namespace API_Cine.Services.Imp
{
    public class PeliculaSalaCineService : IPeliculaSalaCineService
    {
        private readonly IPeliculaSalaCineRepository _asignacionRepository;
        private readonly IPeliculaRepository _peliculaRepository; 
        private readonly ISalaCineRepository _salaRepository;     

        public PeliculaSalaCineService(
            IPeliculaSalaCineRepository asignacionRepository,
            IPeliculaRepository peliculaRepository,
            ISalaCineRepository salaRepository)
        {
            _asignacionRepository = asignacionRepository;
            _peliculaRepository = peliculaRepository;
            _salaRepository = salaRepository;
        }

        public async Task<IEnumerable<PeliculaSalaCineDTO>> GetAllAsignacion()
        {
            var asignaciones = await _asignacionRepository.GetAllAsync();
            return asignaciones.Select(p => new PeliculaSalaCineDTO
            {
                IdPelicula = p.IdPelicula,
                IdSalaCine = p.IdSalaCine,
                IdPeliculaSala = p.IdPeliculaSala,
                FechaPublicacion = p.FechaPublicacion,
                FechaFin = p.FechaFin
            });
        }

        public async Task<PeliculaSalaCineDTO> AsignarPeliculaASalaAsync(int idPelicula, int idSala, PeliculaSalaCineCreateDTO dto)
        {

            var pelicula = await _peliculaRepository.GetByIdAsync(idPelicula);
            var sala = await _salaRepository.GetByIdAsync(idSala);

            if (pelicula == null || !pelicula.Activo || sala == null || !sala.Activo)
            {
                return null;
            }

            var nuevaAsignacion = new PeliculaSalaCine
            {
                IdPelicula = idPelicula,
                IdSalaCine = idSala,
                FechaPublicacion = dto.FechaPublicacion,
                FechaFin = dto.FechaFin
            };

            await _asignacionRepository.AddAsync(nuevaAsignacion);
            await _asignacionRepository.SaveChangesAsync();

            return new PeliculaSalaCineDTO
            {
                IdPeliculaSala = nuevaAsignacion.IdPeliculaSala,
                IdPelicula = nuevaAsignacion.IdPelicula,
                IdSalaCine = nuevaAsignacion.IdSalaCine,
                FechaPublicacion = nuevaAsignacion.FechaPublicacion,
                FechaFin = nuevaAsignacion.FechaFin
            };
        }


        public async Task<bool> RemoverAsignacionAsync(int idAsignacion)
        {
            var asignacion = await _asignacionRepository.GetByIdAsync(idAsignacion);
            if (asignacion == null)
            {
                return false;
            }

            _asignacionRepository.Delete(asignacion); 
            return await _asignacionRepository.SaveChangesAsync();
        }
        public async Task<IEnumerable<PeliculaSalaDetalleDTO>> GetByFechaPublicacion(DateOnly fecha)
        {
            var asignaciones = await _asignacionRepository.GetFechaPelicula(fecha);

            return asignaciones.Select(a => new PeliculaSalaDetalleDTO
            {
                NombrePelicula = a.IdPeliculaNavigation.Nombre,
                NombreSala = a.IdSalaCineNavigation.Nombre,
                TipoSala = a.IdSalaCineNavigation.Tipo,
                FechaPublicacion = a.FechaPublicacion
            });
        }

        public async Task<bool> UpdateAsignacionAsync(PeliculaSalaCineDTO dto)
        {
            var asignacionExistente = await _asignacionRepository.GetByIdAsync(dto.IdPeliculaSala);

            if (asignacionExistente == null)
            {
                return false; 
            }

            asignacionExistente.FechaPublicacion = dto.FechaPublicacion;
            asignacionExistente.FechaFin = dto.FechaFin;

            return await _asignacionRepository.SaveChangesAsync();
        }
    }
}
