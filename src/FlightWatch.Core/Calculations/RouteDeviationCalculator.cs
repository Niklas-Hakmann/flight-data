using FlightWatch.Core.Models;

namespace FlightWatch.Core.Calculations;

public static class RouteDeviationCalculator
{
    public static double DeviationMeters(GeoPoint position, Route route)
    {
        // Shortest distance, in meters, from a position to any segment of the route's waypoints.
        // Returns 0 for a route with fewer than two waypoints (nothing to deviate from).
        if (route.Waypoints.Count < 2)
        {
            return 0;
        }

        var minDistance = double.MaxValue;
        for (var i = 0; i < route.Waypoints.Count - 1; i++)
        {
            var distance = GeoCalculator.DistanceToSegmentMeters(position, route.Waypoints[i], route.Waypoints[i + 1]);
            minDistance = Math.Min(minDistance, distance);
        }

        return minDistance;
    }
}
