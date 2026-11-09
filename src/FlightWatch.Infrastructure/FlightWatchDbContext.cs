using FlightWatch.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightWatch.Infrastructure;

public class FlightWatchDbContext : DbContext
{
    public FlightWatchDbContext(DbContextOptions<FlightWatchDbContext> options) : base(options)
    {
    }

    public DbSet<Alert> Alerts => Set<Alert>();
}
