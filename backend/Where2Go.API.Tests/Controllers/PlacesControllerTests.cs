using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using Where2Go.API.Controllers;
using Where2Go.API.Services;
using Where2Go.API.Models;

namespace Where2Go.API.Tests.Controllers
{
    public class PlacesControllerTests
    {
        private readonly Mock<IPlaceService> _mockPlaceService;
        private readonly PlacesController _controller;

        public PlacesControllerTests()
        {
            _mockPlaceService = new Mock<IPlaceService>();
            _controller = new PlacesController(_mockPlaceService.Object);
        }

        [Fact]
        public async Task CreatePlace_ShouldReturnCreatedAtAction_WhenValidRequest()
        {
            // Arrange
            var request = new PlaceCreateRequest
            {
                Address = "123 Test St",
                Description = "Test Description",
                SpecialInstructions = "Test Instructions",
                ImageUrl = "http://test.com/image.jpg",
                ManagementPassword = "testPassword123"
            };

            var expectedPlace = new Place
            {
                Id = Guid.NewGuid(),
                Address = request.Address,
                Description = request.Description,
                SpecialInstructions = request.SpecialInstructions,
                ImageUrl = request.ImageUrl,
                ShareableUrl = "test1234"
            };

            _mockPlaceService.Setup(s => s.CreatePlaceAsync(It.IsAny<Place>(), request.ManagementPassword))
                .ReturnsAsync(expectedPlace);

            // Act
            var result = await _controller.CreatePlace(request);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(PlacesController.GetPlace), createdAtActionResult.ActionName);
            Assert.Equal(expectedPlace.ShareableUrl, createdAtActionResult.RouteValues["shareableUrl"]);
            Assert.Equal(expectedPlace, createdAtActionResult.Value);
        }

        [Fact]
        public async Task GetPlace_ShouldReturnPlace_WhenExists()
        {
            // Arrange
            var shareableUrl = "test1234";
            var expectedPlace = new Place
            {
                Id = Guid.NewGuid(),
                Address = "123 Test St",
                Description = "Test Description",
                ShareableUrl = shareableUrl
            };

            _mockPlaceService.Setup(s => s.GetPlaceByShareableUrlAsync(shareableUrl))
                .ReturnsAsync(expectedPlace);

            // Act
            var result = await _controller.GetPlace(shareableUrl);

            // Assert
            var okResult = Assert.IsType<ActionResult<Place>>(result);
            Assert.Equal(expectedPlace, okResult.Value);
        }

        [Fact]
        public async Task GetPlace_ShouldReturnNotFound_WhenPlaceDoesNotExist()
        {
            // Arrange
            var shareableUrl = "nonexistent";
            _mockPlaceService.Setup(s => s.GetPlaceByShareableUrlAsync(shareableUrl))
                .ReturnsAsync((Place)null);

            // Act
            var result = await _controller.GetPlace(shareableUrl);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task ValidatePassword_ShouldReturnTrue_WhenPasswordIsValid()
        {
            // Arrange
            var shareableUrl = "test1234";
            var request = new PasswordValidationRequest { Password = "correctPassword" };
            _mockPlaceService.Setup(s => s.ValidateManagementPasswordAsync(shareableUrl, request.Password))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.ValidatePassword(shareableUrl, request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.True((bool)okResult.Value);
        }

        [Fact]
        public async Task ValidatePassword_ShouldReturnFalse_WhenPasswordIsInvalid()
        {
            // Arrange
            var shareableUrl = "test1234";
            var request = new PasswordValidationRequest { Password = "wrongPassword" };
            _mockPlaceService.Setup(s => s.ValidateManagementPasswordAsync(shareableUrl, request.Password))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.ValidatePassword(shareableUrl, request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.False((bool)okResult.Value);
        }
    }
} 