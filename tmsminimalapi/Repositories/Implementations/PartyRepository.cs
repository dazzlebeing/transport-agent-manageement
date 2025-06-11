using Microsoft.EntityFrameworkCore;
using tmsminimalapi.Data;
using tmsminimalapi.Models;
using tmsminimalapi.Repositories.Interfaces;

namespace tmsminimalapi.Repositories.Implementations
{
    public class PartyRepository : IPartyRepository
    {
        private readonly TmsDbContext _context;

        public PartyRepository(TmsDbContext context)
        {
            _context = context;
        }

        public async Task<Party?> GetByIdAsync(Guid id)
        {
            return await _context.Parties.FindAsync(id);
        }

        public async Task<IEnumerable<Party>> GetAllAsync()
        {
            return await _context.Parties.ToListAsync();
        }

        public async Task<IEnumerable<Party>> SearchByNameAsync(string searchTerm)
        {
            return await _context.Parties
                .Where(p => p.PartyName.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task<Party> CreateAsync(Party party)
        {
            _context.Parties.Add(party);
            await _context.SaveChangesAsync();
            return party;
        }

        public async Task<Party> UpdateAsync(Party party)
        {
            _context.Entry(party).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return party;
        }

        public async Task DeleteAsync(Guid id)
        {
            var party = await _context.Parties.FindAsync(id);
            if (party != null)
            {
                _context.Parties.Remove(party);
                await _context.SaveChangesAsync();
            }
        }
    }
} 