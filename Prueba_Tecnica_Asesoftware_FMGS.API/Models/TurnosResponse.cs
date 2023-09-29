namespace Prueba_Tecnica_Asesoftware_FMGS.API.Models
{
    public class TurnosResponse
    {
        public long NumeroTurno { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
    }

    public class TurnosResponseDetalle
    {
        public string NombreServicio { get; set; } = null!;
        public int CantidadTurnos { get; set; }
        public List<TurnosResponse> Detalle { get; set; } = new List<TurnosResponse>();
    }
}
