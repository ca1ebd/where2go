using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Where2Go.API.Models;
using Where2Go.API.Services;

namespace Where2Go.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlacesController : ControllerBase
    {
        private readonly IPlaceService _placeService;

        public PlacesController(IPlaceService placeService)
        {
            _placeService = placeService;
        }

        [HttpPost]
        public async Task<ActionResult<Place>> CreatePlace([FromBody] PlaceCreateRequest request)
        {
            var place = new Place
            {
                Address = request.Address,
                Description = request.Description,
                SpecialInstructions = request.SpecialInstructions,
                ImageUrl = request.ImageUrl
            };

            var createdPlace = await _placeService.CreatePlaceAsync(place, request.ManagementPassword);
            return CreatedAtAction(nameof(GetPlace), new { shareableUrl = createdPlace.ShareableUrl }, createdPlace);
        }

        [HttpGet("{shareableUrl}")]
        public async Task<ActionResult<Place>> GetPlace(string shareableUrl)
        {
            var place = await _placeService.GetPlaceByShareableUrlAsync(shareableUrl);
            if (place == null)
            {
                return NotFound();
            }

            return place;
        }

        [HttpPost("{shareableUrl}/validate")]
        public async Task<ActionResult<bool>> ValidatePassword(string shareableUrl, [FromBody] PasswordValidationRequest request)
        {
            var isValid = await _placeService.ValidateManagementPasswordAsync(shareableUrl, request.Password);
            return Ok(isValid);
        }
    }

    public class PlaceCreateRequest
    {
        public string Address { get; set; }
        public string Description { get; set; }
        public string SpecialInstructions { get; set; }
        public string ImageUrl { get; set; }
        public string ManagementPassword { get; set; }
    }

    public class PasswordValidationRequest
    {
        public string Password { get; set; }
    }
} 