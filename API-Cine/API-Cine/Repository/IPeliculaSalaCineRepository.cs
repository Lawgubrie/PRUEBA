using API_Cine.Models.Entities;

namespace API_Cine.Repository
{
    public interface IPeliculaSalaCineRepository
    {
        Task<IEnumerable<PeliculaSalaCine>> GetAllAsync();
        Task<PeliculaSalaCine> GetByIdAsync(int id);
        Task<IEnumerable<PeliculaSalaCine>> GetSDashboardsync();
        Task AddAsync(PeliculaSalaCine asignacion);
        void Update(PeliculaSalaCine asignacion);
        void Delete(PeliculaSalaCine asignacion);
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<PeliculaSalaCine>> GetFechaPelicula(DateOnly fecha);


    }
}
