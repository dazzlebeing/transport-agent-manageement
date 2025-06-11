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

        public BookingService(TmsDbContext context)
        {
            _context = context;
        }

        public async Task<BookingResponseDTO> CreateBookingAsync(BookingCreateDTO bookingDto)
        {
            var party = await _context.Parties.FindAsync(bookingDto.PartyId);
            if (party == null)
            {
                throw new KeyNotFoundException($"Party with ID {bookingDto.PartyId} not found");
            }

            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                PartyId = bookingDto.PartyId,
                SourceCity = bookingDto.SourceCity,
                DestinationCity = bookingDto.DestinationCity,
                BookingDate = bookingDto.BookingDate,
                TotalAmount = bookingDto.TotalAmount,
                PaidByParty = bookingDto.PaidByParty,
                Notes = bookingDto.Notes,
                CreatedAt = DateTime.UtcNow
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return new BookingResponseDTO(
                booking.Id,
                booking.PartyId,
                party.PartyName,
                booking.SourceCity,
                booking.DestinationCity,
                booking.BookingDate,
                booking.TotalAmount,
                booking.PaidByParty,
                booking.Notes,
                booking.CreatedAt
            );
        }
    }
} 