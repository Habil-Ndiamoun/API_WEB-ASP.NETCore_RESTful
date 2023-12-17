using Gestion_Municipalites.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Municipalites.Data
{
    public class MunicipaliteContext : DbContext
    {
        public DbSet<Municipalite> Municipalites { get; set; }
        public DbSet<Election> Elections { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlServer(ConfigureConnectionString);
           
        }

        private static string? m_configureConnectionString;
        private static string? ConfigureConnectionString
        {
            get
            {
                if(string.IsNullOrWhiteSpace(m_configureConnectionString))
                {
                    var config = new ConfigurationBuilder()
                        .AddJsonFile(Path.Combine(Environment.CurrentDirectory, "appsettings.json"))
                        .Build();

                    m_configureConnectionString = config.GetSection("ConnectionStrings")["DefaultConnection"];
                }

                return m_configureConnectionString;
            }
        }
    }
}
