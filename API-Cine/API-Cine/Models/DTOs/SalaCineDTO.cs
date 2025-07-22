namespace API_Cine.Models.DTOs
{
    public class SalaCineDTO
    {
        public int Id_Sala { get; set; }
        public string Nombre { get; set; }
        public int? Estado { get; set; }
        public string Tipo { get; set; }
        public bool Activo { get; set; }
    }

    public class SalaCineCreateDTO
    {
        public string Nombre { get; set; }
        public int? Estado { get; set; }
        public string Tipo { get; set; }
    }


    public class SalaCineUpdateDTO
    {
        public int Id_Sala { get; set; }
        public string Nombre { get; set; }
        public int? Estado { get; set; }
        public string Tipo { get; set; }
    }
}
