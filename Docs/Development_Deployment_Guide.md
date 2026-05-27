# Development and Deployment Guide
This guide is focused on practical developer onboarding and day-to-day development.

---

## 1. Current Architecture

### Backend

- ASP.NET Core MVC (.NET 10)

### Frontend
- Razor Pages 
- Bootstrap for styling

---
## 2. Prerequisites
- .NET 10 SDK
- Git
- (Optional) Visual Studio, Visual Studio Code or any other code editor of choice for better development experience
- Terminal or command line interface for running commands

Required access:
- GitHub repository access

Verify SDK installation:

```bash
dotnet --version
```

---
## 3. Developer Quickstart (Get Up and Running)

## 3.1 Clone Repository

```bash
git clone https://github.com/HonzaPhan/Task_DirectoryTracker.git
cd Task_DirectoryTracker
```

---

## 3.2 Restore Dependencies

```bash
dotnet restore Task_DirectoryTracker.slnx
```

---

## 3.3 Build Solution

```bash
dotnet build Task_DirectoryTracker.slnx
```

## 3.4 Run Application

```bash
dotnet run --project DirectoryTracker
```

Application URLs:

- HTTPS: `https://localhost:7226`
- HTTP: `http://localhost:5251`

# 4. Development Workflow

## Run with Hot Reload

Recommended during development:

```bash
dotnet watch --project DirectoryTracker.Web
```

---

## Environment

Default environment:

```text
Development
```

# 5. Configuration

Primary configuration files:

```text
appsettings.json
appsettings.Development.json
```

# 6. Recommended Improvements