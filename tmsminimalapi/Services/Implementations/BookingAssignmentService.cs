using Microsoft.EntityFrameworkCore;
using tmsminimalapi.Data;
using tmsminimalapi.DTOs;
using tmsminimalapi.Models;
using tmsminimalapi.Services.Interfaces;

namespace tmsminimalapi.Services.Implementations;

public class BookingAssignmentService : IBookingAssignmentService
{
    private readonly TmsDbContext _context;

    public BookingAssignmentService(TmsDbContext context)
    {
        _context = context;
    }

    public async Task<BookingAssignmentResponseDTO> AssignTrucksToBookingAsync(BookingAssignmentRequestDTO request)
    {
        // Verify booking exists and is in Pending status
        var booking = await _context.Bookings
            .FirstOrDefaultAsync(b => b.Id == request.BookingId);

        if (booking == null)
        {
            throw new KeyNotFoundException($"Booking with ID {request.BookingId} not found");
        }

        if (booking.Status != BookingStatus.Pending)
        {
            throw new InvalidOperationException($"Booking must be in Pending status to assign trucks. Current status: {booking.Status}");
        }

        var assignments = new List<TruckAssignmentResponseDTO>();

        foreach (var assignmentRequest in request.Assignments)
        {
            // Find or create truck
            var truck = await _context.Trucks
                .FirstOrDefaultAsync(t => t.TruckNumber == assignmentRequest.TruckNumber);

            if (truck == null)
            {
                truck = new Truck
                {
                    Id = Guid.NewGuid(),
                    TruckNumber = assignmentRequest.TruckNumber,
                    TruckSize = assignmentRequest.TruckSize,
                    CreatedAt = DateTime.UtcNow
                };
                _context.Trucks.Add(truck);
            }

            // Find or create driver
            var driver = await _context.Drivers
                .FirstOrDefaultAsync(d => d.DriverMobile == assignmentRequest.DriverMobile);

            if (driver == null)
            {
                driver = new Driver
                {
                    Id = Guid.NewGuid(),
                    DriverName = assignmentRequest.DriverName,
                    DriverMobile = assignmentRequest.DriverMobile,
                    CreatedAt = DateTime.UtcNow
                };
                _context.Drivers.Add(driver);
            }

            // Create assignment
            var assignment = new BookingTruckAssignment
            {
                Id = Guid.NewGuid(),
                BookingId = request.BookingId,
                TruckId = truck.Id,
                DriverId = driver.Id,
                AgreedAmount = assignmentRequest.AgreedAmount,
                AdvancePaid = assignmentRequest.AdvancePaid,
                AssignmentDate = assignmentRequest.AssignmentDate,
                Notes = assignmentRequest.Notes,
                CreatedAt = DateTime.UtcNow
            };

            _context.BookingTruckAssignments.Add(assignment);

            assignments.Add(new TruckAssignmentResponseDTO(
                assignment.Id,
                truck.TruckNumber,
                truck.TruckSize,
                driver.DriverName,
                driver.DriverMobile,
                assignment.AgreedAmount,
                assignment.AdvancePaid,
                assignment.AssignmentDate,
                assignment.Notes,
                assignment.CreatedAt
            ));
        }

        // Update booking status to Assigned
        booking.Status = BookingStatus.Assigned;
        booking.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return new BookingAssignmentResponseDTO(request.BookingId, assignments);
    }

    public async Task<BookingAssignmentResponseDTO> GetBookingAssignmentsAsync(Guid bookingId)
    {
        var assignments = await _context.BookingTruckAssignments
            .Include(a => a.Truck)
            .Include(a => a.Driver)
            .Where(a => a.BookingId == bookingId)
            .Select(a => new TruckAssignmentResponseDTO(
                a.Id,
                a.Truck.TruckNumber,
                a.Truck.TruckSize,
                a.Driver.DriverName,
                a.Driver.DriverMobile,
                a.AgreedAmount,
                a.AdvancePaid,
                a.AssignmentDate,
                a.Notes,
                a.CreatedAt
            ))
            .ToListAsync();

        return new BookingAssignmentResponseDTO(bookingId, assignments);
    }
} 