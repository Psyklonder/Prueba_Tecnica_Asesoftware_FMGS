namespace Prueba_Tecnica_Asesoftware_FMGS.API.Models
{
    public class TurnosRequest
    {
        public string FechaInicio { get; set; } = null!;
        public string FechaFin { get; set; } = null!;
        public int IdServicio { get; set; }
    }
}
