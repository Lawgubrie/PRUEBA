using API_Cine.Contexts;
using API_Cine.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_Cine.Repository.Imp
{
    public class PeliculaSalaCineRepository:IPeliculaSalaCineRepository
    {
        private readonly CineDbContext _context;

        public PeliculaSalaCineRepository(CineDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PeliculaSalaCine>> GetAllAsync()
        {
            
            return await _context.PeliculaSalaCines.ToListAsync();
        }

        public async Task<PeliculaSalaCine> GetByIdAsync(int id)
        {
            return await _context.PeliculaSalaCines.FindAsync(id);
        }

        public async Task<IEnumerable<PeliculaSalaCine>> GetSDashboardsync()
        {

            return await _context.PeliculaSalaCines
                .Include(ps => ps.IdPeliculaNavigation)
                .Include(ps => ps.IdSalaCineNavigation)
                .ToListAsync();
        }

        public async Task AddAsync(PeliculaSalaCine asignacion)
        {
            await _context.PeliculaSalaCines.AddAsync(asignacion);
        }

        public void Update(PeliculaSalaCine asignacion)
        {
            _context.PeliculaSalaCines.Update(asignacion);
        }

        public void Delete(PeliculaSalaCine asignacion)
        {
            _context.PeliculaSalaCines.Remove(asignacion);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<PeliculaSalaCine>> GetFechaPelicula(DateOnly fecha)
        {
            return await _context.PeliculaSalaCines
                    .Where(ps => ps.FechaPublicacion == fecha)
                    .Include(ps => ps.IdPeliculaNavigation)
                    .Include(ps => ps.IdSalaCineNavigation)
                    .ToListAsync();
        }
    }
}
