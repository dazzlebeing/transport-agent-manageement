using Microsoft.EntityFrameworkCore;
using tmsminimalapi.Data;
using tmsminimalapi.DTOs;
using tmsminimalapi.Models;
using tmsminimalapi.Services.Interfaces;

namespace tmsminimalapi.Services.Implementations
{
    public class PartyService : IPartyService
    {
        private readonly TmsDbContext _context;

        public PartyService(TmsDbContext context)
        {
            _context = context;
        }

        public async Task<PartyResponseDTO> CreatePartyAsync(PartyCreateDTO partyDto)
        {
            var party = new Party
            {
                Id = Guid.NewGuid(),
                PartyName = partyDto.PartyName,
                CreatedAt = DateTime.UtcNow
            };

            _context.Parties.Add(party);
            await _context.SaveChangesAsync();

            return new PartyResponseDTO(
                party.Id,
                party.PartyName,
                party.CreatedAt,
                party.UpdatedAt
            );
        }

        public async Task<IEnumerable<PartySearchDTO>> SearchPartiesAsync(string query)
        {
            return await _context.Parties
                .Where(p => p.PartyName.Contains(query))
                .OrderBy(p => p.PartyName)
                .Take(10)
                .Select(p => new PartySearchDTO(p.Id, p.PartyName))
                .ToListAsync();
        }
    }
} 