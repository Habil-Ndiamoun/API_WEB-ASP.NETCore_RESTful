﻿using Gestion_Municipalites.Data;
using Gestion_Municipalites.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Municipalites.Services
{
    public class MunicipaliteService
    {
        private readonly MunicipaliteContext? m_municipaliteContext;
        public MunicipaliteService(MunicipaliteContext? p_municipaliteContext)
        {
            m_municipaliteContext = p_municipaliteContext;
        }

        //1.Demander la liste complète des municipalités
        public IEnumerable<Municipalite> GetAll()
        {
            return m_municipaliteContext!.Municipalites
                    .AsNoTracking()
                    .Include(m => m.Elections)
                    .ToList();
        }

        //2.Demander une seule municipalité
        public Municipalite? GetById(int p_MunicipaliteId) 
        { 
            if(p_MunicipaliteId < 0)
            {
                throw new ArgumentOutOfRangeException("Le paramètre \"p_MunicipaliteId\" doit être supérieur à 0", nameof(p_MunicipaliteId));
            }

            return m_municipaliteContext!.Municipalites
                    .AsNoTracking()
                    .Include(m => m.Elections)
                    .FirstOrDefault(m => m.MunicipaliteId == p_MunicipaliteId);
        }

        //3.Ajouter une nouvelle municipalité
        public Municipalite Create(Municipalite p_nouvelleMunicipalite)
        {
            if(p_nouvelleMunicipalite is null)
            {
                throw new ArgumentNullException($"Le paramètre {p_nouvelleMunicipalite} ne peut pas être null", nameof(p_nouvelleMunicipalite));
            }

            if(m_municipaliteContext!.Municipalites.Any(m => m.MunicipaliteId == p_nouvelleMunicipalite.MunicipaliteId))
            {
                throw new InvalidOperationException($"La municipalité de numéro {p_nouvelleMunicipalite.MunicipaliteId} existe déjà dans la base de données!");
            }

            m_municipaliteContext.Municipalites.Add(p_nouvelleMunicipalite);
            m_municipaliteContext.SaveChanges();

            return p_nouvelleMunicipalite;
        }
    }
}