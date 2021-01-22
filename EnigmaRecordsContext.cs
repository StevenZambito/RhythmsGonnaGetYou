using Microsoft.EntityFrameworkCore;

namespace RhythmsGonnaGetYou
{
    public class EnigmaRecordsContext : DbContext
    {
        public DbSet<Artist> Artists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("server=localhost;database=EnigmaRecords");
        }
    }
}