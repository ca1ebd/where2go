using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Where2Go.API.Data;
using Where2Go.API.Models;
using System.Security.Cryptography;
using System.Text;

namespace Where2Go.API.Services
{
    public class PlaceService
    {
        private readonly ApplicationDbContext _context;

        public PlaceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Place> CreatePlaceAsync(Place place, string managementPassword)
        {
            place.Id = Guid.NewGuid();
            place.CreatedAt = DateTime.UtcNow;
            place.LastAccessedAt = DateTime.UtcNow;
            place.ShareableUrl = GenerateShareableUrl();
            place.ManagementPasswordHash = HashPassword(managementPassword);

            // Generate map links
            place.GoogleMapsLink = GenerateGoogleMapsLink(place.Address);
            place.AppleMapsLink = GenerateAppleMapsLink(place.Address);

            _context.Places.Add(place);
            await _context.SaveChangesAsync();

            return place;
        }

        public async Task<Place> GetPlaceByShareableUrlAsync(string shareableUrl)
        {
            var place = await _context.Places.FirstOrDefaultAsync(p => p.ShareableUrl == shareableUrl);
            if (place != null)
            {
                place.LastAccessedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
            return place;
        }

        public async Task<bool> ValidateManagementPasswordAsync(string shareableUrl, string password)
        {
            var place = await _context.Places.FirstOrDefaultAsync(p => p.ShareableUrl == shareableUrl);
            if (place == null) return false;

            return place.ManagementPasswordHash == HashPassword(password);
        }

        private string GenerateShareableUrl()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 8);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private string GenerateGoogleMapsLink(string address)
        {
            var encodedAddress = Uri.EscapeDataString(address);
            return $"https://www.google.com/maps/search/?api=1&query={encodedAddress}";
        }

        private string GenerateAppleMapsLink(string address)
        {
            var encodedAddress = Uri.EscapeDataString(address);
            return $"maps://maps.apple.com/?q={encodedAddress}";
        }
    }
} 