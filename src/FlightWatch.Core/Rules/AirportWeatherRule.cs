using FlightWatch.Core.Models;

namespace FlightWatch.Core.Rules;

/// <summary>Triggers an alert when a monitored airport's conditions drop below a minimum flight category.</summary>
public class AirportWeatherRule : IWeatherRule
{
    public required string AirportIcaoCode { get; init; }
    public required FlightCategory MinimumAcceptableCategory { get; init; }
    public AlertSeverity Severity { get; init; } = AlertSeverity.Warning;

    public IEnumerable<Alert> Evaluate(WeatherReport report)
    {
        if (report.AirportIcaoCode != AirportIcaoCode || report.FlightCategory <= MinimumAcceptableCategory)
        {
            yield break;
        }

        yield return new Alert
        {
            Type = AlertType.AirportWeather,
            Severity = Severity,
            Timestamp = report.ObservationTime,
            AirportIcaoCode = report.AirportIcaoCode,
            Message = $"{report.AirportIcaoCode} weather is {report.FlightCategory} (below minimum {MinimumAcceptableCategory}): {report.RawText}",
        };
    }
}
