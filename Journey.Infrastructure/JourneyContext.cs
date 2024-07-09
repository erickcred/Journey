using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Journey.Infrastructure;

public class JourneyContext : DbContext
{
  private readonly IConfiguration _config;

  public JourneyContext(DbContextOptions<JourneyContext> options) : base(options) { }

  public DbSet<Trip> Trips { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Activity>().ToTable("Activities");
  }

}