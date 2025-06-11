using tmsminimalapi.Models;

namespace tmsminimalapi.Repositories.Interfaces
{
    public interface IPartyRepository
    {
        Task<Party?> GetByIdAsync(Guid id);
        Task<IEnumerable<Party>> GetAllAsync();
        Task<IEnumerable<Party>> SearchByNameAsync(string searchTerm);
        Task<Party> CreateAsync(Party party);
        Task<Party> UpdateAsync(Party party);
        Task DeleteAsync(Guid id);
    }
} 