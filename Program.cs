using LocationVoitures.Web.Data;
using LocationVoitures.Web.Models;
using LocationVoitures.Web.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// =========================================================================
// 1. SERVICES & INJECTION DEPENDANCES (DI)
// =========================================================================
builder.Services.AddControllersWithViews();

// EF Core 10.0.x — SQL Server LocalDB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories (cycle de vie Scoped) — injectés par constructeur dans les contrôleurs
builder.Services.AddScoped<ICategoryVoitureRepository, CategoryVoitureRepository>();
builder.Services.AddScoped<IVoitureRepository, VoitureRepository>();

var app = builder.Build();

// Migrations (équivalent PMC : Add-Migration / Update-Database) + amorçage si le catalogue est vide
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();

    if (!context.CategoriesVoitures.Any())
    {
        SeedDatabase(context);
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();

// --- AMORÇAGE — données de démonstration du TP Partie 4 (≥ 3 catégories & ≥ 4 véhicules) ---
static void SeedDatabase(ApplicationDbContext context)
{
    var categories = new CategoryVoiture[]
    {
        new() { Libelle = "Citadine", Description = "Parfaite pour les déplacements urbains et les stations trottoir. Du confort dans un espace compact.", ImageUrl = "/uploads/categories/category_1.svg" },
        new() { Libelle = "Berline Élite", Description = "Confort premium pour les longs trajets. Espace tronçon haut de gamme pour une conduite exceptionnelle.", ImageUrl = "/uploads/categories/category_2.svg" },
        new() { Libelle = "SUV & Tout-Terrain", Description = "Polyvalence maximale sur route et piste. L'espace et la puissance pour l'aventure familiale.", ImageUrl = "/uploads/categories/category_3.svg" },
        new() { Libelle = "Commerciale & Transports", Description = "Grands volumes pour les clients professionnels. Capacité élevée de chargement et de volant de rodamont.", ImageUrl = "/uploads/categories/category_4.svg" }
    };
    context.CategoriesVoitures.AddRange(categories);
    context.SaveChanges();

    // Corriger l'URL de l'image selon l'ID réel (nommage TP : /uploads/categories/category_<id>.svg)
    foreach (var c in categories)
    {
        c.ImageUrl = $"/uploads/categories/category_{c.Id}.svg";
    }
    context.SaveChanges();

    var voitures = new Voiture[]
    {
        new() { Marque = "Renault", Modele = "Clio 5", Matricule = "235 TUN 1010", Annee = 2025, PrixJour = 88.50m, EstDisponible = true, CategoryVoitureId = 1 },
        new() { Marque = "Peugeot", Modele = "208", Matricule = "220 TUN 2020", Annee = 2022, PrixJour = 96.00m, EstDisponible = true, CategoryVoitureId = 1 },
        new() { Marque = "Toyota", Modele = "Aygo X", Matricule = "201 TUN 3344", Annee = 2021, PrixJour = 79.90m, EstDisponible = true, CategoryVoitureId = 1 },
        new() { Marque = "Renault", Modele = "Symbol", Matricule = "180 TUN 5110", Annee = 2019, PrixJour = 134.20m, EstDisponible = true, CategoryVoitureId = 2 },
        new() { Marque = "Peugeot", Modele = "508 GT", Matricule = "340 TUN 7720", Annee = 2021, PrixJour = 187.50m, EstDisponible = true, CategoryVoitureId = 2 },
        new() { Marque = "BMW", Modele = "X5", Matricule = "501 TUN 9003", Annee = 2020, PrixJour = 302.40m, EstDisponible = true, CategoryVoitureId = 3 },
        new() { Marque = "Audi", Modele = "Q5", Matricule = "210 TUN 5555", Annee = 2019, PrixJour = 268.80m, EstDisponible = true, CategoryVoitureId = 3 },
        new() { Marque = "Ford", Modele = "Kuga", Matricule = "103 TUN 6641", Annee = 2022, PrixJour = 221.40m, EstDisponible = false, CategoryVoitureId = 3 },
        new() { Marque = "Chevrolet", Modele = "Silverado", Matricule = "400 TUN 1122", Annee = 2021, PrixJour = 345.00m, EstDisponible = true, CategoryVoitureId = 4 },
        new() { Marque = "Volvo", Modele = "V40", Matricule = "251 TUN 3305", Annee = 2020, PrixJour = 224.60m, EstDisponible = true, CategoryVoitureId = 4 }
    };
    context.Voitures.AddRange(voitures);
    context.SaveChanges();
}
