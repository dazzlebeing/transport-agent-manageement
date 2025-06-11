using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tmsminimalapi.Models;

public class Driver
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [Column("driver_name")]
    [StringLength(100)]
    public string DriverName { get; set; } = string.Empty;

    [Required]
    [Column("driver_mobile")]
    [StringLength(15)]
    public string DriverMobile { get; set; } = string.Empty;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    // Navigation property
    public virtual ICollection<BookingTruckAssignment> BookingAssignments { get; set; } = new List<BookingTruckAssignment>();
} 