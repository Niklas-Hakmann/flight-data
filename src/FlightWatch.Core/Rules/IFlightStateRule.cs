using FlightWatch.Core.Models;

namespace FlightWatch.Core.Rules;

/// <summary>A rule evaluated against a single aircraft position sample.</summary>
public interface IFlightStateRule
{
    IEnumerable<Alert> Evaluate(FlightState state);
}
