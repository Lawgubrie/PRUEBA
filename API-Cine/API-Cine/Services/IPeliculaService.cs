using API_Cine.Models.DTOs;

namespace API_Cine.Services
{
    public interface IPeliculaService
    {
        Task<IEnumerable<PeliculaDTO>> GetAllPeliculasAsync();
        Task<PeliculaDTO> GetPeliculaByIdAsync(int id);
        Task<PeliculaDTO> CreatePeliculaAsync(PeliculaCreateDTO peliculaCreateDto);
        Task<bool> UpdatePeliculaAsync(PeliculaUpdateDTO peliculaUpdateDto);
        Task<bool> DeletePeliculaAsync(int id);
        Task<IEnumerable<PeliculaCreateDTO>> buscarNombre(string nombre);
    }
}
