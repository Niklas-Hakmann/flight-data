namespace FlightWatch.Core.Models;

// SI units. Unlike WeatherReport, OpenSky's state vectors already report altitude and
// velocity in meters/meters-per-second, so Infrastructure can pass these through as-is.
public class FlightState
{
    public required string Icao24 { get; init; } // Source: icao24, transponder adress
    public required DateTimeOffset Timestamp { get; init; }
    public required GeoPoint Position { get; init; } // Source: latitude/longitude
    public double? AltitudeMeters { get; init; } // Source: baro_altitude, meters above sea level
    public double? GroundSpeedMetersPerSecond { get; init; } // Source: velocity, speed over ground (not airspeed), already m/s
    public double? VerticalRateMetersPerSecond { get; init; } // Source: vertical_rate, already m/s
    public double? TrackDegrees { get; init; } // Source: degrees clockwise from true north
    public bool OnGround { get; init; }
}
