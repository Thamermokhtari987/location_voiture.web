# ðŸš— LocationVoitures â€” Agence de location de voitures

Site web **ASP.NET Core 10 MVC (.NET 10)** â€” suite complÃ¨te du TP SÃ©ance 2 : persistance SQL Server LocalDB, **Repository Pattern**, tÃ©lÃ©versement d'images et front vitrine dynamique.

Votre agence **LocationVoitures** met en vente une flotte dynamique : homepage vitrine exploitant la base, back-office complet pour gÃ©rer les **catÃ©gories** et la **flotte de vÃ©hicules**, avec upload d'images sur disque.

## ðŸ–¸ Captures d'Ã©cran

| Page | Description |
| :-- | :-- |
| <img src="screenshots/01-home.jpeg" width="200" alt="Accueil"> | **Accueil vitrine dynamique** â€” Hero animÃ©, stats en temps rÃ©el (4 catÃ©gories, 10 vÃ©hicules, 9 disponibles) et grille de cartes de vÃ©hicules |
| <img src="screenshots/02-voiture.jpeg" width="200" alt="Flotte"> | **Flotte de vÃ©hicules** â€” Back-office avec tableau dynamique : vÃ©hicule, matricule, catÃ©gorie (chargÃ©e via `.Include`), annÃ©e, tarif et badge disponibilitÃ© |
| <img src="screenshots/03-categories.jpeg" width="200" alt="CatÃ©gories"> | **Gestion des catÃ©gories** â€” Back-office avec tableau, images et dÃ©tails des catÃ©gories |

## ðŸ› ï¸ Stack technique

- **ASP.NET Core 10.0 MVC** (.NET 10, C#)
- **Entity Framework Core 10.0.15** avec provider **SqlServer** + **LocalDB** (`(localdb)\mssqllocaldb`)
- **Repository Pattern** : `@@IREPOCAT@@` + `@@REPO@@` (+ `@@VIREPOCAT@@` + `@@VREPOCAT@@` avec `Include`)
- **Relation 1-N** : clÃ© Ã©trangÃ¨re `@@FKID@@` sur `Voiture` (FK) + propriÃ©tÃ© de navigation `Voitures` (inverse de `CategoryVoiture.Voitures`)
- **Upload d'images** : `IFormFile` â†’ Ã©criture sur disque `wwwroot/uploads/categories/category_<id>.<ext>` + URL relative en base

## âš™ï¸ Installation & Lancement

```bash
# Lancement direct :
dotnet run --urls https://localhost:5051
```

Ou avec **Visual Studio 2022** : bouton **DÃ©marrer (F5)** â†’ page `https://localhost:5051/`.

> L'application dÃ©marre en **Development**, amorce le schÃ©ma SQL (`Database.Migrate()`) et amorce les donnÃ©es de dÃ©monstration si le catalogue est vide (4 catÃ©gories + 10 vÃ©hicules, cf. `Program.cs`).

## ðŸ—ƒï¸ Base de donnÃ©es LocalDB

ChaÃ®ne de connexion (`appsettings.json`) :

```
Server=(localdb)\mssqllocaldb;Database=LocationVoituresDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True
```

Commandes EF (SÃ©ance 2 Partie 2 & 3) :

```bash
# PMC
Add-Migration InitialCreate
Update-Database

# CLI
dotnet ef migrations add InitialCreate
dotnet ef database update
```

> âš ï¸ Note : une base `LocationVoituresDb` existait dÃ©jÃ  dans LocalDB avec une migration antÃ©rieure (`InitialCreateCategory`). Elle a Ã©tÃ© recrÃ©e proprement avec `InitialCreate` + seed.

## ðŸ§ª VÃ©rifications effectuÃ©es (SÃ©ance 2)

- [x] Projet MVC (.NET 10) + EF Core 10
- [x] Packages NuGet : `SqlServer`, `Tools`, `Design`
- [x] ChaÃ®ne de connexion LocalDB dans `appsettings.json`
- [x] `ApplicationDbContext` : `DbSet<CategoryVoiture>` (`@@DBSET1@@`) + `DbSet<Voiture>` (`@@DBSET2@@`), enregistrÃ© dans `Program.cs`
- [x] Repository Pattern : `@@IREPOCAT@@` + `@@REPO@@` (GetAll / GetById / Add / Update / Delete / Exists) + `@@VIREPOCAT@@` + `@@VREPOCAT@@` avec `.Include`
- [x] ModÃ¨le `CategoryVoiture` (identifiant, libellÃ©, description, URL image, collection de navigation)
- [x] Relation 1-N : `Voiture.@@FKID@@` (FK) â€” `Voitures` sur `CategoryVoiture`
- [x] Upload d'images (Create / Edit) : `IFormFile` â†’ Ã©criture sur disque `category_<id>` + URL relative en base
- [x] Vues Razor complÃ¨tes : Index / Create / Edit / Details / Delete pour catÃ©gories et voitures avec validation
- [x] Liens de navigation mis Ã  jour dans `Views/Shared/_Layout.cshtml` : Accueil / Flotte / CatÃ©gories
- [x] Page d'accueil (Home) dynamique â€” vÃ©hicules prÃ©sents en base
- [x] Migrations Code-First + `Update-Database` LocalDB
- [x] Tests CRUD â€” via navigateur : crÃ©ation, modification, suppression sans erreur de console

## ðŸ“ Structure du projet

```
LocationVoitures.Web/
â”œâ”€â”€ Program.cs                      # DI + Database.Migrate() + seed
â”œâ”€â”€ appsettings.json                # Connexion LocalDB
â”œâ”€â”€ Controllers/
â”‚   â”œâ”€â”€ HomeController.cs
â”‚   â”œâ”€â”€ @@CATCTRL@@                 # CRUD catÃ©gories + upload images
â”‚   â””â”€â”€ @@VOICTRL@@                 # CRUD flotte (+ SelectList catÃ©gories)
â”œâ”€â”€ Data/
â”‚   â””â”€â”€ ApplicationDbContext.cs     # DbSet<CategoryVoiture>, DbSet<Voiture>
â”œâ”€â”€ Models/
â”‚   â”œâ”€â”€ CategoryVoiture.cs
â”‚   â””â”€â”€ Voiture.cs
â”œâ”€â”€ Repositories/
â”‚   â”œâ”€â”€ @@IREPOCAT@@.cs + @@REPO@@.cs
â”‚   â””â”€â”€ @@VIREPOCAT@@.cs + @@VREPOCAT@@.cs (Include)
â”œâ”€â”€ Views/
â”‚   â”œâ”€â”€ Home/Index.cshtml           # Vitrine dynamique
â”‚   â”œâ”€â”€ @@CATFOLDER@@/{Index, Create, Edit, Details, Delete}.cshtml
â”‚   â”œâ”€â”€ @@VOIFOLDER@@/{Index, Create, Edit, Details, Delete}.cshtml
â”‚   â””â”€â”€ Shared/_Layout.cshtml       # Navbar + footer
â”œâ”€â”€ wwwroot/
â”‚   â”œâ”€â”€ css/site.css
â”‚   â”œâ”€â”€ uploads/categories/          # Images de catÃ©gories (category_<id>.svg)
â”‚   â””â”€â”€ lib/bootstrap/, lib/jquery/
â””â”€â”€ Migrations/InitialCreate.cs
```

## ðŸ”„ Workflow Git

```bash
git add -A
git commit -m "TP SÃ©ance 2 : LocationVoitures.Web â€” MVC .NET 10 + EF Core 10 LocalDB + Repository Pattern + Upload images"
git push -u origin main
```

---

ðŸ’¼ Agence **LocationVoitures** â€” suite complÃ¨te du TP SÃ©ance 2.

TP SÃ©ance 2 : https://cours-web.onebyte.tn/TP_S2/TP_ASPNET_MVC_Seance2.html

