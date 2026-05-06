# FinanceAPI

A full-stack expense tracking app — REST API built with ASP.NET Core (.NET 10) and a vanilla JS/HTML/CSS frontend. Supports JWT authentication, expense management, and a live Swagger UI.

## Tech Stack

**Backend**
- **ASP.NET Core 10** — Web API
- **Entity Framework Core 10** — ORM and migrations
- **SQL Server** — database
- **JWT Bearer** — authentication
- **BCrypt.Net** — password hashing
- **AutoMapper** — object mapping
- **FluentValidation** — request validation
- **Swagger / Swashbuckle** — API documentation

**Frontend**
- Vanilla **HTML / CSS / JavaScript** — no frameworks
- Communicates with the API via `fetch`
- JWT stored in `localStorage`

## Project Structure

```
FinanceAPI/
├── FinanceAPI/               — ASP.NET Core backend
│   ├── Controllers/          — HTTP controllers
│   ├── DTOs/                 — data transfer objects
│   ├── Data/                 — DbContext
│   ├── Exceptions/           — custom exceptions
│   ├── Mapping/              — AutoMapper profiles
│   ├── Middleware/           — global error handling
│   ├── Migrations/           — EF Core migrations
│   ├── Models/               — database entities
│   ├── Services/             — business logic
│   │   └── Interfaces/       — service interfaces
│   ├── Validators/           — FluentValidation validators
│   ├── Program.cs
│   └── appsettings.json
└── frontend/                 — vanilla JS frontend
    ├── index.html            — login / register page
    ├── dashboard.html        — expenses dashboard
    ├── css/style.css         — dark theme styles
    └── js/api.js             — API fetch layer
```

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server
- Any browser (for the frontend)

### Backend Setup

1. Clone the repository:
   ```bash
   git clone <repo-url>
   cd FinanceAPI
   ```

2. Update `FinanceAPI/appsettings.json` with your SQL Server name:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER;Database=FinanceAPIDb;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```

3. Apply migrations:
   ```bash
   cd FinanceAPI
   dotnet ef database update
   ```

4. Run the API:
   ```bash
   dotnet run
   ```
   API will be available at `http://localhost:5253`

### Frontend Setup

No build step needed. Just open `frontend/index.html` in your browser.

> Make sure the backend is running first.

## API Endpoints

### Auth

| Method | URL | Description | Auth |
|--------|-----|-------------|------|
| POST | `/api/auth/register` | Register a new user | No |
| POST | `/api/auth/login` | Login, returns JWT token | No |

**Register:**
```json
POST /api/auth/register
{
  "username": "john",
  "password": "password123"
}
```

**Login:**
```json
POST /api/auth/login
{
  "username": "john",
  "password": "password123"
}
```
Response: `{ "token": "...", "username": "john" }`

---

### Expenses

> All endpoints require: `Authorization: Bearer <token>`

| Method | URL | Description |
|--------|-----|-------------|
| GET | `/api/expenses` | Get all expenses for current user |
| GET | `/api/expenses/{id}` | Get expense by ID |
| POST | `/api/expenses` | Create a new expense |
| DELETE | `/api/expenses/{id}` | Delete an expense |

**Create expense:**
```json
POST /api/expenses
{
  "title": "Groceries",
  "category": "Food",
  "amount": 250.00
}
```

## Using Swagger

1. Run the backend
2. Open `http://localhost:5253/swagger`
3. Call `POST /api/auth/login` to get a token
4. Click **Authorize** and paste the token
5. You can now test all protected endpoints
