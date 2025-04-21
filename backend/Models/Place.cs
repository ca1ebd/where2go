using System;
using System.ComponentModel.DataAnnotations;

namespace Where2Go.API.Models
{
    public class Place
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string GoogleMapsLink { get; set; } = string.Empty;

        [Required]
        public string AppleMapsLink { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? SpecialInstructions { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        public string ManagementPasswordHash { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime LastAccessedAt { get; set; }

        public string ShareableUrl { get; set; } = string.Empty;
    }
} 