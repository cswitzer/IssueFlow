# IssueFlow.Infrastructure

This project contains all **external system implementations** for IssueFlow, including persistence, identity, email, and other integrations.

## Responsibilities

- EF Core `IssueFlowDbContext` and migrations
- ASP.NET Core Identity (user, role, claims)
- JWT token creation and validation
- Email and external service integrations

## Key Packages

- `Microsoft.EntityFrameworkCore` → core EF functionality
- `Microsoft.EntityFrameworkCore.SqlServer` → SQL Server provider
- `Microsoft.EntityFrameworkCore.Design` → design-time EF support (migrations)
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore` → Identity tables and management
- `Microsoft.AspNetCore.Authentication.JwtBearer` → JWT middleware
- `Microsoft.IdentityModel.Tokens` → token validation and signing
- `System.IdentityModel.Tokens.Jwt` → create and parse JWTs
- `MailKit` (optional) → sending emails

## Dependency Injection

The project exposes an extension method to register services in the DI container:

```csharp
builder.Services.AddInfrastructure(builder.Configuration);
```

