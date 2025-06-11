using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace tmsminimalapi.Models;

public class BookingTruckAssignment
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [Column("booking_id")]
    public Guid BookingId { get; set; }

    [Required]
    [Column("truck_id")]
    public Guid TruckId { get; set; }

    [Required]
    [Column("driver_id")]
    public Guid DriverId { get; set; }

    [Required]
    [Column("agreed_amount")]
    [Precision(18, 2)]
    public decimal AgreedAmount { get; set; }

    [Required]
    [Column("advance_paid")]
    [Precision(18, 2)]
    public decimal AdvancePaid { get; set; }

    [Required]
    [Column("assignment_date")]
    public DateTime AssignmentDate { get; set; }

    [Column("notes")]
    public string? Notes { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    [ForeignKey("BookingId")]
    public virtual Booking Booking { get; set; } = null!;

    [ForeignKey("TruckId")]
    public virtual Truck Truck { get; set; } = null!;

    [ForeignKey("DriverId")]
    public virtual Driver Driver { get; set; } = null!;
} 