namespace API_Cine.Models.DTOs
{
    public class VerificarDisponibilidadRequestDTO
    {
        public string NombreSala { get; set; }
    }

    public class VerificarDisponibilidadResponseDTO
    {
        public string Mensaje { get; set; }
    }

}
