using Microsoft.EntityFrameworkCore;
using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Data
{
    public class SanctuaryDbContext : DbContext
    {
        public SanctuaryDbContext(DbContextOptions<SanctuaryDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Sanctuary> Sanctuaries { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<CostManagement> CostManagements { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<MedicalRecord> AnimalsMedicalRecords { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<EnvironmentalData> EnvironmentalData { get; set; }
        public DbSet<WildlifeData> WildlifeData { get; set; }

        public DbSet<Notification>Notifications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Sanctuary -> Animal Relationship
            modelBuilder.Entity<Sanctuary>()
                .HasMany(s => s.Animals)
                .WithOne(a => a.Sanctuary)
                .HasForeignKey(a => a.SanctuaryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Animal -> Sanctuary Relationship
            modelBuilder.Entity<Animal>().HasKey(a => a.AnimalId);
            modelBuilder.Entity<Animal>()
                .HasOne(a => a.Sanctuary)
                .WithMany(s => s.Animals)
                .HasForeignKey(a => a.SanctuaryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Sanctuary -> Resource Relationship
            modelBuilder.Entity<Sanctuary>()
                .HasMany(s => s.Resources)
                .WithOne(r => r.Sanctuary)
                .HasForeignKey(r => r.SanctuaryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Sanctuary -> Project Relationship
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Sanctuary)
                .WithMany()
                .HasForeignKey(p => p.SanctuaryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Incident -> Sanctuary Relationship
            modelBuilder.Entity<Incident>()
                .HasOne(i => i.Sanctuary)
                .WithMany()
                .HasForeignKey(i => i.SanctuaryId)
                .OnDelete(DeleteBehavior.Restrict);

            // MedicalRecord -> Animal Relationship
            modelBuilder.Entity<MedicalRecord>()
                .HasOne(m => m.Animal)
                .WithMany()
                .HasForeignKey(m => m.AnimalId)
                .OnDelete(DeleteBehavior.Restrict);

            // CostManagement -> Sanctuary Relationship
            modelBuilder.Entity<CostManagement>()
                .HasOne(cm => cm.Sanctuary)
                .WithMany()
                .HasForeignKey(cm => cm.SanctuaryId)
                .OnDelete(DeleteBehavior.Restrict);

            // EnvironmentalData -> Sanctuary Relationship
            modelBuilder.Entity<EnvironmentalData>()
                .HasOne(ed => ed.Sanctuary)
                .WithMany()
                .HasForeignKey(ed => ed.SanctuaryId)
                .OnDelete(DeleteBehavior.Restrict);

            // WildlifeData -> Sanctuary Relationship
            modelBuilder.Entity<WildlifeData>()
                .HasOne(wd => wd.Sanctuary)
                .WithMany()
                .HasForeignKey(wd => wd.SanctuaryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Additional configurations (commented for future implementation)
            // For relationships involving users or custom logic, ensure consistent ordering and restrict cascading operations.

            // Example: Report -> User (CreatedBy) Relationship
            // modelBuilder.Entity<Report>()
            //     .HasOne(r => r.CreatedBy)
            //     .WithMany(u => u.Reports)
            //     .HasForeignKey(r => r.UserId)
            //     .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
