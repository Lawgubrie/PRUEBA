using API_Cine.Models.Entities;

namespace API_Cine.Repository
{
    public interface ISalaCineRepository
    {

        Task<IEnumerable<SalaCine>> GetAllActivasAsync();
        Task<SalaCine> GetByIdAsync(int id);
        Task AddAsync(SalaCine salaCine);
        void Update(SalaCine salaCine);
        Task<bool> SaveChangesAsync();

        Task<int> numeroDePeliculasPorSala(int salaid);
        Task<string> VerificarDisponibilidadSalaAsync(string nombreSala);

    }
}
