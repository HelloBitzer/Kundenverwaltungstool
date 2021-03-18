using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Context.Models
{
    public partial class KundeDBContext : DbContext
    {
        public KundeDBContext()
        {
        }

        public KundeDBContext(DbContextOptions<KundeDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ansprechpartner> Ansprechpartners { get; set; }
        public virtual DbSet<Firma> Firmas { get; set; }
        public virtual DbSet<Kundentermin> Kundentermins { get; set; }
        public virtual DbSet<Termin> Termins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=bitzer-pc2; Database=KundeDB; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Ansprechpartner>(entity =>
            {
                entity.ToTable("Ansprechpartner");

                entity.Property(e => e.AnsprechpartnerId).HasColumnName("AnsprechpartnerID");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirmenId).HasColumnName("FirmenID");

                entity.Property(e => e.Nachname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Telefon)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Titel).HasMaxLength(100);

                entity.Property(e => e.Vorname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Firmen)
                    .WithMany(p => p.Ansprechpartners)
                    .HasForeignKey(d => d.FirmenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ansprechpartner_Firma");
            });

            modelBuilder.Entity<Firma>(entity =>
            {
                entity.HasKey(e => e.FirmenId);

                entity.ToTable("Firma");

                entity.Property(e => e.FirmenId).HasColumnName("FirmenID");

                entity.Property(e => e.Hausnummer)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Ort)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Plz)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("PLZ");

                entity.Property(e => e.Strasse)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Kundentermin>(entity =>
            {
                entity.HasKey(e => new { e.AnsprechpartnerId, e.TerminId });

                entity.ToTable("Kundentermin");

                entity.Property(e => e.AnsprechpartnerId).HasColumnName("AnsprechpartnerID");

                entity.Property(e => e.TerminId).HasColumnName("TerminID");

                entity.HasOne(d => d.Ansprechpartner)
                    .WithMany(p => p.Kundentermins)
                    .HasForeignKey(d => d.AnsprechpartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Kundentermin_Ansprechpartner");

                entity.HasOne(d => d.Termin)
                    .WithMany(p => p.Kundentermins)
                    .HasForeignKey(d => d.TerminId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Kundentermin_Termin");
            });

            modelBuilder.Entity<Termin>(entity =>
            {
                entity.ToTable("Termin");

                entity.Property(e => e.TerminId).HasColumnName("TerminID");

                entity.Property(e => e.Bemerkung)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Ende).HasColumnType("datetime");

                entity.Property(e => e.Start).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
