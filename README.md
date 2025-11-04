# 🏢 Agency Appointment System

A simple **Appointment Booking and Token Management System** built for the **Qwiik Technical Test**.  
The project allows an agency to manage customer appointments, issue tokens, and visualize the daily queue.  
It also includes additional management features such as defining off days and setting maximum daily appointments.

---

## 🚀 Features

### Core Features
- ✅ Customers can **book appointments** for a selected date.
- ✅ The system **issues a unique token** for each appointment.
- ✅ The agency can **view all appointments for a day** in a queue/grid format.

### Optional Enhancements
- 🗓️ Agency can **set off days / public holidays** (no appointments allowed).
- 📅 Agency can **set a daily appointment limit** — extra bookings are automatically moved to the next available day.
- 📘 All API endpoints are **documented with Swagger**.
- 🧠 **Separation of Concerns** between Business Logic and Web Layer.
- 🔁 Supports **Dependency Injection (IoC)** and **Extensibility** via Autofac.
- 🧩 Simplified code using **LINQ**.
- ✅ Includes **Unit Tests**.
- ☁️ Ready for **Azure deployment**.
- 🔗 Source control managed via **GitHub**.

---

## 🧱 Architecture Overview

The project follows a **layered architecture** to ensure maintainability and clean separation of concerns.

```
AgencyAppointment/
│
├── src/
│   ├── AgencyAppointment.WebAPI/        → Web API layer (Controllers, Swagger, DI)
│   ├── AgencyAppointment.Business/      → Core business logic (Services, Interfaces)
│   ├── AgencyAppointment.Data/          → Data access (Repositories, Models)
│   └── AgencyAppointment.Tests/         → Unit tests (xUnit / NUnit)
│
└── global.json                          → Forces SDK version (8.0.x)
```

---

## ⚙️ Tech Stack

| Layer | Technology |
|-------|-------------|
| Web API | ASP.NET Core 8 (WebAPI) |
| Business Logic | C# Services + Interfaces |
| Data Layer | In-memory repository or SQL (configurable) |
| IoC / DI | Autofac |
| API Docs | Swagger / Swashbuckle |
| Unit Testing | xUnit |
| Hosting | Azure App Service |
| Source Control | GitHub |

---

## 🧩 Setup & Installation

### 1️⃣ Prerequisites
- [.NET SDK 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Visual Studio 2022 / VS Code
- Git

### 2️⃣ Clone Repository
```bash
git clone https://github.com/<your-username>/AgencyAppointment.git
cd AgencyAppointment
```

### 3️⃣ Restore & Build
```bash
dotnet restore
dotnet build
```

### 4️⃣ Run Application
```bash
cd src/AgencyAppointment.WebAPI
dotnet run
```

Open in browser:
```
https://localhost:5001/swagger
```

---

## 🧪 Running Tests

```bash
cd src/AgencyAppointment.Tests
dotnet test
```

---

## 🌐 API Documentation

Once the app is running, visit Swagger UI at:

```
https://localhost:5001/swagger
```

You’ll find:
- `POST /api/appointments` — Book new appointment
- `GET /api/appointments/{date}` — View appointments for a specific day
- `POST /api/agency/offdays` — Add off days
- `POST /api/agency/settings` — Set max appointments per day

---

## 🏗️ Deployment on Azure

1. Build release:
   ```bash
   dotnet publish -c Release
   ```
2. Deploy to **Azure App Service** (via Visual Studio or Azure CLI).
3. Set environment variables for production (if needed).

---

## 🧰 NuGet Packages Used

| Package | Purpose |
|----------|----------|
| `Swashbuckle.AspNetCore` | Swagger documentation |
| `Autofac.Extensions.DependencyInjection` | IoC container |
| `Microsoft.Extensions.Logging` | Logging |
| `xunit` | Unit testing |
| `Moq` | Mocking dependencies in tests |

---

## 🧑‍💻 Developer Notes

- The solution enforces **Clean Coding Principles (SOLID)**.
- Business logic is isolated in service classes for testability.
- Repositories abstract data access.
- Dependency injection is used throughout (configurable via Autofac).
- Swagger provides automatic API documentation.

---

## 🏁 Version

- **.NET SDK:** 8.0.412  
- **C# Language Version:** 12  
- **Last Updated:** November 2025  

---

## 📜 License

This project was developed for the **Qwiik Technical Test** and is intended for demonstration purposes only.

---

🧑‍💼 **Author:** Nurul Hidayat  
📧 Email: *van.daytch@gmail.com*  
🔗 LinkedIn: [https://linkedin.com/in/nurulhidayat](https://linkedin.com/in/nurulhidayat)
