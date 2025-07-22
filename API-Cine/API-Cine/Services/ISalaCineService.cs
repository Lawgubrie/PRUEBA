using API_Cine.Models.DTOs;

namespace API_Cine.Services
{
    public interface ISalaCineService
    {
        Task<IEnumerable<SalaCineDTO>> GetAllSalasCineAsync();
        Task<SalaCineDTO> GetSalaCineByIdAsync(int id);
        Task<SalaCineDTO> CreateSalaCineAsync(SalaCineCreateDTO salaCineCreateDto);
        Task<bool> UpdateSalaCineAsync(SalaCineUpdateDTO salaCineUpdateDto);
        Task<bool> DeleteSalaCineAsync(int id); 
        Task<VerificarDisponibilidadResponseDTO> VerificarDisponibilidadAsync(VerificarDisponibilidadRequestDTO request);

        Task<DashBoardDTO> GetDash();
    }
}
