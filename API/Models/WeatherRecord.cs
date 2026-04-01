using System;
using System.ComponentModel.DataAnnotations;

namespace WeatherApi.Models
{
    public class WeatherRecord
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string City { get; set; } = string.Empty;
        
        [Required]
        public double Temperature { get; set; }
        
        public double FeelsLike { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Description { get; set; } = string.Empty;
        
        public int Humidity { get; set; }
        
        public double WindSpeed { get; set; }
        
        public int Pressure { get; set; }
        
        [MaxLength(10)]
        public string Icon { get; set; } = "🌤️";
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
    }
    
    public class WeatherRequest
    {
        [Required]
        public string City { get; set; } = string.Empty;
    }
}