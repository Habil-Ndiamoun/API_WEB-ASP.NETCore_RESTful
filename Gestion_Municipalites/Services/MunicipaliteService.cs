using Gestion_Municipalites.Data;
using Gestion_Municipalites.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Municipalites.Services
{
    public class MunicipaliteService
    {
        private readonly MunicipaliteContext? m_context;
        public MunicipaliteService(MunicipaliteContext? p_context)
        {
            m_context = p_context;
        }

        //1.Demander la liste complète des municipalités
        public IEnumerable<Municipalite> GetAll()
        {
            return m_context!.Municipalites
                    .AsNoTracking()
                    .Include(m => m.Elections)
                    .ToList();
        }

        //2.Demander une seule municipalité
        public Municipalite? GetById(int p_municipaliteId) 
        { 
            if(p_municipaliteId < 0)
            {
                throw new ArgumentOutOfRangeException("Le paramètre \"p_municipaliteId\" doit être supérieur à 0", nameof(p_municipaliteId));
            }

            return m_context!.Municipalites
                    .AsNoTracking()
                    .Include(m => m.Elections)
                    .FirstOrDefault(m => m.MunicipaliteId == p_municipaliteId);
        }

        //3.Ajouter une nouvelle municipalité
        public Municipalite Create(Municipalite p_nouvelleMunicipalite)
        {
            if(p_nouvelleMunicipalite is null)
            {
                throw new ArgumentNullException($"Le paramètre {p_nouvelleMunicipalite} ne peut pas être null", nameof(p_nouvelleMunicipalite));
            }

            if(m_context!.Municipalites.Any(m => m.MunicipaliteId == p_nouvelleMunicipalite.MunicipaliteId))
            {
                throw new InvalidOperationException($"La municipalité de numéro {p_nouvelleMunicipalite.MunicipaliteId} existe déjà dans la base de données!");
            }

            m_context.Municipalites.Add(p_nouvelleMunicipalite);
            m_context.SaveChanges();

            return p_nouvelleMunicipalite;
        }

        //4.Modifier une municipalité
        public Municipalite Update(Municipalite p_municipaliteContenantLesModifications)
        {
            if (p_municipaliteContenantLesModifications is null)
            {
                throw new ArgumentNullException($"Le paramètre {p_municipaliteContenantLesModifications} ne peut pas être null", nameof(p_municipaliteContenantLesModifications));
            }

            var municipaliteAModifier = GetById(p_municipaliteContenantLesModifications.MunicipaliteId);

            if (municipaliteAModifier == null)
            {
                throw new InvalidOperationException($"La municipalité de numéro {municipaliteAModifier!.MunicipaliteId} n'existe pas dans la base de données!");
            }

            m_context!.Municipalites.Update(p_municipaliteContenantLesModifications);
            m_context.SaveChanges();

            return p_municipaliteContenantLesModifications;
        }

        //5.Supprimer une municipalité
        public void DeleteById(int p_municipaliteId) 
        { 
            if(p_municipaliteId < 0)
            {
                throw new ArgumentOutOfRangeException("Le paramètre \"p_municipaliteId\" doit être supérieur à 0", nameof(p_municipaliteId));
            }

            var municipaliteASupprimer = GetById(p_municipaliteId);

            if(municipaliteASupprimer == null)
            {
                throw new InvalidOperationException($"La municipalité de numéro {municipaliteASupprimer!.MunicipaliteId} n'existe pas dans la base de données!");
            }

            m_context!.Municipalites.Remove(municipaliteASupprimer);
            m_context.SaveChanges();
        }
    }
}
