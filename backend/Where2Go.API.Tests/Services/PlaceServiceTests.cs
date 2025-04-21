using System;
using System.Threading.Tasks;
using Xunit;
using Where2Go.API.Services;

namespace Where2Go.API.Tests.Services
{
    public class PlaceServiceTests
    {
        private readonly PlaceService _service;

        public PlaceServiceTests()
        {
            _service = new PlaceService(null); // We'll only test methods that don't require the database
        }

        [Fact]
        public void GenerateGoogleMapsLink_ShouldReturnValidUrl()
        {
            // Arrange
            var address = "123 Test St, City, Country";

            // Act
            var result = _service.GenerateGoogleMapsLink(address);

            // Assert
            Assert.Contains("google.com/maps", result);
            Assert.Contains(Uri.EscapeDataString(address), result);
        }

        [Fact]
        public void GenerateAppleMapsLink_ShouldReturnValidUrl()
        {
            // Arrange
            var address = "123 Test St, City, Country";

            // Act
            var result = _service.GenerateAppleMapsLink(address);

            // Assert
            Assert.Contains("maps.apple.com", result);
            Assert.Contains(Uri.EscapeDataString(address), result);
        }

        [Fact]
        public void HashPassword_ShouldReturnConsistentHash()
        {
            // Arrange
            var password = "testPassword123";

            // Act
            var hash1 = _service.HashPassword(password);
            var hash2 = _service.HashPassword(password);

            // Assert
            Assert.NotNull(hash1);
            Assert.NotNull(hash2);
            Assert.Equal(hash1, hash2);
        }

        [Fact]
        public void HashPassword_ShouldReturnDifferentHashesForDifferentPasswords()
        {
            // Arrange
            var password1 = "testPassword123";
            var password2 = "testPassword456";

            // Act
            var hash1 = _service.HashPassword(password1);
            var hash2 = _service.HashPassword(password2);

            // Assert
            Assert.NotEqual(hash1, hash2);
        }
    }
} 