using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tmsminimalapi.Models;

public class Party
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [Column("party_name")]
    [StringLength(100)]
    public string PartyName { get; set; } = string.Empty;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    // Navigation property
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
} 