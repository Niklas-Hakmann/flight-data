namespace FlightWatch.Core.Models;

/// <summary>
/// A user-defined expected corridor for a flight, used to detect route deviation.
/// There is no live flight-plan source, so waypoints are configured manually.
/// </summary>
public class Route
{
    public required string Name { get; init; }
    public required IReadOnlyList<GeoPoint> Waypoints { get; init; }
}
