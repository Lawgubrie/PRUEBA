namespace API_Cine.Models.DTOs
{
    public class PeliculaSalaCineDTO
    {
        public int IdPeliculaSala { get; set; }

        public int IdPelicula { get; set; }

        public int IdSalaCine { get; set; }

        public DateOnly FechaPublicacion { get; set; }

        public DateOnly FechaFin { get; set; }
    }

    public class PeliculaSalaCineCreateDTO
    {

        public int IdPelicula { get; set; }

        public int IdSalaCine { get; set; }

        public DateOnly FechaPublicacion { get; set; }

        public DateOnly FechaFin { get; set; }
    }

    public class PeliculaSalaDetalleDTO
    {
        public string NombrePelicula { get; set; }
        public string NombreSala { get; set; }
        public string TipoSala { get; set; }
        public DateOnly FechaPublicacion { get; set; }
    }


}
