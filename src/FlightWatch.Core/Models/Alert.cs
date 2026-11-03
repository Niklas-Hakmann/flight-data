namespace FlightWatch.Core.Models;

public enum AlertSeverity
{
    Info,
    Warning,
    Critical,
}

public enum AlertType
{
    GeofenceEntry,
    AirportWeather,
    RouteDeviation,
}

public class Alert
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required AlertType Type { get; init; }
    public required AlertSeverity Severity { get; init; }
    public required string Message { get; init; }
    public required DateTimeOffset Timestamp { get; init; }
    public string? AircraftIcao24 { get; init; }
    public string? AirportIcaoCode { get; init; }
    
}
