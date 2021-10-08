using Microsoft.EntityFrameworkCore;

namespace VehicleWebAPI.DAL
{
    public class VehicleWebAPIDbContext : DbContext
    {
        private readonly string _connectionString;

        public VehicleWebAPIDbContext(string connectionString = null)
        {
            _connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(_connectionString == null)
            {
                optionsBuilder.UseSqlite("Data Source=Vehicle.db");
            }
            else
            {
                optionsBuilder.UseSqlite(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
