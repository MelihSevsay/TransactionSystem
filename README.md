# Transaction System

A simple .NET Core API project that stores users and transaction data in a SQL Server database and provides summarization over transaction data.

## About

The system covers two core responsibilities:

- **Database Operations**: User CRUD operations and adding and fetching transactions
- **Complex Logic Operations**: Total amount per user, total amount per transaction type, identifying high-volume transactions

## Tech Stack

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- AutoMapper
- Swagger

## Requirements

- .NET 10 SDK
- SQL Server

## Setup

```bash
git clone <repo-url>
cd TransactionSystem
dotnet restore
```

## Database Connection

The `appsettings.json` / `appsettings.Development.json` files contain **two different connection strings**. Depending on your setup, you can choose which one to use as `DefaultConnection`.

```json

"DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=TransactionSystemDb;Trusted_Connection=True;TrustServerCertificate=True;"
 or
"DefaultConnection": "Server=.\\SQLEXPRESS;Database=TransactionSystemDb;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=True;"

```

- **LocalDb**: Requires no additional setup — comes bundled with Visual Studio. Recommended for quick start and local development.
- **SqlServer**: For those who want to use their own SQL Server / SQL Server Express instance. You'll need to update the `sa` username and password to match your environment.

## Tests

Integration tests run against an **in-memory SQLite** database instead of LocalDB or SQL Server.