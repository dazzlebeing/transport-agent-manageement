using tmsminimalapi.DTOs;

namespace tmsminimalapi.Services.Interfaces
{
    public interface IPartyService
    {
        Task<PartyResponseDTO> CreatePartyAsync(PartyCreateDTO partyDto);
        Task<IEnumerable<PartySearchDTO>> SearchPartiesAsync(string query);
    }
} 