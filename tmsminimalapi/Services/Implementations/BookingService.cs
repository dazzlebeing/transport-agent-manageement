using Microsoft.EntityFrameworkCore;
using tmsminimalapi.Data;
using tmsminimalapi.DTOs;
using tmsminimalapi.Models;
using tmsminimalapi.Services.Interfaces;

namespace tmsminimalapi.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly TmsDbContext _context;
        private readonly ICityService _cityService;

        public BookingService(TmsDbContext context, ICityService cityService)
        {
            _context = context;
            _cityService = cityService;
        }

        public async Task<BookingResponseDTO> CreateBookingAsync(BookingCreateDTO bookingDto)
        {
            var party = await _context.Parties.FindAsync(bookingDto.PartyId);
            if (party == null)
            {
                throw new KeyNotFoundException($"Party with ID {bookingDto.PartyId} not found");
            }

            var sourceCity = await _cityService.GetOrCreateCityAsync(bookingDto.SourceCity);
            var destinationCity = await _cityService.GetOrCreateCityAsync(bookingDto.DestinationCity);

            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                PartyId = bookingDto.PartyId,
                SourceCityId = sourceCity.Id,
                DestinationCityId = destinationCity.Id,
                BookingDate = bookingDto.BookingDate,
                TotalAmount = bookingDto.TotalAmount,
                PaidByParty = bookingDto.PaidByParty,
                Notes = bookingDto.Notes,
                Status = BookingStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return new BookingResponseDTO(
                booking.Id,
                booking.PartyId,
                party.PartyName,
                sourceCity.Name,
                destinationCity.Name,
                booking.BookingDate,
                booking.TotalAmount,
                booking.PaidByParty,
                booking.Status,
                booking.Notes,
                booking.CreatedAt
            );
        }

        public async Task<IEnumerable<BookingResponseDTO>> GetBookingsByStatusAsync(BookingStatus status)
        {
            var bookings = await _context.Bookings
                .Include(b => b.Party)
                .Include(b => b.SourceCity)
                .Include(b => b.DestinationCity)
                .Where(b => b.Status == status)
                .ToListAsync();

            return bookings.Select(b => new BookingResponseDTO(
                b.Id,
                b.PartyId,
                b.Party.PartyName,
                b.SourceCity.Name,
                b.DestinationCity.Name,
                b.BookingDate,
                b.TotalAmount,
                b.PaidByParty,
                b.Status,
                b.Notes,
                b.CreatedAt
            ));
        }
    }
} 