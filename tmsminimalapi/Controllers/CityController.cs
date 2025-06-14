using Microsoft.AspNetCore.Mvc;
using tmsminimalapi.Services.Interfaces;

namespace tmsminimalapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CityController : ControllerBase
{
    private readonly ICityService _cityService;

    public CityController(ICityService cityService)
    {
        _cityService = cityService;
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchCities([FromQuery] string searchTerm)
    {
        var cities = await _cityService.SearchCitiesAsync(searchTerm);
        return Ok(cities);
    }
} 