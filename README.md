# FlightWatch

Live ADS-B flight tracking and weather monitoring with rule-based alerts.

Fetches aircraft positions from [OpenSky Network](https://openskynetwork.github.io/opensky-api/) and METAR/TAF
weather data from [aviationweather.gov](https://aviationweather.gov/data/api/), stores them in PostgreSQL, and
displays them in an Avalonia desktop app with a map and alert list.

## Architecture

| Project                      | Responsibility                                                                                            |
| ---------------------------- | --------------------------------------------------------------------------------------------------------- |
| `FlightWatch.Core`           | Domain models, rules and data handling (calculations etc.)                                                |
| `FlightWatch.Infrastructure` | EF Core/Postgres, OpenSky and weather API clients. Methods for fetching and saving data.                  |
| `FlightWatch.Worker`         | Background service that polls data and writes it to the database. Uses .Core and .Infrastructure          |
| `FlightWatch.Desktop`        | UI. Avalonia MVVM app, reads from the database and shows the map + alerts. Uses .Core and .Infrastructure |

## Why this folder structure

- **Separation of concerns:** Worker should always be running, but desktop is only used when a user turns it on. They are very different in terms of deployment, so it makes sense to separate them as different projects.
- **Extensibility:** Another UI or a different channel for alarms/communication can easily be added as a new project. For example alarms through text messages, Slack and email.
- **Low coupling:** Changes to database and API only need to happen in .Infrastructure, as long as data from methods still fit .Core.

Having 4 different projects is not needed for a relatively small project. It has mainly been chosen to reflect on real-world architecture.

Core sets the rules for the rest of the code, so research into data types etc. should be thorough, so changes are minimal.

## Running locally

```bash
docker compose up -d postgres
dotnet run --project src/FlightWatch.Worker
dotnet run --project src/FlightWatch.Desktop
```
