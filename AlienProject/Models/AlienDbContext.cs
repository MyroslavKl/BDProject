using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AlienProject.Models
{
    public partial class AlienDbContext : DbContext
    {
        public AlienDbContext()
        {
        }

        public AlienDbContext(DbContextOptions<AlienDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Abduction> Abductions { get; set; } = null!;
        public virtual DbSet<Alien> Aliens { get; set; } = null!;
        public virtual DbSet<AlienExcursion> AlienExcursions { get; set; } = null!;
        public virtual DbSet<AlienExperiment> AlienExperiments { get; set; } = null!;
        public virtual DbSet<Excursion> Excursions { get; set; } = null!;
        public virtual DbSet<Experiment> Experiments { get; set; } = null!;
        public virtual DbSet<Human> Humans { get; set; } = null!;
        public virtual DbSet<Killing> Killings { get; set; } = null!;
        public virtual DbSet<SpaceshipExperiment> SpaceshipExperiments { get; set; } = null!;
        public virtual DbSet<Spaceshipp> Spaceshipps { get; set; } = null!;
        public virtual DbSet<SpaceshippVisit> SpaceshippVisits { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-B6DS7BT\\SQLEXPRESS;Database=AlienDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Abduction>(entity =>
            {
                entity.ToTable("Abduction");

                entity.Property(e => e.AbductionId)
                    .ValueGeneratedNever()
                    .HasColumnName("AbductionID");

                entity.Property(e => e.AbductionDate).HasColumnType("date");

                entity.Property(e => e.AlienId).HasColumnName("AlienID");

                entity.Property(e => e.HumanId).HasColumnName("HumanID");

                entity.HasOne(d => d.Alien)
                    .WithMany(p => p.Abductions)
                    .HasForeignKey(d => d.AlienId)
                    .HasConstraintName("FK__Abduction__Alien__3E52440B");

                entity.HasOne(d => d.Human)
                    .WithMany(p => p.Abductions)
                    .HasForeignKey(d => d.HumanId)
                    .HasConstraintName("FK__Abduction__Human__3D5E1FD2");
            });

            modelBuilder.Entity<Alien>(entity =>
            {
                entity.ToTable("Alien");

                entity.HasIndex(e => e.Email, "UQ__Alien__A9D10534A4BEADCE")
                    .IsUnique();

                entity.Property(e => e.AlienId)
                    .ValueGeneratedNever()
                    .HasColumnName("AlienID");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AlienExcursion>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AlienExcursion");

                entity.Property(e => e.AlienId).HasColumnName("AlienID");

                entity.Property(e => e.ExcursionId).HasColumnName("ExcursionID");

                entity.HasOne(d => d.Alien)
                    .WithMany()
                    .HasForeignKey(d => d.AlienId)
                    .HasConstraintName("FK__AlienExcu__Alien__5535A963");

                entity.HasOne(d => d.Excursion)
                    .WithMany()
                    .HasForeignKey(d => d.ExcursionId)
                    .HasConstraintName("FK__AlienExcu__Excur__5441852A");
            });

            modelBuilder.Entity<AlienExperiment>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AlienExperiment");

                entity.Property(e => e.AlienId).HasColumnName("AlienID");

                entity.Property(e => e.ExperimentId).HasColumnName("ExperimentID");

                entity.HasOne(d => d.Alien)
                    .WithMany()
                    .HasForeignKey(d => d.AlienId)
                    .HasConstraintName("FK__AlienExpe__Alien__5812160E");

                entity.HasOne(d => d.Experiment)
                    .WithMany()
                    .HasForeignKey(d => d.ExperimentId)
                    .HasConstraintName("FK__AlienExpe__Exper__571DF1D5");
            });

            modelBuilder.Entity<Excursion>(entity =>
            {
                entity.ToTable("Excursion");

                entity.Property(e => e.ExcursionId)
                    .ValueGeneratedNever()
                    .HasColumnName("ExcursionID");

                entity.Property(e => e.AlienId).HasColumnName("AlienID");

                entity.Property(e => e.ExcursionDate).HasColumnType("date");

                entity.Property(e => e.HumanId).HasColumnName("HumanID");

                entity.HasOne(d => d.Alien)
                    .WithMany(p => p.Excursions)
                    .HasForeignKey(d => d.AlienId)
                    .HasConstraintName("FK__Excursion__Alien__4BAC3F29");

                entity.HasOne(d => d.Human)
                    .WithMany(p => p.Excursions)
                    .HasForeignKey(d => d.HumanId)
                    .HasConstraintName("FK__Excursion__Human__4AB81AF0");
            });

            modelBuilder.Entity<Experiment>(entity =>
            {
                entity.ToTable("Experiment");

                entity.Property(e => e.ExperimentId)
                    .ValueGeneratedNever()
                    .HasColumnName("ExperimentID");

                entity.Property(e => e.AlienId).HasColumnName("AlienID");

                entity.Property(e => e.ExperimentDate).HasColumnType("date");

                entity.Property(e => e.HumanId).HasColumnName("HumanID");

                entity.HasOne(d => d.Alien)
                    .WithMany(p => p.Experiments)
                    .HasForeignKey(d => d.AlienId)
                    .HasConstraintName("FK__Experimen__Alien__4F7CD00D");

                entity.HasOne(d => d.Human)
                    .WithMany(p => p.Experiments)
                    .HasForeignKey(d => d.HumanId)
                    .HasConstraintName("FK__Experimen__Human__4E88ABD4");
            });

            modelBuilder.Entity<Human>(entity =>
            {
                entity.ToTable("Human");

                entity.HasIndex(e => e.Email, "UQ__Human__A9D10534A7091210")
                    .IsUnique();

                entity.Property(e => e.HumanId)
                    .ValueGeneratedNever()
                    .HasColumnName("HumanID");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Killing>(entity =>
            {
                entity.ToTable("Killing");

                entity.Property(e => e.KillingId)
                    .ValueGeneratedNever()
                    .HasColumnName("KillingID");

                entity.Property(e => e.AlienId).HasColumnName("AlienID");

                entity.Property(e => e.HumanId).HasColumnName("HumanID");

                entity.Property(e => e.KillingDate).HasColumnType("date");

                entity.HasOne(d => d.Alien)
                    .WithMany(p => p.Killings)
                    .HasForeignKey(d => d.AlienId)
                    .HasConstraintName("FK__Killing__AlienID__47DBAE45");

                entity.HasOne(d => d.Human)
                    .WithMany(p => p.Killings)
                    .HasForeignKey(d => d.HumanId)
                    .HasConstraintName("FK__Killing__HumanID__46E78A0C");
            });

            modelBuilder.Entity<SpaceshipExperiment>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SpaceshipExperiment");

                entity.Property(e => e.ExperimentId).HasColumnName("ExperimentID");

                entity.Property(e => e.SpaceshipId).HasColumnName("SpaceshipID");

                entity.HasOne(d => d.Experiment)
                    .WithMany()
                    .HasForeignKey(d => d.ExperimentId)
                    .HasConstraintName("FK__Spaceship__Exper__5165187F");

                entity.HasOne(d => d.Spaceship)
                    .WithMany()
                    .HasForeignKey(d => d.SpaceshipId)
                    .HasConstraintName("FK__Spaceship__Space__52593CB8");
            });

            modelBuilder.Entity<Spaceshipp>(entity =>
            {
                entity.HasKey(e => e.SpaceshipId)
                    .HasName("PK__Spaceshi__F9938B95FFB04251");

                entity.ToTable("Spaceshipp");

                entity.Property(e => e.SpaceshipId)
                    .ValueGeneratedNever()
                    .HasColumnName("SpaceshipID");

                entity.Property(e => e.SpaceshipName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpaceshippVisit>(entity =>
            {
                entity.HasKey(e => e.SpaceshipVisitId)
                    .HasName("PK__Spaceshi__A6069F56B4DF4BDA");

                entity.ToTable("SpaceshippVisit");

                entity.Property(e => e.SpaceshipVisitId)
                    .ValueGeneratedNever()
                    .HasColumnName("SpaceshipVisitID");

                entity.Property(e => e.HumanId).HasColumnName("HumanID");

                entity.Property(e => e.SpaceshipId).HasColumnName("SpaceshipID");

                entity.Property(e => e.SpaceshipVisitDate).HasColumnType("date");

                entity.HasOne(d => d.Human)
                    .WithMany(p => p.SpaceshippVisits)
                    .HasForeignKey(d => d.HumanId)
                    .HasConstraintName("FK__Spaceship__Human__4316F928");

                entity.HasOne(d => d.Spaceship)
                    .WithMany(p => p.SpaceshippVisits)
                    .HasForeignKey(d => d.SpaceshipId)
                    .HasConstraintName("FK__Spaceship__Space__440B1D61");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
