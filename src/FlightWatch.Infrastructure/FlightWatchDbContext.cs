using FlightWatch.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightWatch.Infrastructure;

public class FlightWatchDbContext : DbContext
{
    //Constructor for database. Will be set to postgresql
    public FlightWatchDbContext(DbContextOptions<FlightWatchDbContext> options) : base(options)
    {
    }

    //Each DbSet is a table in our database.
    public DbSet<Alert> Alerts => Set<Alert>();
    public DbSet<Aircraft> Aircraft => Set<Aircraft>();
    public DbSet<Airport> Airports => Set<Airport>();
    public DbSet<FlightState> FlightStates => Set<FlightState>();
    public DbSet<WeatherReport> WeatherReports => Set<WeatherReport>();
}
