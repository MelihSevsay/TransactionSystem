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