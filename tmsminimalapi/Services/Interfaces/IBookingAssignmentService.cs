using tmsminimalapi.DTOs;

namespace tmsminimalapi.Services.Interfaces;

public interface IBookingAssignmentService
{
    Task<BookingAssignmentResponseDTO> AssignTrucksToBookingAsync(BookingAssignmentRequestDTO request);
    Task<BookingAssignmentResponseDTO> GetBookingAssignmentsAsync(Guid bookingId);
} 