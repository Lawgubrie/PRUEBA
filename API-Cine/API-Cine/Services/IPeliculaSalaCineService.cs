using API_Cine.Models.DTOs;
using System.Threading.Tasks;

namespace API_Cine.Services
{
    public interface IPeliculaSalaCineService
    {
        Task<PeliculaSalaCineDTO> AsignarPeliculaASalaAsync(int idPelicula, int idSala, PeliculaSalaCineCreateDTO dto);
        Task<bool> UpdateAsignacionAsync(PeliculaSalaCineDTO dto);
        Task<bool> RemoverAsignacionAsync(int idAsignacion);
        Task<IEnumerable<PeliculaSalaCineDTO>> GetAllAsignacion();
        Task<IEnumerable<PeliculaSalaDetalleDTO>> GetByFechaPublicacion(DateOnly fecha);



    }
}
