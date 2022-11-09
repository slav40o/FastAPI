# Database set-up

## Add SQLServer or PostgreSQL
1.Add project reference to either: 
	a) FastAPI.Layers.Infrastructure.Persistence.SQL
	b) FastAPI.Layers.Infrastructure.Persistence.Postgre
2.Call AddPostgrePersistence() or AddSqlServerPersistence() configuration methods

## Setup for migrations
1. Set your web API as startup project
2. Open Package Manager Console
3. Set your infrastructure project as default project in the Package Manager Console


## Add migration
```
Add-Migration 'Identity{MigrationName}' -OutputDir "Persistence/Migrations" -context IdentityUserDbContext
```

## Update database
```
Update-Database -Context IdentityUserDbContext
```

## Required configuration variables
Examples for connection string for both DB types
```
"ConnectionStrings:DefaultConnection": "Server=localhost; Port=5432; User Id=postgres; Password={pass}; Database=SB;",
"ConnectionStrings:DefaultConnection": "Server=127.0.0.1,1433; Database=SB;User Id=sa;Password={pass};MultipleActiveResultSets=true;TrustServerCertificate=true"
```