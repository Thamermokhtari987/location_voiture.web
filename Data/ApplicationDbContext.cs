using Microsoft.EntityFrameworkCore;
using LocationVoitures.Web.Models;

namespace LocationVoitures.Web.Data
{
    /// <summary>
    /// Contexte de données principal de l'application (Site de LocationVoitures).
    /// Session par requête vers SQL Server LocalDB via <c>DbSet&lt;CategoryVoiture&gt;</c> et <c>DbSet&lt;Voiture&gt;</c>.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>Table « CategoriesVoitures » — Segments de véhicules (Citadine, Berline Élite, SUV, Commerciale).</summary>
        public DbSet<CategoryVoiture> CategoriesVoitures { get; set; }

        /// <summary>Table « Voitures » — Flotte de véhicules en location.</summary>
        public DbSet<Voiture> Voitures { get; set; }
    }
}
