namespace tmsminimalapi.DTOs;

public record BookingCreateDTO(
    Guid PartyId,
    string SourceCity,
    string DestinationCity,
    DateTime BookingDate,
    decimal TotalAmount,
    decimal PaidByParty,
    string? Notes
);

public record BookingResponseDTO(
    Guid Id,
    Guid PartyId,
    string PartyName,
    string SourceCity,
    string DestinationCity,
    DateTime BookingDate,
    decimal TotalAmount,
    decimal PaidByParty,
    string? Notes,
    DateTime CreatedAt
); 