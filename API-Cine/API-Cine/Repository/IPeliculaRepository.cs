using API_Cine.Models.Entities;

namespace API_Cine.Repository
{
    public interface IPeliculaRepository
    {

        Task<IEnumerable<Pelicula>> GetAllActivasAsync();
        Task<Pelicula> GetByIdAsync(int id); 
        Task AddAsync(Pelicula pelicula);
        void Update(Pelicula pelicula);
        void Delete(Pelicula pelicula);
        Task<IEnumerable<Pelicula>> BuscarPeliculaNAsync(string nombre);
        Task<bool> SaveChangesAsync();

    }
}
