using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica_Asesoftware_FMGS.API.Models;
using Prueba_Tecnica_Asesoftware_FMGS.DA.Context;
using Prueba_Tecnica_Asesoftware_FMGS.DA.Entities;
using System.Globalization;
using System.Text.Json;

namespace Prueba_Tecnica_Asesoftware_FMGS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ComercioController : ControllerBase
    {
        private readonly AsesoftwareDbContext _db;
        public ComercioController(AsesoftwareDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerComercios()
        {          
            try
            {
                var Comercios = await _db.comercios.ToListAsync();
                return Ok(MapearListadoComercios(Comercios));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ComercioDTO Request)
        {
            try
            {
                var Comercio = MapearComercio(Request);
                await _db.comercios.AddAsync(Comercio);
                await _db.SaveChangesAsync();
                return Ok(Comercio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditarComercio([FromBody] ComercioDTO Request)
        {
            try
            {
                var Comercio = await _db.comercios.FindAsync(Request.Id);
                if (Comercio == null)
                {
                    return NotFound($"No se encontró el comercio con el ID: {Request.Id}");
                }
                Comercio.nom_comercio = Request.Nombre;
                Comercio.aforo_maximo = Request.AforoMaximo;
                _db.Update(Comercio);
                await _db.SaveChangesAsync();
                return Ok(Comercio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        comercios MapearComercio(ComercioDTO Request)
        {
            return new comercios
            {
                id_comercio = 0,
                nom_comercio = Request.Nombre,
                aforo_maximo = Request.AforoMaximo
            };
        }

        List<ComercioDTO> MapearListadoComercios(List<comercios> Response)
        {
            var Listado = new List<ComercioDTO>();
            foreach (var item in Response)
            {
                Listado.Add(new ComercioDTO
                {
                    Id = item.id_comercio,

                    Nombre = item.nom_comercio,
                    AforoMaximo = item.aforo_maximo
                });
            }
            return Listado;
        }
    }
}
