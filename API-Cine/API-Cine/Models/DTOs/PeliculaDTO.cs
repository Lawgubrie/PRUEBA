namespace API_Cine.Models.DTOs
{
    public class PeliculaDTO
    {
        public int Id_Pelicula { get; set; }
        public string Nombre { get; set; }
        public int Duracion { get; set; }
        public string? Imagen { get; set; }
        public bool Activo { get; set; }
    }

    public class PeliculaCreateDTO
    {
        public string Nombre { get; set; }
        public int Duracion { get; set; }
        public string? Imagen { get; set; }
    }
    public class PeliculaUpdateDTO
    {
        public int Id_Pelicula { get; set; }
        public string Nombre { get; set; }
        public int Duracion { get; set; }
        public string? Imagen { get; set; }

    }
}
