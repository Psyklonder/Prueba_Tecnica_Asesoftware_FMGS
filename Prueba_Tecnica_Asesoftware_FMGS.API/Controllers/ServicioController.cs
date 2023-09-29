using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica_Asesoftware_FMGS.API.Models;
using Prueba_Tecnica_Asesoftware_FMGS.DA.Context;
using Prueba_Tecnica_Asesoftware_FMGS.DA.Entities;

namespace Prueba_Tecnica_Asesoftware_FMGS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ServicioController : ControllerBase
    {
        private readonly AsesoftwareDbContext _db;
        public ServicioController(AsesoftwareDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerServicios()
        {
            try
            {
                var Servicios = await _db.servicios.Include(x => x.id_comercioNavigation).ToListAsync();
                return Ok(MapearListadoServicios(Servicios));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ServicioDTO Request)
        {
            try
            {
                var Servicio = MapearServicio(Request);
                await _db.servicios.AddAsync(Servicio);
                await _db.SaveChangesAsync();
                return Ok(Servicio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        servicios MapearServicio(ServicioDTO Request)
        {
            return new servicios
            {
                id_servicio = 0,
                id_comercio = Request.IdComercio,
                nom_servicio = Request.Nombre,
                hora_apertura = ConvertirHora(Request.HoraApertura),
                hora_cierre = ConvertirHora(Request.HoraCierre),
                duracion = Request.Duracion
            };
        }
        private List<ServicioDTO> MapearListadoServicios(List<servicios> Response)
        {
            var Listado = new List<ServicioDTO>();
            foreach (var item in Response)
            {
                Listado.Add(new ServicioDTO
                {
                    Id = item.id_servicio,
                    IdComercio = item.id_comercio,
                    Nombre = item.nom_servicio,
                    HoraApertura = $"{item.hora_apertura.Hours}:{item.hora_apertura.Minutes}:{item.hora_apertura.Seconds}",
                    HoraCierre = $"{item.hora_cierre.Hours}:{item.hora_cierre.Minutes}:{item.hora_cierre.Seconds}",
                    Duracion = item.duracion,
                    NombreComercio = item.id_comercioNavigation.nom_comercio
                });
            }
            return Listado;
        }

        TimeSpan ConvertirHora(string Hora)
        {
            int hora = int.Parse(Hora.Split(":")[0]);
            int min = int.Parse(Hora.Split(":")[1]);
            int seg = int.Parse(Hora.Split(":")[2]);
            TimeSpan tiempo = new TimeSpan(hora, min, seg);
            return tiempo;
        }
    }
}
