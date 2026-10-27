# FlightWatch

Live ADS-B flight tracking and weather monitoring with rule-based alerts.

Fetches aircraft positions from [OpenSky Network](https://openskynetwork.github.io/opensky-api/) and METAR/TAF
weather data from [aviationweather.gov](https://aviationweather.gov/data/api/), stores them in PostgreSQL, and
displays them in an Avalonia desktop app with a map and alert list.

## Architecture

| Project | Responsibility |
|---|---|
| `FlightWatch.Core` | Domain models, rules and data handling (calculations etc.) |
| `FlightWatch.Infrastructure` | EF Core/Postgres, OpenSky and weather API clients. Fetches and  |
| `FlightWatch.Worker` | Background service that polls data and writes it to the database. Uses .Core and .Infrastructure |
| `FlightWatch.Desktop` | UI. Avalonia MVVM app, reads from the database and shows the map + alerts. Uses .Core and .Infrastructure|

## Running locally

```bash
docker compose up -d postgres
dotnet run --project src/FlightWatch.Worker
dotnet run --project src/FlightWatch.Desktop
```
