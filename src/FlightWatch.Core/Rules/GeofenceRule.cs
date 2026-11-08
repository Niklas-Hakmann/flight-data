using FlightWatch.Core.Calculations;
using FlightWatch.Core.Models;

namespace FlightWatch.Core.Rules;

/// <summary>Triggers an alert when an aircraft enters a circular zone.</summary>
public class GeofenceRule : IFlightStateRule
{
    public required string Name { get; init; }
    public required GeoPoint Center { get; init; }
    public required double RadiusMeters { get; init; }
    public AlertSeverity Severity { get; init; } = AlertSeverity.Warning;

    public IEnumerable<Alert> Evaluate(FlightState state)
    {
        var distance = GeoCalculator.DistanceMeters(state.Position, Center);
        if (distance > RadiusMeters)
        {
            yield break;
        }

        yield return new Alert
        {
            Type = AlertType.GeofenceEntry,
            Severity = Severity,
            Timestamp = state.Timestamp,
            AircraftIcao24 = state.Icao24,
            Message = $"Aircraft {state.Icao24} entered geofence \"{Name}\" ({distance:F0} m from center)",
        };
    }
}
