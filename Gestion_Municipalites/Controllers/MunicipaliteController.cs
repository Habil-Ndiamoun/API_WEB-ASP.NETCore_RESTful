using Gestion_Municipalites.Models;
using Gestion_Municipalites.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_Municipalites.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MunicipaliteController : ControllerBase
    {
        private readonly MunicipaliteService? m_municipaliteService;
        public MunicipaliteController(MunicipaliteService? p_municipaliteService)
        {
            m_municipaliteService = p_municipaliteService;
        }

        // GET: municipalite
        [HttpGet]
        [ProducesResponseType(200)]
        public IEnumerable<Municipalite> GetAll()
        {
            return m_municipaliteService!.GetAll();
        }

        // GET: municipalite/5
        [HttpGet("{municipaliteId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Municipalite> GetById(int municipaliteId) 
        {
            var municipaliteRecherchee = m_municipaliteService!.GetById(municipaliteId);

            if(municipaliteRecherchee is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(municipaliteRecherchee);
            }
        }

        // POST: municipalite
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult Create(Municipalite nouvelleMunicipalite)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var municipalite = m_municipaliteService!.Create(nouvelleMunicipalite);
            return CreatedAtAction(nameof(GetById), new { id = municipalite.MunicipaliteId }, municipalite);
        }


    }
}
