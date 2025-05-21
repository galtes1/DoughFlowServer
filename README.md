# DoughFlow Server

This is the backend API for the DoughFlow application – a full-stack financial management system.
The server is built with **ASP.NET Core** and communicates with a SQL Server database.

---

## 🧠 Key Features

- RESTful API for managing user incomes and expenses
- Entity Framework Core for data access and migrations
- Authentication & authorization support
- SQL scripts for initial data seeding (expense/income categories, 2024 months)
- CORS-enabled for integration with the React frontend

---

## 🚀 Getting Started

### Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Installation

1. **Clone the repository**

```
git clone https://github.com/galtes1/DoughFlowServer.git
```

2. **Set your connection string**

In `Program.cs`, update the connection string to match your local SQL Server instance:

```
string connectionString = "Server=YOUR_SERVER_NAME; Database=DoughFlowDb; Trusted_Connection=True;TrustServerCertificate=True;";
```

3. **Apply database migrations**

Open terminal in the project folder and run:

```
dotnet ef database update
```

4. **Run the application**

```
dotnet run
```

The API should now be running at: `https://localhost:1463`

---

## 📂 SQL Scripts

You’ll find three SQL files in the `SQL/` folder:

- `1. ExpenseCategoriesScript.sql`
- `2. IncomeCategoriesScript.sql`
- `3. insertRecordYear2024.sql`

These are executed automatically through the migration logic on first-time setup.

---

## 🧪 Technologies Used

- ASP.NET Core 7
- Entity Framework Core
- SQL Server
- LINQ & C# 10

---

## ℹ️ Notes

- Make sure CORS is enabled for the frontend domain (localhost:3000 by default).
- The connection string is hard-coded in `Program.cs` but can be moved to `appsettings.json` for better configuration management.

---

**created and developed by Gal Testa, Israel 2025**