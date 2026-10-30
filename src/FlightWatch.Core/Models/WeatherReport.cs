namespace FlightWatch.Core.Models;


// Flight category based on ceiling and visibility (FAA definitions).
// The worse of the two values decides the category.
//
//   Category   Ceiling (ft AGL)    Visibility (statute miles)
//   Vfr        > 3000        and   > 5
//   Mvfr       1000 to 3000  or    3 to 5
//   Ifr        500 to < 1000 or    1 to < 3
//   Lifr       < 500         or    < 1
//
// Values increase with severity (Vfr = 0, Lifr = 3), so rules can compare
// with plain operators, e.g. category >= FlightCategory.Ifr.
public enum FlightCategory
{
    Vfr = 0,
    Mvfr = 1,
    Ifr = 2,
    Lifr = 3,
}
// SI units, same convention as FlightState, for consistency across Core. The
// aviationweather.gov API itself returns aviation units (statute miles, knots, feet),
// not SI, so Infrastructure is responsible for converting on the way in.
public class WeatherReport
{
    public required string AirportIcaoCode { get; init; }
    public required DateTimeOffset ObservationTime { get; init; }
    public required string RawText { get; init; }
    public required FlightCategory FlightCategory { get; init; }
    public double? VisibilityMeters { get; init; } // Source: statute miles if
    public double? CeilingMeters { get; init; } // Source: feet (lowest broken/overcast cloud layer)
    public double? WindSpeedMetersPerSecond { get; init; } // Source: knots
    public double? WindGustMetersPerSecond { get; init; } // Source: knots
    public int? WindDirectionDegrees { get; init; } // Degrees from true north, no conversion needed
}
