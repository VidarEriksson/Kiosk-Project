using Microsoft.EntityFrameworkCore;

namespace InfoService.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<TextFileContent> TextFiles { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TextFileContent>().ToTable("TxtFileContents");
        }
    }
}
