using API_Cine.Contexts;
using API_Cine.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace API_Cine.Repository
{
    public class SalaCineRepository : ISalaCineRepository
    {
        private readonly CineDbContext _context;

        public SalaCineRepository(CineDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SalaCine>> GetAllActivasAsync()
        {
            return await _context.SalaCines.Where(s => s.Activo).ToListAsync();
        }

        public async Task<SalaCine> GetByIdAsync(int id)
        {
            return await _context.SalaCines.FindAsync(id);
        }

        public async Task AddAsync(SalaCine salaCine)
        {
            await _context.SalaCines.AddAsync(salaCine);
        }

        public void Update(SalaCine salaCine)
        {
            _context.SalaCines.Update(salaCine);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> numeroDePeliculasPorSala(int salaid) 
        {
            return await _context.PeliculaSalaCines.CountAsync(c => c.IdSalaCine == salaid);
        }


        public async Task<string> VerificarDisponibilidadSalaAsync(string nombreSala)
        {
            var connection = _context.Database.GetDbConnection();
            string resultadoMensaje = "Error: No se pudo obtener un resultado.";
            try
            {
                if (connection.State != ConnectionState.Open) await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "sp_VerificarDisponibilidadSala";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@NombreSala", nombreSala));
                    var result = await command.ExecuteScalarAsync();
                    if (result != null && result != DBNull.Value) resultadoMensaje = result.ToString();
                }
            }
            catch { }
            return resultadoMensaje;
        }
    }
}