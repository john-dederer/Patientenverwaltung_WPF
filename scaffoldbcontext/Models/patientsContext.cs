using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace scaffoldbcontext.Models
{
    public partial class patientsContext : DbContext
    {
        public patientsContext()
        {
        }

        public patientsContext(DbContextOptions<patientsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Healthinsurances> Healthinsurances { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<Patients> Patients { get; set; }
        public virtual DbSet<Treatments> Treatments { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS2;Database=patients;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<Healthinsurances>(entity =>
            {
                entity.HasKey(e => e.HealthinsuranceId)
                    .HasName("PK_dbo.Healthinsurances");

                entity.Property(e => e.City).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Postalcode).IsRequired();

                entity.Property(e => e.Street).IsRequired();

                entity.Property(e => e.Streetnumber).IsRequired();
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Patients>(entity =>
            {
                entity.HasKey(e => e.PatientId)
                    .HasName("PK_dbo.Patients");

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.City).IsRequired();

                entity.Property(e => e.Firstname).IsRequired();

                entity.Property(e => e.Postalcode).IsRequired();

                entity.Property(e => e.Secondname).IsRequired();

                entity.Property(e => e.Street).IsRequired();

                entity.Property(e => e.Streetnumber).IsRequired();
            });

            modelBuilder.Entity<Treatments>(entity =>
            {
                entity.HasKey(e => e.TreatmentId)
                    .HasName("PK_dbo.Treatments");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Treatments)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Treatments_Patients");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_dbo.Users");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Passwordhash).IsRequired();

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.Username).IsRequired();
            });
        }
    }
}
