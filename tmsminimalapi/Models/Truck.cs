using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tmsminimalapi.Models;

public class Truck
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [Column("truck_number")]
    [StringLength(20)]
    public string TruckNumber { get; set; } = string.Empty;

    [Required]
    [Column("truck_size")]
    [StringLength(20)]
    public string TruckSize { get; set; } = string.Empty;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    // Navigation property
    public virtual ICollection<BookingTruckAssignment> BookingAssignments { get; set; } = new List<BookingTruckAssignment>();
} 