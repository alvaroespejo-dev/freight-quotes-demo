# AEspejo.FreightQuotes

Freight rating platform for requesting and comparing LTL shipping quotes from multiple carriers. The system provides an Angular web application, an ASP.NET Core API, carrier adapters for external rating services, and a configurable reference-data catalog for addresses, accessorials, freight classes and carrier settings.

`.NET 8` `ASP.NET Core` `EF Core 9` `Angular 19` `TypeScript` `SQLite` `SQL Server` `SignalR` `MediatR` `Azure`

API deployment: `https://freightquotesapi-dxc8d0agaqepd0c4.canadacentral-01.azurewebsites.net`

Repository: `alvaroespejo-dev/freight-quotes-demo` · Default branch: `main`

## Overview

A quote request contains origin, destination and billing addresses, shipment line items, accessorials, equipment and service options. The API resolves database IDs into the carrier codes required by external APIs, sends the request to every active carrier in parallel, and streams results back to the browser as they arrive.

The frontend opens a SignalR connection before submitting the request. It joins a group using the request ID and receives these events:

- `ReceiveQuotes` when a carrier returns one or more quotes.
- `ReceiveQuoteError` when a carrier request fails.
- `AllQuotesCompleted` when all active carriers have finished.

The application is seeded with FedEx, Estes and UPS carriers in mock mode, so local development does not require carrier credentials. Real carrier endpoints and credentials are stored as carrier settings and can be configured through the application.

## Features

- Multi-carrier LTL quote requests from a single shipment form.
- Parallel carrier calls with independent results and error reporting.
- Real-time quote streaming through ASP.NET Core SignalR.
- Mock carrier mode for development and demos without external API calls.
- FedEx OAuth and LTL rating adapter.
- UPS OAuth client-credentials and rating adapter.
- Carrier management with active/inactive and mock-mode settings.
- Carrier-specific authentication and rating settings such as URLs, client IDs, secrets, API keys and accounts.
- Reference data for countries, states, constants, freight classes, shipping units and accessorials.
- Address-level and shipment-level accessorials.
- Swagger/OpenAPI documentation in development.
- SQLite by default for local development, with SQL Server support through configuration.
- EF Core migrations kept in provider-specific projects.
- Unit tests for domain, repositories, carrier services and rate-quote services.
- Azure App Service deployment for the API and Azure Static Web Apps deployment for the Angular client.

## Quote Flow

1. The Angular client loads countries, states, constants, accessorials and carriers.
2. The user enters shipment addresses and one or more freight line items.
3. The client connects to `/hubs/rate-quote` and joins the request group.
4. The client submits the request to `POST /api/RateQuote`.
5. The API resolves state, country, freight-class and shipping-unit IDs into carrier codes.
6. Every active carrier is queried concurrently using its configured adapter.
7. Results and carrier-specific errors are sent to the request group through SignalR.
8. The client displays quotes as they arrive and stops loading after `AllQuotesCompleted`.

## Architecture

The solution uses a layered architecture with feature-oriented API handlers and a provider-independent persistence layer.

| Layer | Project | Responsibility |
| --- | --- | --- |
| Domain | `AEspejo.FreightQuotes.Domain` | Entities and core domain models. |
| Application | `AEspejo.FreightQuotes.Application` | Services, repository contracts, mapping and business orchestration. |
| Shared | `AEspejo.FreightQuotes.Shared` | DTOs, constants and contracts shared by the API and integrations. |
| Infrastructure | `AEspejo.FreightQuotes.Infrastructure` | EF Core `DbContext`, repositories, unit of work and database seeds. |
| Carrier clients | `AEspejo.FreightQuotes.CarrierApiClient` | Carrier adapters, HTTP transport, authentication and response mapping. |
| API | `AEspejo.FreightQuotes.Api` | Controllers, MediatR handlers, SignalR hub, dependency injection and health endpoints. |
| Migrations | `AEspejo.FreightQuotes.Migrations.Sqlite` / `AEspejo.FreightQuotes.Migrations.SqlServer` | EF Core migrations for each database provider. |
| Client | `AEspejo.FreightQuotes.AngularApp` | Angular application for quote requests, results and administration. |

### Design patterns and techniques

- Feature folders organize API commands, queries, handlers and controllers by business capability.
- MediatR separates HTTP controllers from application request handling.
- Repository and unit-of-work abstractions isolate persistence from application services.
- Keyed dependency injection selects a carrier rate client by SCAC.
- A decorator centralizes carrier-client exception logging and handling.
- SignalR groups isolate quote events by `requestId`.
- Angular Signals hold lookup data, quote results, errors and loading state.

## Tech Stack

**Backend** · .NET 8 · ASP.NET Core Web API · Entity Framework Core 9 · MediatR 13 · AutoMapper 15 · Scrutor · Swagger/OpenAPI.

**Carrier integrations** · `HttpClient` · Newtonsoft.Json · FedEx OAuth/rating API · UPS OAuth/rating API · mock rate client.

**Frontend** · Angular 19 · TypeScript 5.6 · Angular Material 19 · RxJS 7 · `@microsoft/signalr` · `ngx-toastr`.

**Data** · SQLite by default · SQL Server supported at runtime · provider-specific EF Core migration assemblies.

**Hosting and CI/CD** · GitHub Actions · Azure App Service · Azure Static Web Apps.

## Requirements

- .NET 8 SDK.
- Node.js 20 or later and npm.
- SQL Server only when using the SQL Server provider.
- Optional: EF Core CLI tools: `dotnet tool install --global dotnet-ef`.
- Optional: a SQLite browser for inspecting local database files.

## Getting Started

Clone and build the solution:

```bash
git clone git@github-work:alvaroespejo-dev/freight-quotes-demo.git
cd freight-quotes-demo
dotnet restore AEspejo.FreightQuotes.sln
dotnet build AEspejo.FreightQuotes.sln
```

### Run the API

The development configuration uses SQLite and creates `freightquotes.dev.db` in the API working directory. The API applies pending migrations during startup.

```bash
dotnet run --project AEspejo.FreightQuotes.Api
```

Local URLs:

- HTTP API: `http://localhost:5047`
- HTTPS API: `https://localhost:7129`
- Swagger UI: `https://localhost:7129/swagger`
- Health check: `https://localhost:7129/health`
- SignalR hub: `https://localhost:7129/hubs/rate-quote`

The root endpoint returns the service status at `/`. The health endpoint verifies that the configured database can be reached.

### Run the Angular client

Install dependencies and start the development server:

```bash
cd AEspejo.FreightQuotes.AngularApp
npm install
npm start
```

The client runs at `http://localhost:49672`. Development requests use `https://localhost:5001/api` as configured in `src/app/environments/environment.ts`. If the API is running on the launch profile above, update that environment URL or start the API on the expected port.

Create a production build with:

```bash
npm run build -- --configuration production
```

The production environment points to the deployed API:
`https://freightquotesapi-dxc8d0agaqepd0c4.canadacentral-01.azurewebsites.net/api`.

## Configuration

The API reads its database configuration from `AEspejo.FreightQuotes.Api/appsettings.json` and environment-specific files.

```json
{
  "DatabaseProvider": "Sqlite",
  "ConnectionStrings": {
    "Default": "Data Source=freightquotes.db"
  }
}
```

Supported values for `DatabaseProvider` are `Sqlite` and `SqlServer`.

### SQLite

```json
{
  "DatabaseProvider": "Sqlite",
  "ConnectionStrings": {
    "Default": "Data Source=freightquotes.dev.db"
  }
}
```

### SQL Server

```json
{
  "DatabaseProvider": "SqlServer",
  "ConnectionStrings": {
    "Default": "Server=localhost,1433;Database=FreightQuotes;User Id=sa;Password=<password>;TrustServerCertificate=True;"
  }
}
```

Do not commit carrier credentials or database passwords. Use user secrets, environment variables or the secret store provided by the deployment platform.

## Carrier Settings

Carrier records are stored in the database and are selected by their SCAC code. The initial seed contains:

| Carrier | SCAC | Initial mode |
| --- | --- | --- |
| FedEx | `FXFE` | Mock |
| Estes | `EXLA` | Mock |
| UPS | `UPS` | Mock |

To use a real carrier integration:

1. Set the carrier to active.
2. Disable mock mode for the carrier.
3. Add the rating and authentication settings required by the adapter.
4. Store credentials outside source control.
5. Verify the carrier endpoint and credentials in a non-production environment.

FedEx uses an OAuth token endpoint, client ID and client secret. UPS uses an OAuth token endpoint, client ID, client secret and optional merchant ID, followed by the rating endpoint. The seeded Estes record is available for mock responses; no Estes-specific adapter is currently registered.

## API Endpoints

All REST controllers use the `api` prefix.

| Endpoint | Purpose |
| --- | --- |
| `GET /` | Service status. |
| `GET /health` | Database connectivity health check. |
| `POST /api/RateQuote` | Starts an asynchronous quote request. Returns `202 Accepted` with the request ID. |
| `GET /api/Carriers` | Lists carriers. |
| `POST /api/Carriers` | Creates a carrier. |
| `PUT /api/Carriers/{id}` | Updates a carrier. |
| `DELETE /api/Carriers/{id}` | Deletes a carrier. |
| `GET /api/CarrierSettings?carrierId={id}` | Lists settings for a carrier. |
| `POST /api/CarrierSettings` | Creates a carrier setting. |
| `PUT /api/CarrierSettings/{id}` | Updates a carrier setting. |
| `DELETE /api/CarrierSettings/{id}` | Deletes a carrier setting. |
| `GET /api/Countries` | Lists countries. |
| `GET /api/States?countryId={id}` | Lists states, optionally filtered by country. |
| `GET /api/Accessorials` | Lists accessorials. |
| `GET /api/Constants?constantTypeIds={id}` | Lists constants for the requested types. |
| `GET /hubs/rate-quote` | SignalR endpoint for streamed quote results. |

## Database and Migrations

The API registers `FreightQuotesDbContext` using the provider named by `DatabaseProvider`. Each provider uses its own migration assembly because EF Core migrations are provider-specific.

The API also runs `Database.Migrate()` at startup. For production deployments, review and apply migrations as part of the release process rather than relying only on application startup.

Examples for creating a migration:

```bash
# SQLite migrations
dotnet ef migrations add <MigrationName> \
  --project AEspejo.FreightQuotes.Migrations.Sqlite \
  --startup-project AEspejo.FreightQuotes.Api

# SQL Server migrations
dotnet ef migrations add <MigrationName> \
  --project AEspejo.FreightQuotes.Migrations.SqlServer \
  --startup-project AEspejo.FreightQuotes.Api
```

The database is seeded with countries, states, constants, accessorials, carriers and carrier-setting definitions through EF Core model configuration.

## Testing

Run all .NET tests from the repository root:

```bash
dotnet test AEspejo.FreightQuotes.sln
```

Run frontend tests:

```bash
cd AEspejo.FreightQuotes.AngularApp
npm test
```

The unit-test project covers repositories, domain entities, carrier HTTP services, rate clients, decorators and quote services. The integration-test project is included in the solution for future API-level coverage.

## CI/CD and Deployment

GitHub Actions define separate deployment workflows:

| Workflow | Trigger | Purpose |
| --- | --- | --- |
| `.github/workflows/deploy-api-azure.yml` | Push to `main` when backend paths change, or manual dispatch | Restore, build, publish and deploy the API to Azure App Service `freightquotesapi`. |
| `.github/workflows/azure-static-web-apps-agreeable-field-098225d10.yml` | Push or pull request to `main` when Angular paths change | Install dependencies, build the Angular SPA and deploy it to Azure Static Web Apps. |

The API workflow uses .NET `8.0.x` and deploys `AEspejo.FreightQuotes.Api/AEspejo.FreightQuotes.Api.csproj`. The frontend workflow uses Node.js `20.x` and deploys the prebuilt Angular output from `dist/aespejo.freight-quotes.angular-app/browser`.

Required GitHub secrets are configured in the repository settings and must not be committed to source control:

- `AZURE_WEBAPP_PUBLISH_PROFILE` for the API App Service.
- `AZURE_STATIC_WEB_APPS_API_TOKEN` for the Angular Static Web App.

For production, configure the API connection string, `DatabaseProvider`, carrier settings and credentials in Azure App Service configuration. The Angular production API URL is compiled from `src/app/environments/environment.production.ts`.

## Project Structure

```text
freight-quotes-demo/
├─ .github/workflows/                         # Azure deployment workflows
├─ AEspejo.FreightQuotes.Api/                 # ASP.NET Core API, features and SignalR hub
├─ AEspejo.FreightQuotes.Application/         # Services and persistence contracts
├─ AEspejo.FreightQuotes.CarrierApiClient/    # FedEx, UPS and mock carrier clients
├─ AEspejo.FreightQuotes.Domain/              # Domain entities
├─ AEspejo.FreightQuotes.Infrastructure/      # EF Core, repositories and seeds
├─ AEspejo.FreightQuotes.Migrations.Sqlite/   # SQLite migrations
├─ AEspejo.FreightQuotes.Migrations.SqlServer/ # SQL Server migrations
├─ AEspejo.FreightQuotes.Shared/              # DTOs and shared constants
├─ AEspejo.FreightQuotes.AngularApp/          # Angular client
├─ AEspejo.FreightQuotes.UnitTests/            # Unit tests
├─ AEspejo.FreightQuotes.IntegrationTests/    # Integration-test project
└─ AEspejo.FreightQuotes.sln                  # .NET solution
```

## Conventions

- Keep business behavior in application services or feature handlers rather than controllers.
- Add carrier-specific behavior behind `ICarrierRateClient` and register it by SCAC.
- Keep carrier credentials and environment-specific connection strings out of source control.
- Reuse shared DTOs from `AEspejo.FreightQuotes.Shared` for API and client contracts.
- Preserve separate migration assemblies for SQLite and SQL Server.
- Add or update unit tests when changing quote resolution, carrier adapters or persistence behavior.
