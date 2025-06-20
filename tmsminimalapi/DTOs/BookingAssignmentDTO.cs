using tmsminimalapi.Models;

namespace tmsminimalapi.DTOs;

public record TruckAssignmentRequestDTO(
    string TruckNumber,
    string TruckSize,
    string DriverName,
    string DriverMobile,
    decimal AgreedAmount,
    decimal AdvancePaid,
    DateTime AssignmentDate,
    string? Notes
);

public record BookingAssignmentRequestDTO(
    Guid BookingId,
    List<TruckAssignmentRequestDTO> Assignments
);

public record TruckAssignmentResponseDTO(
    Guid Id,
    string TruckNumber,
    string TruckSize,
    string DriverName,
    string DriverMobile,
    decimal AgreedAmount,
    decimal AdvancePaid,
    DateTime AssignmentDate,
    string? Notes,
    DateTime CreatedAt
);

public record BookingAssignmentAnalyticsDTO(
    Guid BookingId,
    BookingStatus Status,
    int AssignmentsCreated,
    decimal TotalDriverCost,
    List<TruckAssignmentResponseDTO> Assignments
);

public record BookingAssignmentResponseDTO(
    bool Success,
    string Message,
    BookingAssignmentAnalyticsDTO Data
); 