using Beauty_Salon.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Beauty_Salon.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext() : base() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Reservation> Reservations { get; set; } = null!;
        public virtual DbSet<ReservationTerm> ReservationTerms { get; set; } = null!;
        public virtual DbSet<Treatment> Treatments { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-EO8BTG1;Database=BeautySalon;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("Reservation");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ReservationDate).HasColumnType("date");

                entity.Property(e => e.ReservationTermId).HasColumnName("ReservationTermID");

                entity.Property(e => e.Status).HasMaxLength(20);

                entity.Property(e => e.TreatmentId).HasColumnName("TreatmentID");

                entity.Property(e => e.UserId).HasMaxLength(256);

                entity.HasOne(d => d.ReservationTerm)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.ReservationTermId)
                    .HasConstraintName("FK_Reservation_ReservationTerm");

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.TreatmentId)
                    .HasConstraintName("FK_Reservation_Threatment");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Reservation_AspNetUsers");

            });

            modelBuilder.Entity<ReservationTerm>(entity =>
            {
                entity.ToTable("ReservationTerm");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EndHour).HasColumnType("int");

                entity.Property(e => e.StartHour).HasColumnType("int");
            });

            modelBuilder.Entity<Treatment>(entity =>
            {
                entity.ToTable("Treatment");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Image).HasMaxLength(250);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}