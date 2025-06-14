using tmsminimalapi.DTOs;
using tmsminimalapi.Models;

namespace tmsminimalapi.Services.Interfaces
{
    public interface IBookingService
    {
        Task<BookingResponseDTO> CreateBookingAsync(BookingCreateDTO bookingDto);
        Task<IEnumerable<BookingResponseDTO>> GetBookingsByStatusAsync(BookingStatus status);
    }
} 