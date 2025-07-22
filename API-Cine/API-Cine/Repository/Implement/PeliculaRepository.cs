using API_Cine.Contexts;
using API_Cine.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_Cine.Repository.Imp
{
    public class PeliculaRepository : IPeliculaRepository
    {
        protected readonly CineDbContext _context;

        public PeliculaRepository(CineDbContext context) { 

            _context = context;

        }

        public async Task<IEnumerable<Pelicula>> GetAllActivasAsync()
        {

            return await _context.Peliculas.Where(p => p.Activo).ToListAsync();
        }

        public async Task<Pelicula> GetByIdAsync(int id)
        {

            return await _context.Peliculas.FirstOrDefaultAsync(p => p.IdPelicula == id);
        }

        public async Task AddAsync(Pelicula pelicula)
        {
            await _context.Peliculas.AddAsync(pelicula);
        }

        public void Update(Pelicula pelicula)
        {
            _context.Peliculas.Update(pelicula);
        }

        public void Delete(Pelicula pelicula)
        {
            _context.Peliculas.Remove(pelicula);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Pelicula>> BuscarPeliculaNAsync(string nombre)
        {

            return await _context.Peliculas.Where(p => p.Activo).Where(p => p.Nombre.Contains(nombre)).ToListAsync();
        }



    }
}
