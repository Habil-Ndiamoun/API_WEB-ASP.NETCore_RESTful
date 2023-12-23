using Gestion_Municipalites.Models;
using Gestion_Municipalites.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_Municipalites.Controllers
{
    [ApiController]
    [Route("municipalite/{municipaliteId}/elections")]
    public class ElectionController : ControllerBase
    {
        private readonly MunicipaliteService m_municipaliteService;
        private readonly ElectionService m_electionService;
        public ElectionController(MunicipaliteService p_municipaliteService, ElectionService p_service)
        {
            m_municipaliteService = p_municipaliteService;
           m_electionService = p_service;
        }

        //GET: municipalite/1/elections
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<Election>> GetAllElections(int municipaliteId)
        {
            var municipalite = m_municipaliteService.GetById(municipaliteId);

            if(municipalite == null) 
            {
                return NotFound();
            }
            else
            {
                return Ok(m_electionService.GetAllElections(municipaliteId));
            }
        }

        //GET: municipalite/1/elections/5
        [HttpGet("{electionId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Election> GetElectionById(int municipaliteId, int electionId)
        {
            var municipalite = m_municipaliteService.GetById(municipaliteId);
            var electionRecherchee = m_electionService.GetElectionById(municipaliteId, electionId);

            if(municipalite == null || electionRecherchee == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(electionRecherchee);
            }
        }

        //POST: municipalite/1/elections
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult Create(int municipaliteId, Election nouvelElection)
        {
            if(municipaliteId != nouvelElection.MunicipaliteId || !ModelState.IsValid) 
            {
                return BadRequest();
            }

            var election = m_electionService.Create(municipaliteId, nouvelElection);
            return CreatedAtAction(nameof(GetElectionById), new { id = election.ElectionId }, election);
        }

        //PUT: municipalite/1/elections/5
        [HttpPut("{electionId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Update(int municipaliteId, int electionId, Election election)
        {
            if (municipaliteId != election.MunicipaliteId || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var municipalite = m_municipaliteService.GetById(municipaliteId);
            var electionAModifier = m_electionService.GetElectionById(municipaliteId, electionId);

            if (municipalite == null || electionAModifier == null)
            {
                return NotFound();
            }

            m_electionService.Update(municipaliteId, electionId, election);
            return NoContent();
        }

        //DELETE: municipalite/1/elections/5
        [HttpDelete("{electionId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int municipaliteId, int electionId)
        {
            var municipalite = m_municipaliteService.GetById(municipaliteId);
            var electionASupprimer = m_electionService.GetElectionById(municipaliteId, electionId);

            if(municipalite == null || electionASupprimer == null)
            {
                return NotFound();
            }
            else
            {
                m_electionService.DeleteById(municipaliteId, electionId);
                return NoContent();
            }
        }
    }
}
