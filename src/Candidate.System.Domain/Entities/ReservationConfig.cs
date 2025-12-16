using System.ComponentModel.DataAnnotations;
using Candidate.System.Domain.Enums;

namespace Candidate.System.Domain.Entities;

public class ReservationConfig
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public CandidateCategory Category { get; set; }
    
    [Required]
    [Range(0, 100)]
    public decimal ReservationPercentage { get; set; }
    
    [Required]
    public int TotalSeats { get; set; }
    
    public int ReservedSeats => (int)Math.Ceiling(TotalSeats * ReservationPercentage / 100);
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
}