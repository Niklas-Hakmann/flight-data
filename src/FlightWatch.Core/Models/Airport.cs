namespace FlightWatch.Core.Models;

public class Airport
{
    public required string IcaoCode { get; init; }
    public required string Name { get; init; }
    public required GeoPoint Position { get; init; }
    public double ElevationMeters { get; init; }
}
