using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocioController : ControllerBase
    {
        private BibliotecaContext _db;
        private readonly ILogger<SocioController> _logger;

        public SocioController(BibliotecaContext db, ILogger<SocioController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        [ResponseCache(CacheProfileName = "apicache")]
        public IActionResult GetSocios(/*int pagesize, int pagenumber*/)
        {
            _logger.LogInformation("Fetching Todas las Socio");
            var SocioList = _db.Socio.Where(s => s.NroSocio <= 100)
                .Include(y => y.Localidad)
                .Include(y => y.Calle)
                .Include(y => y.TipoDocumento)
                .Include(y => y.Profesion!)
                .Include(q => q.TipoSocio)
                .Include(r => r.CategoriaSocio)
                .Include(q => q.EstadoSocio)
                .OrderBy(s => s.ApeyNom).ToList();
                
            return Ok(SocioList);

        }

        [HttpGet("GetSociosFiltrados")]
        [Authorize]
        [ResponseCache(CacheProfileName = "apicache")]
        public IActionResult GetSociosFiltrados(int calle, int tipo, int categoria, int estado)
        {
            _logger.LogInformation("Fetching Todas las Socios Filtrados");
            var SocioList = _db.Socio//.Where(s => ((((s.Calle.Id == calle) && (calle!=0)) || (calle==0)) && (((s.TipoSocio.Id == tipo) && (tipo!=0)) || (tipo==0)) && (((s.CategoriaSocio.Id == categoria && categoria!=0) || (categoria==0)) && (((s.EstadoSocio.Id == estado) && (estado!=0))  || (estado==0)))))
                .Include(y => y.Localidad)
                .Include(y => y.Calle)
                .Include(y => y.TipoDocumento)
                .Include(y => y.Profesion!)
                .Include(q => q.TipoSocio)
                .Include(r => r.CategoriaSocio)
                .Include(q => q.EstadoSocio)
                .OrderBy(s => s.ApeyNom).ToList();

            return Ok(SocioList);

        }



        [HttpGet("GetSocioById")]
        [Authorize]
        [ResponseCache(CacheProfileName = "apicache")]
        public ActionResult<Socio> GetSocioById(int id)
        {

            if (id == 0)
            {
                _logger.LogError("Id de Socio no pasada");
                return BadRequest();
            }
            var Socio = _db.Socio.FirstOrDefault(x => x.Id == id);

            if (Socio == null)
            {
                return NotFound();
            }
            return Socio;
        }

        [HttpPost("AddSocio")]
        [Authorize]
        public ActionResult<Socio> AddSocio([FromBody] Socio socio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _db.Socio.Add(socio);
            _db.SaveChanges();
            return Ok(socio);

        }

        [HttpPost("UpdateSocio")]
        [Authorize]
        public ActionResult<Socio> UpdateSocio(Int32 Id, [FromBody] Socio socio)
        {
            if (socio == null)
            {
                return BadRequest(socio);
            }

            var Socio = _db.Socio.FirstOrDefault(x => x.Id == Id);
            if (Socio == null)
            {
                return NotFound();
            }

            Socio.ApeyNom = socio.ApeyNom;
            _db.SaveChanges();
            return Ok(Socio);

        }

        [HttpPut("DeleteSocio")]
        [Authorize(Roles = "Admin")]
        public ActionResult<Socio> DeleteSocio(Int32 Id)
        {
            var Socio = _db.Socio.FirstOrDefault(x => x.Id == Id);
            if (Socio == null)
            {
                return NotFound();
            }
            _db.Remove(Socio);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
