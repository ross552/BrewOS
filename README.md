# BrewOS

**BrewOS — Virtual Coffee Machine Platform**

A production-oriented portfolio project that models a virtual coffee machine platform. Built as a senior-level assessment submission with clear separation of concerns, a modern SPA frontend, and a container-ready data tier.

## Technology Stack

| Layer | Technology |
|-------|------------|
| Backend | .NET 8, ASP.NET Core Web API |
| Frontend | React, TypeScript, Vite, Tailwind CSS |
| Data | PostgreSQL |
| Persistence | Entity Framework Core |
| Testing | xUnit |
| Containers | Docker / Docker Compose |

## Architecture

BrewOS follows **Clean Architecture** with strict dependency direction:

```
BrewOS.Api                → Application, Infrastructure
BrewOS.Infrastructure     → Application, Domain
BrewOS.Application        → Domain
BrewOS.Domain             → (no dependencies)
BrewOS.Tests              → Domain, Application
```

| Project | Responsibility |
|---------|----------------|
| **BrewOS.Domain** | Core entities, value objects, and domain rules |
| **BrewOS.Application** | Use cases, interfaces, and application services |
| **BrewOS.Infrastructure** | EF Core, PostgreSQL, external integrations |
| **BrewOS.Api** | HTTP endpoints, DI composition, cross-cutting concerns |
| **BrewOS.Tests** | Unit and integration tests |
| **brewos-web** | React client (React Query, Zustand, Axios, Framer Motion) |

## Repository Structure

```
BrewOS/
├── backend/
│   ├── BrewOS.sln
│   ├── BrewOS.Api/
│   ├── BrewOS.Application/
│   ├── BrewOS.Domain/
│   ├── BrewOS.Infrastructure/
│   └── BrewOS.Tests/
├── frontend/
│   └── brewos-web/
├── docker-compose.yml
├── .gitignore
└── README.md
```

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) 20+
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (for PostgreSQL)

### Backend

```bash
cd backend
dotnet restore
dotnet build
dotnet run --project BrewOS.Api
```

API health check: `GET /health`  
Swagger (Development): `/swagger`

### Frontend

```bash
cd frontend/brewos-web
npm install
npm run dev
```

### Infrastructure

```bash
docker compose up -d
```

Starts PostgreSQL on port `5432` for upcoming persistence work.

## Current Status

Foundation only — solution structure, project references, frontend tooling, and Docker Compose are in place. Domain models, application use cases, and API controllers will follow in subsequent steps.
