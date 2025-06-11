using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tmsminimalapi.Models;

public enum BookingStatus
{
    Pending,
    Assigned,
    Completed
}

public class Booking
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [Column("party_id")]
    public Guid PartyId { get; set; }

    [Required]
    [Column("source_city")]
    public string SourceCity { get; set; } = string.Empty;

    [Required]
    [Column("destination_city")]
    public string DestinationCity { get; set; } = string.Empty;

    [Required]
    [Column("booking_date")]
    public DateTime BookingDate { get; set; }

    [Required]
    [Column("total_amount")]
    public decimal TotalAmount { get; set; }

    [Required]
    [Column("paid_by_party")]
    public decimal PaidByParty { get; set; }

    [Required]
    [Column("status")]
    public BookingStatus Status { get; set; } = BookingStatus.Pending;

    [Column("notes")]
    public string? Notes { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    // Navigation property
    [ForeignKey("PartyId")]
    public virtual Party Party { get; set; } = null!;
} 