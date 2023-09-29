using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica_Asesoftware_FMGS.API.Models;
using Prueba_Tecnica_Asesoftware_FMGS.DA.Context;
using Prueba_Tecnica_Asesoftware_FMGS.DA.Entities;
using System.Globalization;

namespace Prueba_Tecnica_Asesoftware_FMGS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TurnosController : ControllerBase
    {
        private readonly AsesoftwareDbContext _db;
        public TurnosController(AsesoftwareDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> CrearTurnos([FromBody] TurnosRequest Request)
        {
            try
            {
                var Validacion = ValidarFechas(Request.FechaInicio, Request.FechaFin, out DateTime? FechaI, out DateTime? FechaF);
                if (!Validacion)
                {
                    return BadRequest("Por favor ingrese los rangos de fechas correctamente");
                }
                var ExisteServicio = await _db.servicios.Include(x => x.id_comercioNavigation).Include(x => x.turnos).Where(x => x.id_servicio == Request.IdServicio).FirstOrDefaultAsync();
                if (ExisteServicio == null)
                {
                    return NotFound("El servicio suministrado no existe en la base de datos");
                }

                if (ValidarAforoComercio(ExisteServicio))
                {
                    var Response = await _db.turnos.FromSqlRaw(@$"[dbo].[AgregarTurnos] {Request.IdServicio}, 
                                '{FechaI.Value.Year}-{FechaI.Value.Month}-{FechaI.Value.Day}', 
                                '{FechaF.Value.Year}-{FechaF.Value.Month}-{FechaF.Value.Day}'").ToListAsync();

                    var Detalle = new TurnosResponseDetalle
                    {
                        NombreServicio = ExisteServicio.nom_servicio,
                        CantidadTurnos = Response.Count,
                        Detalle = MapearTuenos(Response)
                    };
                    return Ok(Detalle);
                }
                else
                {
                    return Conflict($"No se pueden agregar mas turnos al comercio: {ExisteServicio.id_comercioNavigation.nom_comercio}");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        bool ValidarFechas(string FechaInicio, string FechaFin, out DateTime? FechaIResult, out DateTime? FechaFResult)
        {
            try
            {
                DateTime FechaI = DateTime.ParseExact(FechaInicio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime FechaF = DateTime.ParseExact(FechaFin, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if ((FechaF - FechaI).TotalMilliseconds < 0)
                {
                    FechaIResult = null;
                    FechaFResult = null;
                    return false;
                }
                FechaIResult = FechaI;
                FechaFResult = FechaF;
                return true;
            }
            catch
            {
                FechaIResult = null;
                FechaFResult = null;
                return false;
            }
        }

        List<TurnosResponse> MapearTuenos(List<turnos> Response)
        {
            var Turnos = new List<TurnosResponse>();
            foreach (var turno in Response)
            {
                Turnos.Add(new TurnosResponse
                {
                    NumeroTurno = turno.id_turno,
                    FechaInicial = turno.fecha_turno + turno.hora_inicio,
                    FechaFinal = turno.fecha_turno + turno.hora_fin
                });
            }
            return Turnos;
        }

        bool ValidarAforoComercio(servicios servicio)
        {
            int AforoMaximo = servicio.id_comercioNavigation.aforo_maximo;
            int TotalTurnos = servicio.turnos.Where(x => x.estado).ToList().Count;
            if (AforoMaximo - TotalTurnos >= 0)
            {
                return true;
            }
            return false;
        }
    }
}
