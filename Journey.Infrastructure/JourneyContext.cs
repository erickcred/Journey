using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Infrastructure;

public class JourneyContext : DbContext
{
  public JourneyContext(DbContextOptions<JourneyContext> options) : base(options) { }

  public DbSet<Trip> Trips { get; set; }
  public DbSet<Activity> Activities { get; set; }

}