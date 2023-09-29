using System.ComponentModel.DataAnnotations;

namespace Prueba_Tecnica_Asesoftware_FMGS.API.Models
{
    public class ComercioDTO
    {
        public int? Id { get; set; }
        [Required(ErrorMessage ="Nombre requerido")]
        [MaxLength(100, ErrorMessage = "El nombre del comercio no debe exceder los 100 caracteres")]
        public string Nombre { get; set; } = null!;        
        public int AforoMaximo { get; set; }
    }
}
