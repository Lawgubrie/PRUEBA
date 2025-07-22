namespace API_Cine.Models.Entities;

public partial class SalaCine
{
    public int IdSala { get; set; }

    public string Nombre { get; set; } = null!;

    public int? Estado { get; set; }

    public string Tipo { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual ICollection<PeliculaSalaCine> PeliculaSalaCines { get; set; } = new List<PeliculaSalaCine>();
}
