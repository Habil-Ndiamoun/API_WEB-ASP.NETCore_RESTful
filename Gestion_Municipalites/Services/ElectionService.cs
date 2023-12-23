using Gestion_Municipalites.Data;
using Gestion_Municipalites.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Municipalites.Services
{
    public class ElectionService
    {
        private readonly MunicipaliteContext? m_context;
        private readonly MunicipaliteService? m_municipaliteService; 
        public ElectionService(MunicipaliteContext? p_context, MunicipaliteService? P_municipaliteService)
        {
            m_context = p_context;
            m_municipaliteService = P_municipaliteService;
        }

        //1. Demander la liste des élections d'une municipalité
        public IEnumerable<Election> GetAllElections(int p_municipaliteId)
        {
            if(p_municipaliteId < 0)
            {
                throw new ArgumentOutOfRangeException("Le paramètre \"p_municipaliteId\" doit être supérieur à 0", nameof(p_municipaliteId));
            }

            var municipalite = m_municipaliteService!.GetById(p_municipaliteId);
            if(municipalite == null)
            {
                throw new InvalidOperationException($"La municipalité de numéro {p_municipaliteId} n'existe pas dans la bd!");
            }

            return m_context!.Elections
                    .AsNoTracking()
                    .Where(e => e.MunicipaliteId == municipalite.MunicipaliteId)
                    .ToList();
        }

        //2.Demander une élection d'une municipalité
        public Election GetElectionById(int p_municipaliteId, int p_electionRechercheeId) 
        {
            if (p_municipaliteId < 0)
            {
                throw new ArgumentOutOfRangeException("Le paramètre \"p_municipaliteId\" doit être supérieur à 0", nameof(p_municipaliteId));
            }
            if (p_electionRechercheeId < 0)
            {
                throw new ArgumentOutOfRangeException("Le paramètre \"p_electionRechercheeId\" doit être supérieur à 0", nameof(p_electionRechercheeId));
            }

            var municipalite = m_municipaliteService!.GetById(p_municipaliteId);
            var electionRecherchee = m_context!.Elections.SingleOrDefault(e => e.ElectionId == p_electionRechercheeId && e.MunicipaliteId == p_municipaliteId);

            if (municipalite == null || electionRecherchee == null)
            {
                throw new InvalidOperationException($"La municipalité de numéro {p_municipaliteId} ou l'election de numéro {p_electionRechercheeId} n'existe pas dans la bd!");
            }

            return electionRecherchee;
        }

        //3.Ajouter une nouvelle élection dans une municipalité
        public Election Create(int p_municipaliteId, Election p_electionAAjouter)
        {
            if (p_municipaliteId < 0)
            {
                throw new ArgumentOutOfRangeException("Le paramètre \"p_municipaliteId\" doit être supérieur à 0", nameof(p_municipaliteId));
            }

            if(p_electionAAjouter == null)
            {
                throw new ArgumentNullException($"Le paramètre {p_electionAAjouter} ne peut pas être null", nameof(p_electionAAjouter));
            }

            if(!m_context!.Municipalites.Any(m => m.MunicipaliteId == p_municipaliteId))
            {
                throw new InvalidOperationException($"La municipalité de numéro {p_municipaliteId} n'existe pas dans la base de données!");
            }

            if(m_context!.Elections.Any(e => e.ElectionId == p_electionAAjouter.ElectionId && e.MunicipaliteId == p_municipaliteId))
            {
                throw new InvalidOperationException($"L'élection de numéro {p_electionAAjouter.ElectionId} existe déjà dans la base de données!");
            }

            m_context!.Elections.Add(p_electionAAjouter);
            m_context.SaveChanges();

            return p_electionAAjouter;
        }

        //4.Modifier une élection dans une municipalité
        public Election Update(int p_municipaliteId, int p_electionId, Election p_electionContenantLesModifications)
        {
            if (p_municipaliteId < 0)
            {
                throw new ArgumentOutOfRangeException("Le paramètre \"p_municipaliteId\" doit être supérieur à 0", nameof(p_municipaliteId));
            }
            if (p_electionId < 0)
            {
                throw new ArgumentOutOfRangeException("Le paramètre \"p_electionId\" doit être supérieur à 0", nameof(p_electionId));
            }

            if (p_electionContenantLesModifications == null)
            {
                throw new ArgumentNullException($"Le paramètre {p_electionContenantLesModifications} ne peut pas être null", nameof(p_electionContenantLesModifications));
            }

            if (!m_context!.Municipalites.Any(m => m.MunicipaliteId == p_municipaliteId))
            {
                throw new InvalidOperationException($"La municipalité de numéro {p_municipaliteId} n'existe pas dans la base de données!");
            }

            var electionAAmodifier = GetElectionById(p_municipaliteId, p_electionContenantLesModifications.ElectionId);
            if (electionAAmodifier == null)
            {
                throw new InvalidOperationException($"L'election de numéro {electionAAmodifier!.ElectionId} n'existe pas dans la bd!");
            }

            m_context.Elections.Update(p_electionContenantLesModifications);
            m_context.SaveChanges();

            return p_electionContenantLesModifications;
        }


        //5.Supprimer une élection dans une municipalité
        public void DeleteById(int p_municipaliteId, int p_electionASupprimerId)
        {
            if (p_municipaliteId < 0)
            {
                throw new ArgumentOutOfRangeException("Le paramètre \"p_municipaliteId\" doit être supérieur à 0", nameof(p_municipaliteId));
            }

            if (p_electionASupprimerId < 0)
            {
                throw new ArgumentOutOfRangeException("Le paramètre \"p_electionASupprimerId\" doit être supérieur à 0", nameof(p_electionASupprimerId));
            }

            var municipalite = m_municipaliteService!.GetById(p_municipaliteId);
            var electionASupprimer = GetElectionById(p_municipaliteId, p_electionASupprimerId);

            if (municipalite == null || electionASupprimer == null) 
            {
                throw new InvalidOperationException($"La municipalité de numéro {p_municipaliteId} ou l'election de numéro {p_electionASupprimerId} n'existe pas dans la bd!");
            }

            m_context!.Elections.Remove(electionASupprimer); 
            m_context.SaveChanges();
        }
    }
}
