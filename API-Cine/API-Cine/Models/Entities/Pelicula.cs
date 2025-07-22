using System.Text.Json.Serialization;

namespace API_Cine.Models.Entities;

public partial class Pelicula
{
    public int IdPelicula { get; set; }

    public string Nombre { get; set; } = null!;

    public int Duracion { get; set; }

    public string? Imagen { get; set; }

    public bool Activo { get; set; }


    [JsonIgnore]
    public virtual ICollection<PeliculaSalaCine> PeliculaSalaCines { get; set; } = new List<PeliculaSalaCine>();
}
