using Microsoft.EntityFrameworkCore;
using tmsminimalapi.Data;
using tmsminimalapi.Models;
using tmsminimalapi.Services.Interfaces;

namespace tmsminimalapi.Services.Implementations;

public class CityService : ICityService
{
    private readonly TmsDbContext _context;

    public CityService(TmsDbContext context)
    {
        _context = context;
    }

    public async Task<City> GetOrCreateCityAsync(string cityName)
    {
        var normalizedName = cityName.ToLowerInvariant();
        var city = await _context.Cities
            .FirstOrDefaultAsync(c => c.Name == normalizedName);

        if (city == null)
        {
            city = new City
            {
                Id = Guid.NewGuid(),
                Name = normalizedName,
                CreatedAt = DateTime.UtcNow
            };
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();
        }

        return city;
    }

    public async Task<IEnumerable<City>> SearchCitiesAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return Enumerable.Empty<City>();

        var normalizedSearch = searchTerm.ToLowerInvariant();
        return await _context.Cities
            .Where(c => c.Name.Contains(normalizedSearch))
            .OrderBy(c => c.Name)
            .Take(10)
            .ToListAsync();
    }
} 