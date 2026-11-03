namespace FlightWatch.Core.Models;

/// <summary>
/// Identity of a tracked aircraft. Icao24 is the 24-bit ICAO transponder address
/// (hex string, e.g. "4b1805") and is stable for the aircraft's lifetime, unlike Callsign.
/// </summary>
public class Aircraft
{
    public required string Icao24 { get; init; }
    public string? Callsign { get; init; }
    public string? OriginCountry { get; init; }
}
