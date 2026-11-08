using FlightWatch.Core.Models;

namespace FlightWatch.Core.Calculations;

public static class ClimbRateCalculator
{
    public static double? ComputeVerticalRateMetersPerSecond(FlightState previous, FlightState current)
    {
        // Vertical rate in meters/second between two samples of the same aircraft, computed
        // from altitude change over time. Useful as a fallback when a source doesn't report
        // vertical rate directly. Returns null if altitude is missing on either sample or the
        // samples are not in chronological order.
        if (previous.AltitudeMeters is not double previousAltitude || current.AltitudeMeters is not double currentAltitude)
        {
            return null;
        }

        var elapsedSeconds = (current.Timestamp - previous.Timestamp).TotalSeconds;
        if (elapsedSeconds <= 0)
        {
            return null;
        }

        return (currentAltitude - previousAltitude) / elapsedSeconds;
    }
}
