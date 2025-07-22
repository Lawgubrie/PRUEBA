using API_Cine.Models.DTOs;
using API_Cine.Models.Entities;
using API_Cine.Repository;

namespace API_Cine.Services.Imp
{
    public class SalaCineService : ISalaCineService
    {
        private readonly ISalaCineRepository _salaCineRepository;
        private readonly IPeliculaRepository _peliculaRepo;

        public SalaCineService(ISalaCineRepository salaCineRepository, IPeliculaRepository peliculaRepo)
        {
            _salaCineRepository = salaCineRepository;
            _peliculaRepo = peliculaRepo;
        }

        public async Task<VerificarDisponibilidadResponseDTO> VerificarDisponibilidadAsync(VerificarDisponibilidadRequestDTO request)
        {
            var mensaje = await _salaCineRepository.VerificarDisponibilidadSalaAsync(request.NombreSala);
            return new VerificarDisponibilidadResponseDTO { Mensaje = mensaje };
        }

        public async Task<IEnumerable<SalaCineDTO>> GetAllSalasCineAsync()
        {
            var salas = await _salaCineRepository.GetAllActivasAsync();
            return salas.Select(s => new SalaCineDTO
            {
                Id_Sala = s.IdSala,
                Nombre = s.Nombre,
                Estado = s.Estado,
                Tipo = s.Tipo,
                Activo = s.Activo
            });
        }

        public async Task<SalaCineDTO> GetSalaCineByIdAsync(int id)
        {
            var sala = await _salaCineRepository.GetByIdAsync(id);
            if (sala == null) return null;

            return new SalaCineDTO
            {
                Id_Sala = sala.IdSala,
                Nombre = sala.Nombre,
                Estado = sala.Estado,
                Tipo = sala.Tipo,
                Activo = sala.Activo
            };
        }

        public async Task<SalaCineDTO> CreateSalaCineAsync(SalaCineCreateDTO salaCineCreateDto)
        {
            var sala = new SalaCine
            {
                Nombre = salaCineCreateDto.Nombre,
                Estado = salaCineCreateDto.Estado,
                Tipo = salaCineCreateDto.Tipo,
                Activo = true
            };

            await _salaCineRepository.AddAsync(sala);
            await _salaCineRepository.SaveChangesAsync();

            return new SalaCineDTO
            {
                Id_Sala = sala.IdSala,
                Nombre = sala.Nombre,
                Estado = sala.Estado,
                Tipo = sala.Tipo,
                Activo = sala.Activo
            };
        }

        public async Task<bool> UpdateSalaCineAsync(SalaCineUpdateDTO salaCineUpdateDto)
        {
            var salaExistente = await _salaCineRepository.GetByIdAsync(salaCineUpdateDto.Id_Sala);
            if (salaExistente == null) return false;

            salaExistente.Nombre = salaCineUpdateDto.Nombre;
            salaExistente.Estado = salaCineUpdateDto.Estado;
            salaExistente.Tipo = salaCineUpdateDto.Tipo;

            _salaCineRepository.Update(salaExistente);
            return await _salaCineRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteSalaCineAsync(int id)
        {
            var salaExistente = await _salaCineRepository.GetByIdAsync(id);
            if (salaExistente == null) return false;

            salaExistente.Activo = false;
            _salaCineRepository.Update(salaExistente);
            return await _salaCineRepository.SaveChangesAsync();
        }

        public async Task<DashBoardDTO> GetDash() {

            var salas = await _salaCineRepository.GetAllActivasAsync();
            var totalS = salas.Count();

            var salasDis = new List<SalaCine>();
            foreach (var sala in salas)
            {
                var count = await _salaCineRepository.numeroDePeliculasPorSala(sala.IdSala);
                if (count <= 3) 
                {
                    salasDis.Add(sala);
                }
            }

            var peliculas = await _peliculaRepo.GetAllActivasAsync();
            var totalP = peliculas.Count();

            return new DashBoardDTO
            {

                totalDeSalas = totalS,
                SalasDisponibles = salasDis.Count(),
                totalPeliculas = totalP
            };

        }
    }
}
