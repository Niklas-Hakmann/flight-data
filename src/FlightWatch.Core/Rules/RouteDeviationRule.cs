using FlightWatch.Core.Calculations;
using FlightWatch.Core.Models;

namespace FlightWatch.Core.Rules;

/// <summary>Triggers an alert when an aircraft strays too far from its expected route.</summary>
public class RouteDeviationRule : IFlightStateRule
{
    public required Route Route { get; init; }
    public required double MaxDeviationMeters { get; init; }
    public AlertSeverity Severity { get; init; } = AlertSeverity.Warning;

    public IEnumerable<Alert> Evaluate(FlightState state)
    {
        var deviation = RouteDeviationCalculator.DeviationMeters(state.Position, Route);
        if (deviation <= MaxDeviationMeters)
        {
            yield break;
        }

        yield return new Alert
        {
            Type = AlertType.RouteDeviation,
            Severity = Severity,
            Timestamp = state.Timestamp,
            AircraftIcao24 = state.Icao24,
            Message = $"Aircraft {state.Icao24} is {deviation:F0} m off route \"{Route.Name}\" (max {MaxDeviationMeters:F0} m)",
        };
    }
}
