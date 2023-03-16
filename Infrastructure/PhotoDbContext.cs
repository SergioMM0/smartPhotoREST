using Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class PhotoDbContext : DbContext
{
    public PhotoDbContext(DbContextOptions<PhotoDbContext> opts) : base(opts)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Photo>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
    }
    
    public DbSet<Photo> PhotoTable { get; set; }
}