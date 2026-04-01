using Microsoft.EntityFrameworkCore;
using WeatherApi.Models;

namespace WeatherApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<WeatherRecord> WeatherRecords { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherRecord>()
                .HasIndex(w => w.City)
                .IsUnique(false); // Permitir múltiples registros por ciudad
            
            // Datos semilla (opcional)
            modelBuilder.Entity<WeatherRecord>().HasData(
                new WeatherRecord
                {
                    Id = 1,
                    City = "Madrid",
                    Temperature = 22.5,
                    FeelsLike = 23.0,
                    Description = "Soleado",
                    Humidity = 45,
                    WindSpeed = 12.3,
                    Pressure = 1015,
                    Icon = "☀️",
                    CreatedAt = DateTime.UtcNow
                },
                new WeatherRecord
                {
                    Id = 2,
                    City = "Barcelona",
                    Temperature = 19.8,
                    FeelsLike = 20.2,
                    Description = "Parcialmente nublado",
                    Humidity = 65,
                    WindSpeed = 8.5,
                    Pressure = 1012,
                    Icon = "⛅",
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
}