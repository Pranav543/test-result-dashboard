# Test Result Dashboard

A full-stack web application for logging, filtering, and visualizing
hardware test results from a manufacturing floor.

**Stack:** ASP.NET Core 8 · Entity Framework Core · SQLite · React · Recharts

## Setup
See each folder's README for detailed instructions.

## Architecture
- `backend/TestDashboard.API` — REST API (C#)
- `backend/TestDashboard.Tests` — xUnit test suite
- `frontend/test-dashboard-ui` — React SPA (Vite)

## Running Locally

### Backend

```bash
cd backend/TestDashboard.API
dotnet run
# API available at http://localhost:5000
```

### Frontend

```bash
cd frontend/test-dashboard-ui
npm run dev
# App available at http://localhost:5173
```

### Tests

```bash
# Backend (14 tests)
cd backend && dotnet test --verbosity normal

cd frontend/test-dashboard-ui && npm test
```

## Key Design Decisions

- **Repository pattern** — `ITestResultRepository` decouples data access from
  business logic, enabling the service layer to be tested without a real database.
- **Service layer** — handles DTO mapping and business rules, keeping controllers thin.
- **DTOs** — separate read (`TestResultDto`) and write (`CreateTestResultDto`) models
  prevent over-posting and give fine-grained API control.
- **Interface-first design** — every dependency is injected through interfaces,
  enabling Moq-based unit testing throughout the backend.