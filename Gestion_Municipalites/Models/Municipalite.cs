using System;
using System.ComponentModel.DataAnnotations;

namespace Gestion_Municipalites.Models
{
    public class Municipalite
    {
        public int MunicipaliteId { get; set; }

        [MaxLength(100)]
        public string? NomMunicipalite { get; set;}

        [MaxLength(100)]
        public string?  AdresseCourriel { get; set; }
        public string? AdresseWeb { get; set; }
        public DateTime? DateProchaineElection { get; set; }
        public bool Actif { get; set; }
        public ICollection<Election>? Elections { get; set; }
    }
}
