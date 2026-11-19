using System;
using System.Linq;
using LocationVoitures.Web.Models;
using LocationVoitures.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LocationVoitures.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVoitureRepository _voitureRepository;

        public HomeController(IVoitureRepository voitureRepository)
        {
            _voitureRepository = voitureRepository;
        }

        /// <summary>
        /// Vitrine publique dynamique : interroge la base de données pour afficher le catalogue réel
        /// (véhicules, catégories associées et statut disponibilité).
        /// </summary>
        public IActionResult Index()
        {
            var voitures = _voitureRepository.GetAll().ToList();

            var categories = voitures
                .Select(v => v.CategoryVoiture)
                .Where(c => c != null)
                .DistinctBy(c => c!.Id)
                .ToList();

            ViewBag.TotalCategories = categories.Count;
            ViewBag.TotalVoitures = voitures.Count;
            ViewBag.Disponibles = voitures.Count(v => v.EstDisponible);

            return View(voitures);
        }
    }
}
