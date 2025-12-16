using Candidate.System.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Candidate.System.Infrastructure.Data;

public class CandidateDbContext : DbContext
{
    public CandidateDbContext(DbContextOptions<CandidateDbContext> options) : base(options)
    {
    }

    public DbSet<Domain.Entities.Candidate> Candidates { get; set; }
    public DbSet<ReservationConfig> ReservationConfigs { get; set; }
    public DbSet<SelectionResult> SelectionResults { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Entities.Candidate>(entity =>
        {
            entity.HasKey(e => e.CandidateId);
            entity.Property(e => e.CandidateName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Marks).HasPrecision(10, 2);
            entity.HasIndex(e => e.Category);
            entity.HasIndex(e => e.Marks);
            entity.HasIndex(e => e.Timestamp);
        });

        modelBuilder.Entity<ReservationConfig>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ReservationPercentage).HasPrecision(5, 2);
            entity.HasIndex(e => e.Category).IsUnique();
        });

        modelBuilder.Entity<SelectionResult>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Marks).HasPrecision(10, 2);
            entity.Property(e => e.CutoffMark).HasPrecision(10, 2);
            entity.HasIndex(e => e.CandidateId);
            entity.HasIndex(e => e.Category);
            entity.HasIndex(e => e.ProcessedAt);
        });

        base.OnModelCreating(modelBuilder);
    }
}