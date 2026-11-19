# 🚗 LocationVoitures — Agence de location de voitures

Site web **ASP.NET Core 10 MVC (.NET 10)** — TP Séance 2 : persistance SQL Server LocalDB, **Repository Pattern**, téléversement d'images et page d'accueil dynamique.

L'agence **LocationVoitures** propose une flotte de véhicules à la location. Le site comprend :
- une **page d'accueil vitrine** alimentée par la base de données ;
- un **back-office** pour gérer les **catégories** et la **flotte de véhicules** ;
- l'**upload d'images** enregistrées sur disque.

## 📸 Captures d'écran

| Page | Description |
| :-- | :-- |
| <img src="screenshots/01-home.jpeg" width="200" alt="Accueil"> | **Accueil** : hero animé, statistiques en temps réel (4 catégories, 10 véhicules, 9 disponibles) et grille de cartes de véhicules |
| <img src="screenshots/02-voiture.jpeg" width="200" alt="Flotte"> | **Flotte de véhicules** : tableau avec véhicule, matricule, catégorie (chargée via `.Include`), année, tarif et badge de disponibilité |
| <img src="screenshots/03-categories.jpeg" width="200" alt="Catégories"> | **Gestion des catégories** : tableau avec images et détails |

## 🛠️ Stack technique

- **ASP.NET Core 10.0 MVC** (C#)
- **Entity Framework Core 10.0.15** avec le provider **SQL Server** et **LocalDB** (`(localdb)\mssqllocaldb`)
- **Repository Pattern** :
  - `ICategoryVoitureRepository` / `CategoryVoitureRepository`
  - `IVoitureRepository` / `VoitureRepository` (avec `.Include` pour charger la catégorie)
- **Relation 1-N** : `Voiture` contient la clé étrangère `CategoryVoitureId` et la propriété de navigation `Category` ; `CategoryVoiture` contient la collection `Voitures`
- **Upload d'images** : `IFormFile` → écriture dans `wwwroot/uploads/categories/category_<id>.<ext>`, l'URL relative est stockée en base

## ⚙️ Installation et lancement

**Prérequis** : .NET 10 SDK et SQL Server LocalDB (inclus avec Visual Studio 2022).

```bash
git clone https://github.com/Thamermokhtari987/location_voiture.web.git
cd location_voiture.web
dotnet run --urls https://localhost:5051
```

Ou avec **Visual Studio 2022** : bouton **Démarrer (F5)**, puis ouvrir `https://localhost:5051/`.

> Au démarrage, l'application applique les migrations (`Database.Migrate()`) et insère des données de démonstration si la base est vide (4 catégories + 10 véhicules, voir `Program.cs`).

## 🗃️ Base de données LocalDB

Chaîne de connexion (`appsettings.json`) :

```
Server=(localdb)\mssqllocaldb;Database=LocationVoituresDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True
```

Commandes EF Core (Séance 2, parties 2 et 3) :

```bash
# Package Manager Console (Visual Studio)
Add-Migration InitialCreate
Update-Database

# CLI .NET
dotnet ef migrations add InitialCreate
dotnet ef database update
```

> ⚠️ Une base `LocationVoituresDb` existait déjà dans LocalDB avec une ancienne migration (`InitialCreateCategory`). Elle a été recréée proprement avec `InitialCreate` + les données de démonstration.

## ✅ Fonctionnalités réalisées

- [x] Projet MVC (.NET 10) + EF Core 10
- [x] Packages NuGet : `SqlServer`, `Tools`, `Design`
- [x] Chaîne de connexion LocalDB dans `appsettings.json`
- [x] `ApplicationDbContext` avec `DbSet<CategoryVoiture>` et `DbSet<Voiture>`, enregistré dans `Program.cs`
- [x] Repository Pattern (GetAll, GetById, Add, Update, Delete, Exists) pour les catégories et les voitures (avec `.Include`)
- [x] Modèle `CategoryVoiture` (identifiant, libellé, description, URL de l'image, collection de navigation)
- [x] Relation 1-N entre `CategoryVoiture` et `Voiture`
- [x] Upload d'images (Create / Edit) sur disque + URL relative en base
- [x] Vues Razor complètes (Index, Create, Edit, Details, Delete) avec validation, pour les catégories et les voitures
- [x] Navigation mise à jour dans `_Layout.cshtml` : Accueil / Flotte / Catégories
- [x] Page d'accueil dynamique (véhicules lus en base)
- [x] Migrations Code-First + `Update-Database` sur LocalDB
- [x] Tests CRUD via le navigateur (création, modification, suppression) sans erreur

## 📁 Structure du projet

```
LocationVoitures.Web/
├── Program.cs                          # DI + Database.Migrate() + seed
├── appsettings.json                    # Connexion LocalDB
├── Controllers/
│   ├── HomeController.cs
│   ├── CategoryVoituresController.cs   # CRUD catégories + upload d'images
│   └── VoituresController.cs           # CRUD flotte (+ SelectList des catégories)
├── Data/
│   └── ApplicationDbContext.cs         # DbSet<CategoryVoiture>, DbSet<Voiture>
├── Models/
│   ├── CategoryVoiture.cs
│   └── Voiture.cs
├── Repositories/
│   ├── ICategoryVoitureRepository.cs
│   ├── CategoryVoitureRepository.cs
│   ├── IVoitureRepository.cs
│   └── VoitureRepository.cs            # Utilise .Include
├── Views/
│   ├── Home/Index.cshtml               # Vitrine dynamique
│   ├── CategoryVoitures/               # Index, Create, Edit, Details, Delete
│   ├── Voitures/                       # Index, Create, Edit, Details, Delete
│   └── Shared/_Layout.cshtml           # Navbar + footer
├── wwwroot/
│   ├── css/site.css
│   ├── uploads/categories/             # category_<id>.<ext>
│   └── lib/                            # bootstrap, jquery
└── Migrations/                         # InitialCreate
```

## 🔄 Workflow Git

```bash
git add -A
git commit -m "TP Séance 2 : LocationVoitures.Web — MVC .NET 10 + EF Core 10 LocalDB + Repository Pattern + Upload images"
git push -u origin main
```

## 📚 Ressources

- [Énoncé du TP Séance 2](https://cours-web.onebyte.tn/TP_S2/TP_ASPNET_MVC_Seance2.html)

---

💼 Agence **LocationVoitures** — TP Séance 2.
