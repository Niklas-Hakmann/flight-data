using FlightWatch.Core.Models;

namespace FlightWatch.Core.Rules;

/// <summary>A rule evaluated against a weather observation for one airport.</summary>
public interface IWeatherRule
{
    IEnumerable<Alert> Evaluate(WeatherReport report);
}
