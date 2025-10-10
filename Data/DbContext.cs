using Microsoft.EntityFrameworkCore;
using TimeTrackerWPF.Shared;

namespace TimeTrackerWPF.Data
{
    public class TimeTrackerDbContext : DbContext
    {
        public DbSet<Record> Records { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=TimeTracker.db");
        }
    }
}
