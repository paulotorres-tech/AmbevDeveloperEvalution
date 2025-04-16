# Ambev Developer Evaluation - Sales API

## 📌 Project Overview

This is a solution developed for Ambev's technical evaluation, with the primary objective of demonstrating skills in software architecture, RESTful API development, domain modeling (DDD), automated testing, and engineering best practices.

The application is a sales management API, supporting sale creation, consultation, and cancellation. The entire implementation follows Clean Architecture, Domain-Driven Design, Git Flow, and semantic commits.

---

## ✅ Implemented Features

### 📌 Sales Management

- **Create Sale**
  - Register a sale with `SaleNumber`, date, customer, branch, and items
  - Automatic discount rules applied based on quantity
  - Triggers `SaleCreatedEvent` domain event

- **Consult Sale**
  - Endpoint: `GET /sales/{saleNumber}`
  - Returns full sale details, including items, discounts, and total
  - Clean mapping via AutoMapper

- **Cancel Sale**
  - Endpoint: `PUT /sales/{saleNumber}/cancel`
  - Cancels the sale and triggers `SaleCancelledEvent`
  - The sale remains in the system with `IsCancelled = true`

### 🧠 Business Rules Applied

- Automatic discount per item:
  - `5 to 10 units`: 10% discount
  - `more than 10`: 20%
- Maximum of 20 units per item
- Duplicate `SaleNumber` validation
- All fields validated using FluentValidation

### 📦 Technical Structure

- Clear responsibility separation using CQRS + Clean Architecture
- Controllers organized by `Features/`, isolated Request/Response DTOs
- Mappings centralized under the `Mappings/` folder
- Repositories implemented with EF Core (PostgreSQL / InMemory)

### 🧪 Tests

- Unit Tests:
  - Domain (`Sale`, `SaleItem`)
  - Application Layer (`Handlers`, `Validators`)
- Integration Tests:
  - Repository (`SaleRepository`)
  - Handler (`CreateSaleHandler`)
  - Using `InMemoryDatabase` and real context

---

## 🧱 Architecture & Technologies

### Design
- Clean Architecture
- Domain-Driven Design (DDD)
- CQRS with MediatR

### Technologies
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core + PostgreSQL / InMemory
- MediatR
- AutoMapper
- FluentValidation
- Swagger (OpenAPI)
- xUnit
- Bogus (Faker)
- Moq (for mocking in unit tests)

### Project Structure

```bash
src/
├── Domain/
├── Application/
├── ORM/               # EF Core, Migrations, Repositories
├── WebApi/            # Controllers, Features, Mappings
└── IoC/               # Dependency Injection

tests/
├── Unit/
├── Integration/
└── Shared/
```

---

## ▶️ How to Run the Project

1. **Clone the repository**
```bash
git clone https://github.com/youruser/ambev-sales-api.git
cd ambev-sales-api
```

2. **Configure the database (PostgreSQL)**
Create the database and update the connection string in:
`appsettings.Development.json`

3. **Apply migrations**
```bash
cd src/Ambev.DeveloperEvaluation.WebApi

# via CLI
dotnet ef database update
```

4. **Run the API**
```bash
dotnet run --project src/Ambev.DeveloperEvaluation.WebApi
```
Access: `https://localhost:{port}/swagger`

---

## 🧪 Running the Tests

### Unit Tests
```bash
dotnet test tests/Ambev.DeveloperEvaluation.Unit
```

### Integration Tests
```bash
dotnet test tests/Ambev.DeveloperEvaluation.Integration
```

---

## 📦 Git Flow & Versioning

- Main branch: `main`
- Development branch: `develop`
- Each feature in its branch: `feature/create-sale`, `feature/cancel-sale`, etc.
- Semantic commits:
  - `feat:` new feature
  - `fix:` bug fix
  - `test:` tests added/changed
  - `chore:` infrastructure, config

---

## 🚀 Extras

- Controllers separated by `Features` as per template
- `Mappings/` folder for centralized AutoMapper profiles
- Validation handled via `ValidationExceptionMiddleware`
- JWT configured (authentication can be disabled in dev)

---

## 📬 Contact
For technical questions or discussions about this project:

**Paulo Torres**  
Senior Software Engineer  
[LinkedIn](https://linkedin.com/in/paulotorresdev)  
Email: your.email@example.com