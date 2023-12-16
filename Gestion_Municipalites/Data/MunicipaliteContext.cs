using Gestion_Municipalites.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Municipalites.Data
{
    public class MunicipaliteContext : DbContext
    {
        public MunicipaliteContext(DbContextOptions<MunicipaliteContext> options) : base(options)
        {
            
        }

        public DbSet<Municipalite> Municipalites { get; set; }
        public DbSet<Election> Elections { get; set; }
    }
}
