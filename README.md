# TP2 - Authentification JWT

Ce projet est une implémentation d'un système d'authentification utilisant JSON Web Tokens (JWT) avec ASP.NET Core. Il est structuré en plusieurs couches pour une meilleure organisation et maintenabilité.

## Structure du Projet

Le projet est divisé en plusieurs couches :

1. **DAL (Data Access Layer)** : Cette couche contient les modèles de données, le contexte de la base de données et les repositories.
2. **Service** : Cette couche contient la logique métier et les services, y compris le service JWT pour la génération de tokens.
3. **WebAPI** : Cette couche expose les API RESTful pour l'authentification et d'autres opérations.

### DAL (Data Access Layer)

- **Context**
  - `ApplicationDbContext.cs` : Le contexte de la base de données utilisant Entity Framework Core.

- **Models**
  - `User.cs` : Le modèle de données représentant un utilisateur.

- **Repositories**
  - `UserRepository.cs` : Le repository pour les opérations CRUD sur les utilisateurs.
  - `UnitOfWork.cs` : Le pattern Unit of Work pour gérer les transactions.

- **Interfaces**
  - `IUserRepository.cs` : Interface pour le repository utilisateur.
  - `IUnitOfWork.cs` : Interface pour le pattern Unit of Work.

### Service

- **Services**
  - `UserService.cs` : Service pour la gestion des utilisateurs.
  - `JwtService.cs` : Service pour la génération de tokens JWT.

- **DTOs**
  - `UserDto.cs` : Data Transfer Object pour les utilisateurs.

- **Mapping**
  - `MappingProfile.cs` : Configuration d'AutoMapper pour mapper les modèles et les DTOs.

- **Interfaces**
  - `IJwtService.cs` : Interface pour le service JWT.

### WebAPI

- **Program.cs** : Point d'entrée de l'application, configuration des services et du pipeline de requêtes.
- **appsettings.json** : Fichier de configuration de l'application.
- **Controllers**
  - `AuthController.cs` : Contrôleur pour les opérations d'authentification (enregistrement, connexion).
- **Models**
  - `RegisterModel.cs` : Modèle de requête pour l'enregistrement.
  - `LoginModel.cs` : Modèle de requête pour la connexion.

## Configuration

Assurez-vous de configurer la clé JWT dans le fichier `appsettings.json` :

