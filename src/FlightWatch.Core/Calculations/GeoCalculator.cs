using FlightWatch.Core.Models;

namespace FlightWatch.Core.Calculations;

public static class GeoCalculator
{
    private const double EarthRadiusMeters = 6_371_000;

    public static double DistanceMeters(GeoPoint a, GeoPoint b)
    {
        // Haversine formula: great-circle distance on a sphere from two points' lat/lon.
        var lat1 = DegreesToRadians(a.Latitude);
        var lat2 = DegreesToRadians(b.Latitude);
        var deltaLat = DegreesToRadians(b.Latitude - a.Latitude);
        var deltaLon = DegreesToRadians(b.Longitude - a.Longitude);

        var sinLat = Math.Sin(deltaLat / 2);
        var sinLon = Math.Sin(deltaLon / 2);
        var h = sinLat * sinLat + Math.Cos(lat1) * Math.Cos(lat2) * sinLon * sinLon;

        return 2 * EarthRadiusMeters * Math.Asin(Math.Min(1, Math.Sqrt(h)));
    }

    public static double DistanceToSegmentMeters(GeoPoint point, GeoPoint segmentStart, GeoPoint segmentEnd)
    {
        // Shortest distance from a point to a line segment, in meters. Segments are flattened
        // onto a local equirectangular plane around the segment's midpoint; this is accurate
        // enough for route corridors spanning tens to low hundreds of kilometers.
        var originLat = DegreesToRadians((segmentStart.Latitude + segmentEnd.Latitude) / 2);
        var cosOriginLat = Math.Cos(originLat);

        var (px, py) = ToPlane(point, cosOriginLat);
        var (ax, ay) = ToPlane(segmentStart, cosOriginLat);
        var (bx, by) = ToPlane(segmentEnd, cosOriginLat);

        var abx = bx - ax;
        var aby = by - ay;
        var lengthSquared = abx * abx + aby * aby;

        double closestX, closestY;
        if (lengthSquared == 0)
        {
            (closestX, closestY) = (ax, ay);
        }
        else
        {
            // Vector projection of the point onto the segment, clamped to [0, 1] so the
            // closest point stays on the segment itself rather than its infinite extension.
            var t = Math.Clamp(((px - ax) * abx + (py - ay) * aby) / lengthSquared, 0, 1);
            closestX = ax + t * abx;
            closestY = ay + t * aby;
        }

        var dx = px - closestX;
        var dy = py - closestY;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    private static (double X, double Y) ToPlane(GeoPoint point, double cosOriginLat)
    {
        var x = EarthRadiusMeters * DegreesToRadians(point.Longitude) * cosOriginLat;
        var y = EarthRadiusMeters * DegreesToRadians(point.Latitude);
        return (x, y);
    }

    private static double DegreesToRadians(double degrees) => degrees * Math.PI / 180.0;
}
