namespace tmsminimalapi.DTOs;

public record PartyCreateDTO(
    string PartyName
);

public record PartyResponseDTO(
    Guid Id,
    string PartyName,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record PartySearchDTO(
    Guid Id,
    string PartyName
); 