using System.ComponentModel.DataAnnotations;

namespace Prueba_Tecnica_Asesoftware_FMGS.API.Models
{
    public class ServicioDTO
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Comercio Requerido")]
        public int IdComercio { get; set; }

        [Required(ErrorMessage = "Nombre requerido")]
        [MaxLength(100, ErrorMessage = "El nombre del servicio no debe exceder los 100 caracteres")]
        public string Nombre { get; set; } = null!;

        //DEBE TENER FORMATO "hh:mm:ss"
        [Required(ErrorMessage = "Hora de apertura requerida")]
        [MaxLength(8, ErrorMessage = "Hora de apertura no tiene el formato correcto")]
        public string HoraApertura { get; set; } = null!;

        //DEBE TENER FORMATO "hh:mm:ss"
        [Required(ErrorMessage = "Hora de cierre requerido")]
        [MaxLength(8, ErrorMessage = "Hora de cierre no tiene el formato correcto")]
        public string HoraCierre { get; set; } = null!;
        public int Duracion { get; set; }
        public virtual string NombreComercio { get; set; } = null!;

    }
}
