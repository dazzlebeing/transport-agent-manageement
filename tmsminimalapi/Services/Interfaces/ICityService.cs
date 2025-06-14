using tmsminimalapi.Models;

namespace tmsminimalapi.Services.Interfaces;

public interface ICityService
{
    Task<City> GetOrCreateCityAsync(string cityName);
    Task<IEnumerable<City>> SearchCitiesAsync(string searchTerm);
} 