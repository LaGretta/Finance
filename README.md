# FinanceAPI

A REST API for managing personal expenses, built with ASP.NET Core (.NET 10), JWT authentication, and SQL Server.

## Tech Stack

- **ASP.NET Core 10** — Web API framework
- **Entity Framework Core 10** — ORM and migrations
- **SQL Server** — database
- **JWT Bearer** — authentication
- **BCrypt.Net** — password hashing
- **AutoMapper** — object mapping
- **FluentValidation** — request validation
- **Swagger / Swashbuckle** — API documentation

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server

### Setup

1. Clone the repository:
   ```bash
   git clone <repo-url>
   cd FinanceAPI
   ```

2. Update `appsettings.json` with your SQL Server name:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER;Database=FinanceAPIDb;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```

3. Apply migrations:
   ```bash
   dotnet ef database update
   ```

4. Run the project:
   ```bash
   dotnet run
   ```

5. Open Swagger UI: `http://localhost:5253/swagger`

## Endpoints

### Auth

| Method | URL | Description | Auth required |
|--------|-----|-------------|---------------|
| POST | `/api/auth/register` | Register a new user | No |
| POST | `/api/auth/login` | Login and receive a JWT token | No |

**Register example:**
```json
POST /api/auth/register
{
  "userName": "john",
  "password": "password123"
}
```

**Login example:**
```json
POST /api/auth/login
{
  "userName": "john",
  "password": "password123"
}
```
Returns a JWT token to use in subsequent requests.

---

### Expenses

> All endpoints require a JWT token in the header: `Authorization: Bearer <token>`

| Method | URL | Description |
|--------|-----|-------------|
| GET | `/api/expenses/{id}` | Get an expense by ID |
| POST | `/api/expenses` | Create a new expense |
| DELETE | `/api/expenses/{id}` | Delete an expense |

**Create expense example:**
```json
POST /api/expenses
{
  "title": "Groceries",
  "category": "Food",
  "amount": 250.00
}
```

## Project Structure

```
FinanceAPI/
├── Controllers/       — HTTP controllers
├── DTOs/              — data transfer objects
├── Data/              — DbContext
├── Exceptions/        — custom exceptions
├── Mapping/           — AutoMapper profiles
├── Middleware/        — global error handling
├── Migrations/        — EF Core migrations
├── Models/            — database entities
├── Services/          — business logic
│   └── Interfaces/    — service interfaces
└── Validators/        — FluentValidation validators
```

## Using Authorization in Swagger

1. Click the **Authorize** button in Swagger UI
2. Enter the token you received from `/api/auth/login`
3. You can now call protected endpoints
