# PracticeLogApi

ASP.NET Core Minimal API (.NET 10) a Microsoft Learn
[Tutorial: Create a Minimal API with ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-10.0&tabs=visual-studio)
alapján. A Todo példa helyett gitárgyakorlási naplót kezel
(dal címe, hangolás, cél-BPM, megtanulva-e).

## Végpontok

| API | Leírás |
|-----|--------|
| `GET /practicesessions` | Összes tétel |
| `GET /practicesessions/mastered` | Csak a megtanult dalok |
| `GET /practicesessions/{id}` | Egy tétel ID alapján |
| `POST /practicesessions` | Új tétel |
| `PUT /practicesessions/{id}` | Teljes frissítés |
| `PATCH /practicesessions/{id}` | Részleges frissítés |
| `DELETE /practicesessions/{id}` | Törlés |

## Futtatás

Visual Studio 2026 → F5, majd a `PracticeLogApi.http` fájlból a kérések elküldése
(*Send request*). Az adatbázis in-memory, újraindításkor kiürül.

## Fejezetenkénti jegyzetek

### 1. Create an API project

ASP.NET Core Empty sablon, .NET 10. A `Program.cs` egyetlen `GET /` végpontot tartalmaz (`Hello World!`).

### 2. Add NuGet packages

`Microsoft.EntityFrameworkCore.InMemory` (in-memory adatbázis) és `Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore` (adatbázis-hibák fejlesztői oldala).
