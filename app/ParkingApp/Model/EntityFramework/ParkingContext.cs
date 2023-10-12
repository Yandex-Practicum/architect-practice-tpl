using Microsoft.EntityFrameworkCore;

namespace ParkingApp.Model.EntityFramework
{
    public class ParkingContext: DbContext
    {
        public string DbPath { get; }
        public ParkingContext() 
        {
            var path = Environment.CurrentDirectory;
            DbPath = Path.Join(path, "Database", "parking.db");
        }

        public DbSet<Manager> Managers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Spot> Spots { get; set; }
        public DbSet<EmailNotify> EmailNotifys { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
    }
}
