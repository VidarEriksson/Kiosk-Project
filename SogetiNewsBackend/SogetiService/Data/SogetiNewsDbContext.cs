using Microsoft.EntityFrameworkCore;
using SogetiService.Data;

namespace SogetiService.Data
{
    public class SogetiNewsDbContext : DbContext
    {
        public DbSet<Post> Posts => Set<Post>();


        public SogetiNewsDbContext(DbContextOptions<SogetiNewsDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().HasIndex(p => p.TagId);
        }
    }
}
