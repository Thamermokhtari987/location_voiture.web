using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace LocationVoitures.Web.Models
{
    public class CategoryVoiture
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le libellé de la catégorie est obligatoire.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Le libellé doit comporter entre 2 et 50 caractères.")]
        [Display(Name = "Libellé de la catégorie")]
        public string Libelle { get; set; } = string.Empty;

        [StringLength(250, ErrorMessage = "La description ne peut pas dépasser 250 caractères.")]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "URL de l'image")]
        public string? ImageUrl { get; set; }

        /// <summary>
        /// Propriété de transport pour recevoir le fichier téléversé depuis le formulaire HTTP (multipart/form-data).
        /// [NotMapped] => EF Core ne crée aucune colonne en base pour cette propriété complexe.
        /// </summary>
        [NotMapped]
        [Display(Name = "Photo illustrative")]
        public IFormFile? ImageFile { get; set; }

        /// <summary>
        /// Propriété de navigation : les véhicules du segment. Complète la relation 1-N (inverse de Voiture.CategoryVoitureId).
        /// </summary>
        public ICollection<Voiture> Voitures { get; set; } = new List<Voiture>();
    }
}
