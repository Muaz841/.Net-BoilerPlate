# BoilerPlate Project

A modern, full-stack boilerplate using ASP.NET Core (backend) and Angular (frontend) with clean architecture, JWT authentication, and role/permission-based authorization.

---

## Table of Contents
- [Overview](#overview)
- [Purpose & Key Features](#purpose--key-features)
- [Backend Summary](#backend-summary)
- [Frontend Summary](#frontend-summary)
- [Folder Structure](#folder-structure)
- [Installation & Setup](#installation--setup)
- [Environment Variables](#environment-variables)
- [Build & Run Instructions](#build--run-instructions)
- [API Documentation](#api-documentation)
- [Authorization & Role Logic](#authorization--role-logic)
- [Database Schema](#database-schema)
- [Testing](#testing)
- [Deployment](#deployment)
- [Coding Standards & Architecture](#coding-standards--architecture)
- [Future Improvements / Roadmap](#future-improvements--roadmap)

---

## Overview
BoilerPlate is a production-ready starter kit for enterprise web apps. It provides robust authentication, authorization, and a scalable architecture for rapid development.

## Purpose & Key Features
- Clean, layered architecture (Domain, Application, Infrastructure, API, Frontend)
- JWT authentication, role/permission-based authorization
- Angular 20+ frontend with modern, responsive UI
- Modular, testable, and maintainable code
- Out-of-the-box login, registration, and dashboard

## Backend Summary
- **Framework:** ASP.NET Core Web API
- **Architecture:** Clean/Onion, layered (API, Application, Infrastructure)
- **Key Services:**
  - Authentication (JWT)
  - User Management
  - Role & Permission Management
- **Controllers:**
  - `AuthController` (`/api/auth/login`, `/api/auth/create`)
  - `UsersController` (`/api/users/GetAll`, `/api/users/{id}/GetById`, `/api/users/create`)
  - `PermissionsController` (`/api/permissions/me`)
- **Authorization:**
  - Policies and guards based on roles/permissions
  - Claims-based, hierarchical permissions

## Frontend Summary
- **Framework:** Angular 20+, standalone components
- **Structure:**
  - `features/auth`: Login & Register
  - `features/dashboard`: Dashboard
  - `layout`: Main layout, header
  - `core`: Guards, interceptors, services
- **Routing:**
  - `/auth` (Login)
  - `/register` (Register)
  - `/dashboard` (Protected dashboard)
- **State Management:** Service-based, RxJS
- **UI:** Responsive, modern SCSS, clean UX
- **Build:** Angular CLI, SSR-ready

## Folder Structure
```
Biolerplate/
├── BoilerPlate.Api/            # ASP.NET Core API
│   └── Controllers/
├── BoilerPlate.Application/    # Application Layer (Entities, Services)
├── BoilerPlate.Infrastructure/ # Infrastructure Layer (DB, Repos)
├── BoilerPlate.Frontend/       # Angular Frontend
│   ├── src/
│   │   ├── app/
│   │   │   ├── core/
│   │   │   ├── features/
│   │   │   ├── layout/
│   │   │   └── ...
│   │   └── ...
│   └── ...
└── ...
```

## Installation & Setup
### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli)
- SQL Server (or update connection string for your DB)

### Backend
```sh
cd BoilerPlate.Api
# Update appsettings.json as needed
# Apply migrations
# dotnet ef database update
# Run API
 dotnet run
```

### Frontend
```sh
cd BoilerPlate.Frontend
npm install
npm start
```

## Environment Variables
- **Backend**: `appsettings.json` for DB connection, JWT secrets, etc.
- **Frontend**: `environment.ts` for API base URL

## Build & Run Instructions
- **Backend**: `dotnet run` (API at http://localhost:5000)
- **Frontend**: `npm start` (UI at http://localhost:4200)

## API Documentation
- **Auth**:
  - `POST /api/auth/login` (body: email, password)
  - `POST /api/auth/create` (body: email, name, password)
- **Users**:
  - `GET /api/users/GetAll` (auth required)
  - `GET /api/users/{id}/GetById`
  - `POST /api/users/create`
- **Permissions**:
  - `GET /api/permissions/me` (auth required)

## Authorization & Role Logic
- JWT tokens issued on login/registration
- Claims-based, with roles and hierarchical permissions
- Frontend guards (authGuard, permissionGuard) enforce route/UI access

## Database Schema
- **User**: Id, Email, Name, PasswordHash, CreatedAt
- **Role**: Id, Name, Code
- **Permission**: Id, Name, DisplayName, ParentId, etc.
- **UserRole**, **RolePermission**, **UserPermission** (many-to-many join tables)
- Migrations managed in Infrastructure/Migrations

## Testing
- **Backend**: xUnit/NUnit (add tests in Application/Tests)
- **Frontend**: Angular CLI (`ng test`)

## Deployment
- **Backend**: Deployable to Azure, AWS, Docker
- **Frontend**: Build with `ng build`, deploy `dist/` to Netlify/Vercel/Azure Static Web Apps

## Coding Standards & Architecture
- Follows SOLID, DRY, and separation of concerns
- Clean, layered structure
- Angular uses standalone components, modern patterns
- Linting and formatting via Prettier, Angular CLI

## Future Improvements / Roadmap
- Add user/role/permission management UIs
- Add email verification, password reset
- Add multi-tenant support
- Expand API documentation (Swagger/OpenAPI)
- Add CI/CD workflows

---

_Authored by a senior engineer. For questions or contributions, open an issue or PR._
