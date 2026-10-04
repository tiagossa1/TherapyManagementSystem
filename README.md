# TherapyManagementSystem

A sample system for managing a therapy practice: therapists, clients, appointments and billing. It is a practice project with an Angular front end and an ASP.NET Core API, not a real product.

Screenshots: https://imgur.com/a/RVzs54v

## Structure

- `TherapyAPI`: ASP.NET Core (.NET 10) API using Entity Framework Core with SQLite, AutoMapper and FluentValidation. Swagger is enabled in Development.
- `TherapyUI`: Angular 6 front end with Angular Material. It has pages for login and register, clients, appointments and billings, and talks to the API at `http://localhost:5000/api`.
- `sqliteDumps`: a SQL dump of the sample data. `TherapyAPI/therapy.db` already contains it.

The API exposes CRUD endpoints for appointments, appointment types, billings, civil statuses, clients, genders and therapists (`/api/Therapist/register` creates a therapist). It has no authentication.

## Running it

You need the .NET 10 SDK.

```bash
git clone https://github.com/tiagossa1/TherapyManagementSystem.git
cd TherapyManagementSystem/TherapyAPI
dotnet run
```

The API listens on `http://localhost:5000`. In Development, Swagger is at `http://localhost:5000/swagger`; set `ASPNETCORE_ENVIRONMENT=Development` to turn it on.

The front end is an old Angular 6 project and I have not updated it along with the API, so it may need an older Node version to install and build:

```bash
cd TherapyUI
npm install
npm start
```
