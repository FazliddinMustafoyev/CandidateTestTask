using Microsoft.EntityFrameworkCore;
using CandidateTestTask.Domain.Entities;

namespace CandidateTestTask.DataAcces.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
    }
}