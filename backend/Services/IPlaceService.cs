using System.Threading.Tasks;
using Where2Go.API.Models;

namespace Where2Go.API.Services
{
    public interface IPlaceService
    {
        Task<Place> CreatePlaceAsync(Place place, string managementPassword);
        Task<Place> GetPlaceByShareableUrlAsync(string shareableUrl);
        Task<bool> ValidateManagementPasswordAsync(string shareableUrl, string password);
        string HashPassword(string password);
        string GenerateGoogleMapsLink(string address);
        string GenerateAppleMapsLink(string address);
    }
} 