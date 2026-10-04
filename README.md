# 📚 Library Management System

A RESTful Web API for managing a library's books, members, loans, reservations, and overdue fines. Built with **ASP.NET Core (.NET 8)**, **Entity Framework Core**, and **SQL Server**, using a clean three-layer architecture (API → BLL → DAL).

---

## ✨ Features

- **Book management** – full CRUD for books, with total and available copy tracking
- **Member management** – full CRUD for library members
- **Issue & return workflow**
  - Issue a book to a member (automatically decrements available copies)
  - 14-day loan period with an automatically calculated due date
  - Return a book (automatically restores available copies)
- **Automatic fines** – late returns generate a fine of **10 per day late**
- **Fine payments** – pay a fine and list a member's unpaid fines
- **Reservations** – reserve a book only when no copies are available
- **Reports** – most-borrowed books
- **Swagger UI** – interactive API documentation in development mode

---

## 🏗️ Architecture

The solution is split into three projects:

```
LibraryManagementSystem/
├── LmsApp/   # Presentation layer – ASP.NET Core Web API (controllers, Program.cs, config)
├── BLL/      # Business Logic Layer – services, DTOs, AutoMapper profiles
└── DAL/      # Data Access Layer – EF Core DbContext, models, repositories, migrations
```

**Request flow:** `Controller → Service (BLL) → DataAccessFactory → Repository (DAL) → SQL Server`

| Layer | Responsibility |
|-------|----------------|
| **LmsApp** | HTTP endpoints, dependency injection, Swagger setup |
| **BLL** | Business rules (issue/return logic, fines, reservations), entity ↔ DTO mapping |
| **DAL** | `LMSContext`, entity models, generic `IRepository<T>`, per-entity repositories, EF migrations |

---

## 🛠️ Tech Stack

- **.NET 8** / ASP.NET Core Web API
- **Entity Framework Core 9** (Code-First, SQL Server provider)
- **SQL Server** (SQL Express works fine)
- **AutoMapper 14** for DTO mapping
- **Swashbuckle** (Swagger / OpenAPI)

---

## 🗄️ Data Model

| Entity | Key Fields |
|--------|-----------|
| **Book** | `BookId`, `Title`, `Author`, `Category`, `ISBN`, `TotalCopies`, `AvailableCopies` |
| **Member** | `MemberId`, `Name`, `Email`, `Phone`, `MembershipDate`, `Status` |
| **IssueRecord** | `IssueRecordId`, `BookId`, `MemberId`, `IssueDate`, `DueDate`, `ReturnDate?`, `Status` |
| **Reservation** | `ReservationId`, `BookId`, `MemberId`, `ReservationDate`, `Status` |
| **Fine** | `FineId`, `IssueRecordId`, `DaysLate`, `Amount`, `IsPaid` |

**Relationships:** an `IssueRecord` belongs to one `Book` and one `Member`; a `Reservation` belongs to one `Book` and one `Member`; a `Fine` belongs to one `IssueRecord`.

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server or SQL Server Express
- (Optional) Visual Studio 2022+ or VS Code
- (Optional) EF Core CLI tools: `dotnet tool install --global dotnet-ef`

### 1. Clone the repository

```bash
git clone <your-repository-url>
cd LibraryManagementSystem
```

### 2. Configure the database connection

Open `LmsApp/appsettings.json` and update the `DbCon` connection string to match your SQL Server instance:

```json
"ConnectionStrings": {
  "DbCon": "Data Source=YOUR_SERVER\\SQLEXPRESS; Initial Catalog=LMS; TrustServerCertificate=True; Integrated Security=True;"
}
```

### 3. Apply the database migration

From the solution root:

```bash
dotnet ef database update --project DAL --startup-project LmsApp
```

This creates the `LMS` database and all tables from the `InitDb` migration.

### 4. Run the API

```bash
cd LmsApp
dotnet run
```

Swagger UI opens automatically at:

- `https://localhost:7073/swagger`
- `http://localhost:5265/swagger`

---

## 📡 API Reference

All routes are prefixed with `/api`.

### Books — `/api/Book`

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/all` | List all books |
| GET | `/{id}` | Get a book by ID |
| POST | `/create` | Add a new book |
| PUT | `/update` | Update a book |
| DELETE | `/delete/{id}` | Delete a book |

### Members — `/api/Member`

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/all` | List all members |
| GET | `/{id}` | Get a member by ID |
| POST | `/create` | Register a new member |
| PUT | `/update` | Update a member |
| DELETE | `/delete/{id}` | Delete a member |

### Issue Records — `/api/IssueRecord`

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/all` | List all issue records |
| GET | `/{id}` | Get an issue record by ID |
| POST | `/create` | Create an issue record manually |
| PUT | `/update` | Update an issue record |
| DELETE | `/delete/{id}` | Delete an issue record |
| POST | `/issue?bookId={bookId}&memberId={memberId}` | **Issue a book** to a member |
| POST | `/return/{issueId}` | **Return** an issued book |
| GET | `/report/most-borrowed` | Books ranked by times borrowed |

### Reservations — `/api/Reservation`

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/all` | List all reservations |
| GET | `/{id}` | Get a reservation by ID |
| POST | `/create` | Create a reservation manually |
| PUT | `/update` | Update a reservation |
| DELETE | `/delete/{id}` | Delete a reservation |
| POST | `/reserve?bookId={bookId}&memberId={memberId}` | **Reserve** a book (only if no copies are available) |

### Fines — `/api/Fine`

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/all` | List all fines |
| GET | `/{id}` | Get a fine by ID |
| POST | `/create` | Create a fine manually |
| PUT | `/update` | Update a fine |
| DELETE | `/delete/{id}` | Delete a fine |
| POST | `/pay/{id}` | **Pay** a fine |
| GET | `/unpaid/member/{memberId}` | List a member's unpaid fines |

---

## 📋 Business Rules

- **Issuing:** a book can only be issued if `AvailableCopies > 0`. The loan period is **14 days**, and the record's status is set to `Issued`.
- **Returning:** the book's `AvailableCopies` is incremented. If the return date is past the due date, a `Fine` is created at **10 × days late** and the record's status becomes `Overdue`; otherwise it becomes `Returned`.
- **Reserving:** a reservation is only accepted when the book has **no** available copies (status `Pending`). If copies are available, the request is rejected.
- **Paying fines:** a fine can only be paid once; paying an already-paid or non-existent fine returns `400 Bad Request`.

---

## 🧪 Example Workflow

```bash
# 1. Add a book
POST /api/Book/create
{
  "title": "Clean Code",
  "author": "Robert C. Martin",
  "category": "Software Engineering",
  "isbn": "9780132350884",
  "totalCopies": 3,
  "availableCopies": 3
}

# 2. Register a member
POST /api/Member/create
{
  "name": "Jane Doe",
  "email": "jane@example.com",
  "phone": "0123456789",
  "membershipDate": "2026-01-01T00:00:00",
  "status": "Active"
}

# 3. Issue the book
POST /api/IssueRecord/issue?bookId=1&memberId=1

# 4. Return it
POST /api/IssueRecord/return/1

# 5. Check unpaid fines for the member
GET /api/Fine/unpaid/member/1
```

---

## 🔮 Possible Improvements

- Authentication & role-based authorization (admin / librarian / member)
- Input validation and consistent error responses (e.g. `404` for missing records)
- Enforce that a member can't reserve/issue the same book twice at once
- Async repository methods and pagination for list endpoints
- Wrap multi-step operations (issue/return) in database transactions
- Unit and integration tests
- Remove the template `WeatherForecast` controller

---

## 📄 License

This project is open source.

---

## 👤 Author

**Md Faisal Ahmed (Rafi)**
