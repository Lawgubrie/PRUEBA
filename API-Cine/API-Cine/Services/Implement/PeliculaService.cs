using API_Cine.Models.DTOs;
using API_Cine.Models.Entities;
using API_Cine.Repository;

namespace API_Cine.Services.Imp
{
    public class PeliculaService : IPeliculaService
    {
        private readonly IPeliculaRepository _peliculaRepository;

        public PeliculaService(IPeliculaRepository peliculaRepository)
        {
            _peliculaRepository = peliculaRepository;
        }

        public async Task<IEnumerable<PeliculaDTO>> GetAllPeliculasAsync()
        {
            var peliculas = await _peliculaRepository.GetAllActivasAsync();
            return peliculas.Select(p => new PeliculaDTO
            {
                Id_Pelicula = p.IdPelicula,
                Nombre = p.Nombre,
                Duracion = p.Duracion,
                Imagen = p.Imagen,
                Activo = p.Activo
            });
        }

        public async Task<IEnumerable<PeliculaCreateDTO>> buscarNombre(string n)
        {
            var peliculas = await _peliculaRepository.BuscarPeliculaNAsync(n);
            return peliculas.Select(p => new PeliculaCreateDTO
            {
                Nombre = p.Nombre,
                Duracion = p.Duracion,
                Imagen = p.Imagen,
            });
        }

        public async Task<PeliculaDTO> GetPeliculaByIdAsync(int id)
        {
            var pelicula = await _peliculaRepository.GetByIdAsync(id);
            if (pelicula == null) return null;

            return new PeliculaDTO
            {
                Id_Pelicula = pelicula.IdPelicula,
                Nombre = pelicula.Nombre,
                Duracion = pelicula.Duracion,
                Imagen = pelicula.Imagen,
                Activo = pelicula.Activo
            };
        }

        public async Task<PeliculaDTO> CreatePeliculaAsync(PeliculaCreateDTO peliculaCreateDto)
        {
            var pelicula = new Pelicula
            {
                Nombre = peliculaCreateDto.Nombre,
                Duracion = peliculaCreateDto.Duracion,
                Imagen = peliculaCreateDto.Imagen,
                Activo = true 
            };

            await _peliculaRepository.AddAsync(pelicula);
            await _peliculaRepository.SaveChangesAsync();

       
            return new PeliculaDTO
            {
                Id_Pelicula = pelicula.IdPelicula,
                Nombre = pelicula.Nombre,
                Duracion = pelicula.Duracion,
                Imagen = pelicula.Imagen,
                Activo = pelicula.Activo
            };
        }

        public async Task<bool> UpdatePeliculaAsync(PeliculaUpdateDTO peliculaUpdateDto)
        {
            var peliculaExistente = await _peliculaRepository.GetByIdAsync(peliculaUpdateDto.Id_Pelicula);
            if (peliculaExistente == null)
            {
                return false;
            }

           
            peliculaExistente.Nombre = peliculaUpdateDto.Nombre;
            peliculaExistente.Duracion = peliculaUpdateDto.Duracion;
            peliculaExistente.Imagen = peliculaUpdateDto.Imagen;

            _peliculaRepository.Update(peliculaExistente);
            return await _peliculaRepository.SaveChangesAsync();
        }

        public async Task<bool> DeletePeliculaAsync(int id)
        {
            var peliculaExistente = await _peliculaRepository.GetByIdAsync(id);
            if (peliculaExistente == null)
            {
                return false; 
            }

           
            peliculaExistente.Activo = false;
            _peliculaRepository.Update(peliculaExistente);

            return await _peliculaRepository.SaveChangesAsync();
        }
    }
}
